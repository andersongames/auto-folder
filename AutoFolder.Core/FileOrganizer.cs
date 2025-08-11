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
  /// <param name="destinationDirectory">Directory to move the organized files</param>
  /// <param name="extensionFilter">Optional file extension to filter (e.g. ".mp4")</param>
  /// <param name="deleteOriginals">If true, original files will be deleted after copying</param>
  /// <param name="normalizeGroupNames">If true, group names will be normaized (remove spaces/symbols, use lowercase)</param>
  /// <param name="dryRun">If true, it will show the actions that will be performed, but no files will actually be copied or deleted.</param>
  public void Organize(string sourceDirectory, string? destinationDirectory, string? extensionFilter, bool deleteOriginals, bool normalizeGroupNames, bool dryRun)
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
    Console.WriteLine($"💯 Total of '{processedCount}' file(s) organized, under '{groupedFiles.Count}' group(s).");
    Logger.Log(dryRun
      ? "Dry-run finished. No files were modified."
      : "File organization completed.");
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
  internal Dictionary<string, List<string>> GroupFilesByPrefix(string[] filePaths)
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
  private string GetCommonPrefix(string a, string b)
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
  internal string NormalizeGroupName(string name)
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
}
