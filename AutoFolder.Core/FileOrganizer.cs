using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoFolder.Core;

/// <summary>
/// Lightweight progress payload reported by FileOrganizer during long operations.
/// </summary>
public readonly record struct ProgressInfo(
    int Processed,     // number of files processed so far
    int Total,         // total files planned to process
    string CurrentFile, // file currently being copied/deleted/simulated
    string Stage       // e.g., "copy", "delete", "dry-run", "scan"
);

/// <summary>
/// Handles the logic for grouping and organizing files into folders
/// based on shared naming patterns.
/// </summary>
public class FileOrganizer
{
    /// <summary>
    /// Organizes files in the specified directory by grouping them into folders
    /// based on common name prefixes.
    /// </summary>
    /// <param name="sourceDirectory">Directory to scan for files</param>
    /// <param name="destinationDirectory">Directory to move the organized files</param>
    /// <param name="extensionFilter">Optional file extension to filter (e.g. ".mp4")</param>
    /// <param name="deleteOriginals">If true, original files will be deleted after copying</param>
    /// <param name="normalizeGroupNames">If true, group names will be normaized (remove spaces/symbols, use lowercase)</param>
    /// <param name="dryRun">If true, it will show the actions that will be performed, but no files will actually be copied or deleted.</param>
    /// <param name="progress">OPTIONAL: UI progress bar hook.</param>
    /// <param name="includeSubdirectories">If true, it will process sub-directories.</param>
    /// <param name="log">OPTIONAL: UI log sink.</param>
    /// <param name="cancellationToken">OPTIONAL: allow cancel from UI.</param>
    public void Organize(
    string sourceDirectory,
    string? destinationDirectory,
    string? extensionFilter,
    bool deleteOriginals,
    bool normalizeGroupNames,
    bool dryRun,
    bool includeSubdirectories,
    IProgress<ProgressInfo>? progress = null,
    Action<string>? log = null,
    CancellationToken cancellationToken = default
)
  {
    // Determine search mode based on user selection
    SearchOption searchOption = includeSubdirectories
        ? SearchOption.AllDirectories
        : SearchOption.TopDirectoryOnly;

    // Enumerate files using streaming (better memory usage for large trees)
    string[] allFiles = Directory
        .GetFiles(sourceDirectory, "*", searchOption);

    string effectiveDestination = destinationDirectory ?? sourceDirectory;
    string fullDestination = Path.GetFullPath(effectiveDestination);

    string resolvedDestination = destinationDirectory ?? sourceDirectory;

    string fullSource = Path.GetFullPath(sourceDirectory);

    // Allow same directory (in-place organization)
    bool sameDirectory = string.Equals(fullSource, fullDestination, StringComparison.OrdinalIgnoreCase);

    // But forbid destination being a child of source
    if (!sameDirectory &&
        fullDestination.StartsWith(fullSource, StringComparison.OrdinalIgnoreCase))
    {
        throw new InvalidOperationException(
            "Destination directory cannot be inside the source directory when including subdirectories. " +
            "This would cause recursive reprocessing.");
    }

    // If an extension filter is provided, apply it (e.g., only ".pdf" files)
    var filteredFiles = string.IsNullOrWhiteSpace(extensionFilter)
      ? allFiles
      : Array.FindAll(allFiles, f =>
        Path.GetExtension(f).Equals(extensionFilter, StringComparison.OrdinalIgnoreCase));

    // Group files by name prefix
    Dictionary<string, List<string>> groupedFiles = GroupFilesByPrefix(filteredFiles);

    // Count the total number of files successfully processed
    int processedCount = 0;

    // For each group, create a folder and copy the files into it
    foreach (var group in groupedFiles)
    {
      // Check for cancellation request
      cancellationToken.ThrowIfCancellationRequested();

      string groupName = group.Key;

      // Optionally normalize group name
      if (normalizeGroupNames)
      {
        groupName = NormalizeGroupName(groupName);
      }

      // Generate the target folder path
      string targetFolder = Path.Combine(destinationDirectory ?? sourceDirectory, groupName);

      // Create the target directory if it does not exist (only if not in dry-run mode)
      if (!Directory.Exists(targetFolder) && !dryRun)
      {
        Directory.CreateDirectory(targetFolder);
      }

      Logger.Log($"Starting group '{groupName}' with {group.Value.Count} file(s).");
      log?.Invoke($"Starting group '{groupName}' with {group.Value.Count} file(s).");

      if (!dryRun)
      {
        // Ensure the group folder exists (if not dry-run mode)
        Directory.CreateDirectory(targetFolder);
      }

      // Keep track of the preocessed files per group
      int totalGroupFiles = group.Value.Count();
      int groupProcessedCount = 0;

      foreach (var filePath in group.Value)
      {
        // Check for cancellation request
        cancellationToken.ThrowIfCancellationRequested();

        string destinationPath = Path.Combine(Path.GetFullPath(targetFolder), Path.GetFileName(filePath));

        try
        {
          if (dryRun)
          {
            // Report current dry-run-copy task to UI
            progress?.Report(new ProgressInfo(
                processedCount,
                filteredFiles.Length,
                filePath,
                "dry-run-copy"
            ));

            // Simulate copy
            Logger.Log($"[DRY-RUN] Would copy: {filePath} → {destinationPath}", true);
            log?.Invoke($"📄 Would copy: {filePath} → {destinationPath}");

            if (deleteOriginals)
            {
              // Report current dry-run-delete task to UI
              progress?.Report(new ProgressInfo(
                  processedCount,
                  filteredFiles.Length,
                  filePath,
                  "dry-run-delete"
              ));

              // Simulate deletion
              Logger.Log($"[DRY-RUN] Would delete: {filePath}", true);
              log?.Invoke($"🗑️ Would delete: {filePath}");
            }

            groupProcessedCount++;
            processedCount++;

            // Report progress to UI
            progress?.Report(new ProgressInfo(
                processedCount,
                filteredFiles.Length,
                filePath,
                deleteOriginals ? "dry-run-delete" : "dry-run-copy"
            ));

            continue;
          }

          // Report current copy task to UI
          progress?.Report(new ProgressInfo(
              processedCount,
              filteredFiles.Length,
              filePath,
              "copy"
          ));

          if (string.Equals(filePath, destinationPath, StringComparison.OrdinalIgnoreCase))
          {
            if (deleteOriginals)
            {
              // Special case: source == destination and user wants to delete originals
              // → This would cause data loss if skipped.
              // Solution: treat as a "move within same path" (no-op).
              Logger.Log($"Skipped delete+copy (same source and destination): {filePath}");
              log?.Invoke($"⚠️ Skipped dangerous operation (same source and destination): {filePath}");
            }
            else
            {
              Logger.Log($"Skipped copy (same source and destination): {filePath}");
              log?.Invoke($"⏭️ Skipped (already in correct location): {filePath}");
            }
          }
          else
          {
            // Attempt to copy the file to the target folder (overwrite if needed)
            File.Copy(filePath, destinationPath, overwrite: true);
            Logger.Log($"Copied: {filePath} → {destinationPath}");
            log?.Invoke($"📄 Copied: {filePath} → {destinationPath}");

            // Optionally delete the original file after a successful copy
            if (deleteOriginals)
            {
              // Report current delete task to UI
              progress?.Report(new ProgressInfo(
                  processedCount,
                  filteredFiles.Length,
                  filePath,
                  "delete"
              ));

              File.Delete(filePath);
              Logger.Log($"Deleted: {filePath}");
              log?.Invoke($"Deleted: {filePath}");
            }
          }

          // Increment the counters only if the file was successfully handled
          groupProcessedCount++;
          processedCount++;

          // Report success to UI
          progress?.Report(new ProgressInfo(
              processedCount,
              filteredFiles.Length,
              filePath,
              deleteOriginals ? "delete" : "copy"
          ));
        }
        catch (Exception ex)
        {
          // If something fails, report it and continue with the next file
          Console.WriteLine($"⚠️ Failed to process file: {Path.GetFileName(filePath)}");
          Console.WriteLine($"   → Reason: {ex.Message}");
          Logger.Log($"ERROR: Failed to process {filePath} → {ex.Message}");
          log?.Invoke($"ERROR: Failed to process {filePath} → {ex.Message}");
        }

        // Show progress line after each file
        Console.WriteLine($"   [{groupProcessedCount}/{totalGroupFiles}] {Path.GetFileName(filePath)} processed");
      }

      if (dryRun)
      {
        Console.WriteLine($"📁 Group '{groupName}' would be organized with {group.Value.Count} file(s).");
        log?.Invoke($"📁 Group '{groupName}' would be organized with {group.Value.Count} file(s).");
      }
      else
      {
        Console.WriteLine($"📁 Group '{groupName}' organized with {group.Value.Count} file(s).");
        log?.Invoke($"📁 Group '{groupName}' organized with {group.Value.Count} file(s).");
      }
    }

    // Report progress to UI
    progress?.Report(new ProgressInfo(
        deleteOriginals ? processedCount : processedCount,
        filteredFiles.Length,
        "success",
        "success"
    ));

    // Success logs
    Console.WriteLine();
    if (dryRun)
    {
      Console.WriteLine($"💯 Total of '{processedCount}' file(s) would be organized, under '{groupedFiles.Count}' group(s).");
      Logger.Log("Dry-run finished. No files were modified.");
      log?.Invoke($"💯 Total of '{processedCount}' file(s) would be organized, under '{groupedFiles.Count}' group(s).");
    }
    else
    {
      Console.WriteLine($"💯 Total of '{processedCount}' file(s) organized, under '{groupedFiles.Count}' group(s).");
      Logger.Log("File organization completed.");
      log?.Invoke($"💯 Total of '{processedCount}' file(s) organized, under '{groupedFiles.Count}' group(s).");
    }
  }

