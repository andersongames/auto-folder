using Xunit;
using AutoFolder.Core;
using System.Collections.Generic;

namespace AutoFolder.Tests;

/// <summary>
/// Unit tests for the FileOrganizer class, specifically the logic that groups files by prefix.
/// </summary>
public class FileOrganizerTests
{
    // Region: GroupFilesByPrefix

    /// <summary>
    /// Test case: files with a common prefix should be grouped under the same key.
    /// </summary>
    [Fact]
    public void GroupFilesByPrefix_ShouldGroupFilesWithSamePrefix()
    {
        // Arrange: simulate a set of filenames that share a common prefix
        var filePaths = new[]
        {
            "C:/mock/video-ep01.mp4",
            "C:/mock/video-ep02.mp4",
            "C:/mock/video-ep03.mp4"
        };

        var organizer = new FileOrganizer();

        // Act: perform the grouping logic
        var result = organizer.GroupFilesByPrefix(filePaths);

        // Assert: all files should be grouped under "video"
        Assert.Single(result); // Only one group
        Assert.True(result.ContainsKey("video"));
        Assert.Equal(3, result["video"].Count);
    }

    /// <summary>
    /// Test case: files with distinct prefixes should form separate groups.
    /// </summary>
    [Fact]
    public void GroupFilesByPrefix_ShouldSeparateDistinctPrefixes()
    {
        var filePaths = new[]
        {
            "C:/mock/intro.mp4",
            "C:/mock/trailer.mp4",
            "C:/mock/ending.mp4"
        };

        var organizer = new FileOrganizer();
        var result = organizer.GroupFilesByPrefix(filePaths);

        // Assert: each file forms its own group
        Assert.Equal(3, result.Count);
        Assert.Contains("intro", result.Keys);
        Assert.Contains("trailer", result.Keys);
        Assert.Contains("ending", result.Keys);
    }

    /// <summary>
    /// Test case: when no file matches a grouping pattern, fallback should be full name or separate groups.
    /// </summary>
    [Fact]
    public void GroupFilesByPrefix_ShouldHandleNonMatchingPatterns()
    {
        var filePaths = new[]
        {
            "C:/mock/file1.docx",
            "C:/mock/file2-final.docx",
            "C:/mock/random123.txt"
        };

        var organizer = new FileOrganizer();
        var result = organizer.GroupFilesByPrefix(filePaths);

        // The actual grouping logic might vary depending on Regex
        // Here we just make sure files are not lost and all are grouped somehow
        int totalFilesGrouped = 0;
        foreach (var group in result.Values)
        {
            totalFilesGrouped += group.Count;
        }

        Assert.Equal(3, totalFilesGrouped);
    }

    /// <summary>
    /// Test case: input with no files should return an empty group set.
    /// </summary>
    [Fact]
    public void GroupFilesByPrefix_ShouldReturnEmpty_WhenNoFiles()
    {
        var filePaths = new string[] { };
        var organizer = new FileOrganizer();

        var result = organizer.GroupFilesByPrefix(filePaths);

        Assert.Empty(result);
    }

    // Region: NormalizeGroupName

    /// <summary>
    /// Test case: check that NormalizeGroupName handles various formatting scenarios.
    /// </summary>
    [Theory]
    [InlineData("  My Folder  ", "my-folder")]
    [InlineData("folder_name", "folder-name")]
    [InlineData("Proj@ct!", "projct")]
    [InlineData("Project", "project")]
    [InlineData(" Série_01 (Completa)", "srie-01-completa")]
    [InlineData("EXTRA__  Spaces__", "extra-spaces")]
    public void NormalizeGroupName_ShouldFormatProperly(string input, string expected)
    {
        // Arrange
        var organizer = new FileOrganizer();

        // Act
        string result = organizer.NormalizeGroupName(input);

        // Assert
        Assert.Equal(expected, result);
    }

    // Region: Extension Filtering
    [Fact]
    public void ShouldFilterByExtension()
    {

    }

    // Region: Dry-run behavior
    [Fact]
    public void DryRunShouldNotModifyFiles()
    {

    }
}
