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
  public void Organize(string sourceDirectory, string? extensionFilter, bool deleteOriginals)
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

    // For each group, create a folder and copy the files into it
    foreach (var group in groupedFiles)
    {
      string groupName = group.Key;
      string targetFolder = Path.Combine(sourceDirectory, groupName);

      // Ensure the group folder exists
      Directory.CreateDirectory(targetFolder);

      foreach (var filePath in group.Value)
      {
        string destinationPath = Path.Combine(targetFolder, Path.GetFileName(filePath));

        // Copy the file to the new folder (overwrite if needed)
        File.Copy(filePath, destinationPath, overwrite: true);

        // Delete the original file if the user requested
        if (deleteOriginals)
        {
          File.Delete(filePath);
        }
      }

      Console.WriteLine($"📁 Group '{groupName}' organized with {group.Value.Count} file(s).");
    }
  }

  /// <summary>
  /// Groups files by extracting a common prefix from the filename using regex.
  /// Example: "project-report-v1.pdf" → group: "project-report"
  /// </summary>
  /// <param name="filePaths">Array of file paths to group</param>
  /// <returns>Dictionary with group name as key and list of files as value</returns>
  private Dictionary<string, List<string>> GroupFilesByPrefix(string[] filePaths)
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
}