  /// <summary>
  /// Groups files by finding the longest common prefix in their file names (excluding extension).
  /// All files sharing this prefix will be placed in the same group.
  /// Example:
  ///   "report_final_2024 (Q1).docx"
  ///   "report_final_2024 (Q2).docx"
  /// → group: "report_final_2024 (Q"
  /// </summary>
  /// <param name="filePaths">Array of file paths to group</param>
  /// <returns>Dictionary with the group name (longest common prefix) as key and list of files as value</returns>
  internal static Dictionary<string, List<string>> GroupFilesByPrefix(string[] filePaths)
  {
    var groups = new Dictionary<string, List<string>>();

    foreach (var path in filePaths)
    {
      string fileName = Path.GetFileNameWithoutExtension(path);

      bool added = false;

      // Try to find an existing group that shares a common prefix with the current file
      foreach (var existingGroup in groups.Keys.ToList())
      {
        // Get the longest common prefix between the current group key and the file name
        string commonPrefix = GetCommonPrefix(existingGroup, fileName);

        // Require a minimum number of characters in common to consider it the same group
        if (commonPrefix.Length >= 3)
        {
          // We need to rename the group key if the new common prefix is shorter than the existing one
          // This ensures the group name always represents the actual shared prefix of all files inside it
          if (commonPrefix != existingGroup)
          {
            // Get the existing file list and reassign it under the new prefix
            var filesInGroup = groups[existingGroup];
            groups.Remove(existingGroup);
            groups[commonPrefix] = filesInGroup;
          }

          // Add the current file to the updated group
          groups[commonPrefix].Add(path);
          added = true;
          break;
        }
      }

      // If no matching group was found, create a new group with the filename as the starting "prefix"
      if (!added)
      {
        groups[fileName] = new List<string> { path };
      }
    }

    return groups;
  }

