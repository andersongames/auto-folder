using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoFolder.Core;

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
  /// <param name="extensionFilter">Optional file extension to filter (e.g. ".mp4")</param>
  /// <param name="deleteOriginals">If true, original files will be deleted after copying</param>
  /// <param name="normalizeGroupNames">If true, group names will be normaized (remove spaces/symbols, use lowercase)</param>
  /// <param name="dryRun">If true, it will show the actions that will be performed, but no files will actually be copied or deleted.</param>
  public void Organize(string sourceDirectory, string? extensionFilter, bool deleteOriginals, bool normalizeGroupNames, bool dryRun)
  {
    // Get all files in the directory (not recursive)
    string[] allFiles = Directory.GetFiles(sourceDirectory);

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
      string groupName = group.Key;

      // Optionally normalize group name
      if (normalizeGroupNames)
      {
        groupName = normalizeGroupName(groupName);
      }

      // Generate the target folder path
      string targetFolder = Path.Combine(sourceDirectory, groupName);

      Logger.Log($"Starting group '{groupName}' with {group.Value.Count} file(s).");

      if (!dryRun)
      {
        // Ensure the group folder exists (if not dry-run mode)
        Directory.CreateDirectory(targetFolder);
      }

      // Keep track of the preocessed files per group
      int totalFiles = group.Value.Count();
      int groupProcessedCount = 0;

      foreach (var filePath in group.Value)
      {
        string destinationPath = Path.Combine(targetFolder, Path.GetFileName(filePath));

        try
        {
          if (dryRun)
          {
            // Simulate copy
            Logger.Log($"[DRY-RUN] Would copy: {filePath} → {destinationPath}", true);

            if (deleteOriginals)
            {
              // Simulate deletion
              Logger.Log($"[DRY-RUN] Would delete: {filePath}", true);
            }

            groupProcessedCount++;
            processedCount++;
            continue;
          }

          // Attempt to copy the file to the target folder (overwrite if needed)
          File.Copy(filePath, destinationPath, overwrite: true);
          Logger.Log($"Copied: {filePath} → {destinationPath}");

          // Optionally delete the original file after a successful copy
          if (deleteOriginals)
          {
            File.Delete(filePath);
            Logger.Log($"Deleted: {filePath}");
          }

          // Increment the counters only if the file was successfully handled
          groupProcessedCount++;
          processedCount++;
        }
        catch (Exception ex)
        {
          // If something fails, report it and continue with the next file
          Console.WriteLine($"⚠️ Failed to process file: {Path.GetFileName(filePath)}");
          Console.WriteLine($"   → Reason: {ex.Message}");
          Logger.Log($"ERROR: Failed to process {filePath} → {ex.Message}");
        }

        // Show progress line after each file
        Console.WriteLine($"   [{groupProcessedCount}/{totalFiles}] {Path.GetFileName(filePath)} processed");
      }

      Console.WriteLine($"📁 Group '{groupName}' organized with {group.Value.Count} file(s).");
    }
    Console.WriteLine();
    Console.WriteLine($"💯 Total of '{processedCount}' file(s) organized.");
    Logger.Log(dryRun
      ? "Dry-run finished. No files were modified."
      : "File organization completed.");
  }

  /// <summary>
  /// Groups files by extracting a common prefix from the filename using regex.
  /// Example: "project-report-v1.pdf" → group: "project-report"
  /// </summary>
  /// <param name="filePaths">Array of file paths to group</param>
  /// <returns>Dictionary with group name as key and list of files as value</returns>
  internal Dictionary<string, List<string>> GroupFilesByPrefix(string[] filePaths)
  {
    var groups = new Dictionary<string, List<string>>();

    // This regex captures the prefix before a version/episode/number pattern
    var regex = new Regex(@"^(.*?)([-_ ]?(ep|part|v|vol|season)?[-_ ]?\d+)?$", RegexOptions.IgnoreCase);

    foreach (var path in filePaths)
    {
      string fileName = Path.GetFileNameWithoutExtension(path);
      var match = regex.Match(fileName);

      // Use the captured prefix group if matched, or the whole name as fallback
      string groupKey = match.Success ? match.Groups[1].Value.Trim('-', '_', ' ') : fileName;

      if (!groups.ContainsKey(groupKey))
        groups[groupKey] = new List<string>();

      groups[groupKey].Add(path);
    }

    return groups;
  }

  /// <summary>
  /// Normalizes a folder name by trimming, removing special characters,
  /// replacing spaces with dashes, and converting to lowercase.
  /// </summary>
  internal string normalizeGroupName(string name)
  {
    // Trim leading/trailing spaces
    string result = name.Trim();

    // Replace spaces and underscores with dashes
    result = result.Replace(" ", "-").Replace("_", "-");

    // Remove unwanted symbols (keep letters, numbers, dashes)
    result = Regex.Replace(result, @"[^a-zA-Z0-9\-]", "");

    // Convert to lowercase
    return result.ToLower();
  }
}
