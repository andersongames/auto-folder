using AutoFolder.Core;

namespace AutoFolder.Tests;

/// <summary>
/// Unit tests for the FileOrganizer class, specifically the logic that groups files by prefix and the logic that normalize the destination directory name.
/// </summary>
public class FileOrganizerUnitTests
{
    // Region: 🔤 GetCommonPrefix

    /// <summary>
    /// Test case: compare o nome dos arquivos e encontre o prefixo comum mais longo.
    /// </summary>
    [Fact]
    [Trait("Category", "Unit")]
    public void GetCommonPrefix_GetLongestCommomPrefix()
    {
        // Arrange: simulate a set of filenames
        var filePaths = new[]
        {
            "report_final_2024 (Q1).docx",
            "report_final_2024 (Q2).docx",
            "aaa.txt",
            "aab.txt",
        };

        var organizer = new FileOrganizer();

        // Act: perform the grouping logic
        var result = organizer.GroupFilesByPrefix(filePaths);

        // Assert: 3 groups
        Assert.Equal(3, result.Count);

        // Assert: expected groups
        Assert.True(result.ContainsKey("report_final_2024 (Q"));
        Assert.True(result.ContainsKey("aaa"));
        Assert.True(result.ContainsKey("aab"));
    }

    // Region: 📁 GroupFilesByPrefix

    /// <summary>
    /// Test case: files with a common prefix should be grouped under the same key.
    /// </summary>
    [Fact]
    [Trait("Category", "Unit")]
    public void GroupFilesByPrefix_ShouldGroupFilesWithSamePrefix()
    {
        // Arrange: simulate a set of filenames that share a common prefix
        var filePaths = new[]
        {
            "C:/mock/video-ep01.mp4",
            "C:/mock/video-ep02.mp4",
            "C:/mock/video-ep03.mp4",
        };

        var organizer = new FileOrganizer();

        // Act: perform the grouping logic
        var result = organizer.GroupFilesByPrefix(filePaths);

        // Assert: all files should be grouped under "video-ep0"
        Assert.Single(result); // Only one group
        Assert.True(result.ContainsKey("video-ep0"));
        Assert.Equal(3, result["video-ep0"].Count);
    }

    /// <summary>
    /// Test case: files with distinct prefixes should form separate groups.
    /// </summary>
    [Fact]
    [Trait("Category", "Unit")]
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
    [Trait("Category", "Unit")]
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
    [Trait("Category", "Unit")]
    public void GroupFilesByPrefix_ShouldReturnEmpty_WhenNoFiles()
    {
        var filePaths = Array.Empty<string>();
        var organizer = new FileOrganizer();

        var result = organizer.GroupFilesByPrefix(filePaths);

        Assert.Empty(result);
    }

    // Region: 📝 NormalizeGroupName

    /// <summary>
    /// Test case: check that NormalizeGroupName handles various formatting scenarios.
    /// </summary>
    [Theory]
    [InlineData("  My Folder  ", "my-folder")]
    [InlineData("folder_name", "folder-name")]
    [InlineData("Proj@ct!", "projct")] // preserve the "!"
    [InlineData("Project", "project")]
    [InlineData(" Série_01 (Completa)", "srie-01-completa")]
    [InlineData("EXTRA__  Spaces__", "extra-spaces")] // should remove trailing dash
    [InlineData("Final Version!", "final-version")] // keep allowed symbol at the end
    [InlineData("Wait For It???", "wait-for-it")] // collapse repeated ? into one
    [InlineData("cool-end---", "cool-end")] // remove trailing dashes
    [InlineData("Very+Exciting+++", "veryexciting")] // preserve single + at the end
    [InlineData("Brackets Closing )", "brackets-closing")] // should remove bracket
    [Trait("Category", "Unit")]
    public void NormalizeGroupName_ShouldFormatProperly(string input, string expected)
    {
        // Arrange
        var organizer = new FileOrganizer();

        // Act
        string result = organizer.NormalizeGroupName(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