  /// <summary>
  /// Finds the longest common prefix between two strings.
  /// The prefix is trimmed to remove trailing separators or punctuation
  /// so that folder names are cleaner.
  /// </summary>
  private static string GetCommonPrefix(string a, string b)
  {
    int minLength = Math.Min(a.Length, b.Length);
    int i = 0;

    // Iterate character by character until they differ
    while (i < minLength && a[i] == b[i])
    {
      i++;
    }

    // Trim any trailing separators or punctuation from the prefix
    return a.Substring(0, i).Trim('-', '_', ' ', '.', '(', ')');
  }

  /// <summary>
  /// Normalizes a folder name by trimming, removing special characters,
  /// replacing spaces with dashes, and converting to lowercase.
  /// </summary>
  internal static string NormalizeGroupName(string name)
  {
    // Trim leading/trailing spaces
    string result = name.Trim();

    // Replace spaces and underscores with dashes
    result = result.Replace(" ", "-").Replace("_", "-");

    // Collapse multiple dashes into one
    result = Regex.Replace(result, "-{2,}", "-");

    // Remove unwanted characters from the core (only keep a-z, A-Z, 0-9 and dash)
    result = Regex.Replace(result, @"[^a-zA-Z0-9\-]", "");

    // Remove trailing dash if any
    result = Regex.Replace(result, @"-+$", "");

    // Turn all chars to lower case
    result = result.ToLower();

    return result;
  }

  /// <summary>
  /// Validates whether the given string can represent a valid file system path.
  /// This method checks only for invalid path characters, not for the existence
  /// of the directory. It is useful when the path may be created later.
  /// </summary>
  /// <param name="path">The path string to validate.</param>
  /// <returns>True if the path is syntactically valid, otherwise false.</returns>
  public static bool IsPathSyntacticallyValid(string path)
  {
    if (string.IsNullOrWhiteSpace(path))
      return false;

    // Check for invalid path characters
    char[] invalidPathChars = Path.GetInvalidPathChars();
    if (path.IndexOfAny(invalidPathChars) >= 0)
      return false;

    // Check for invalid file name characters if the path is intended for a file
    // This is more restrictive than path characters
    string fileName = Path.GetFileName(path);
    if (!string.IsNullOrEmpty(fileName))
    {
      char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
      if (fileName.IndexOfAny(invalidFileNameChars) >= 0)
        return false;
    }

    return true;
  }

  /// <summary>
  /// Normalize the file extension to start with dot (".pdf" not "pdf").
  /// </summary>
  /// <param name="extension">The provided file extenssion.</param>
  /// <returns>The normalized extension.</returns>
  public static string NormalizeFileExtension(string extension)
  {
    string ext = extension.Trim();
    if (!ext.StartsWith("."))
    {
      ext = "." + ext;
      return ext;
    }
    else
      return extension;
  }
}

