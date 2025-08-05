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
    [InlineData("Proj@ct!", "projct!")] // preserve the "!"
    [InlineData("Project", "project")]
    [InlineData(" Série_01 (Completa)", "srie-01-completa")]
    [InlineData("EXTRA__  Spaces__", "extra-spaces")] // should remove trailing dash
    [InlineData("Final Version!", "final-version!")] // keep allowed symbol at the end
    [InlineData("Wait For It???", "wait-for-it?")] // collapse repeated ? into one
    [InlineData("cool-end---", "cool-end")] // remove trailing dashes
    [InlineData("Very+Exciting+++", "veryexciting+")] // preserve single + at the end
    [InlineData("Brackets Closing )", "brackets-closing")] // should remove bracket
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

    /// <summary>
    /// Test case: only files matching the given extension should be processed.
    /// </summary>
    [Fact]
    public void Organize_ShouldOnlyProcessFilesWithGivenExtension()
    {
        // Arrange
        string tempSourceDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        // Note: tempDestDir is unused for now; kept for future use when Organize supports destination dir
        string tempDestDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempSourceDir);
        Directory.CreateDirectory(tempDestDir);

        try
        {
            // Create test files with different extensions
            string[] testFiles =
            {
                "file1.pdf",
                "file2.pdf",
                "file3.docx",
                "video.mp4"
            };

            foreach (var file in testFiles)
            {
                File.WriteAllText(Path.Combine(tempSourceDir, file), "test content");
            }

            var organizer = new FileOrganizer();

            // Act: filter only ".pdf"
            organizer.Organize(tempSourceDir, ".pdf", false, false, false);

            // Derive programmatically the group name
            var groups = organizer.GroupFilesByPrefix(
                Directory.GetFiles(tempSourceDir)
                    .Where(f => f.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    .ToArray()
            );
            string groupName = groups.Keys.First();

            // Assert: new directory for organized files exists
            string groupFolder = Path.Combine(tempSourceDir, groupName);
            Assert.True(Directory.Exists(groupFolder));

            // Assert: only PDF files should have been copied
            string[] copiedFiles = Directory.GetFiles(groupFolder);
            Assert.All(copiedFiles, f => Assert.EndsWith(".pdf", f));
            Assert.Equal(2, copiedFiles.Length);

            // Assert: no directory is created for non-PDF files
            string ignoredFile3 = Path.Combine(tempSourceDir, "file3");
            string ignoredVideo = Path.Combine(tempSourceDir, "video");
            Assert.False(Directory.Exists(ignoredFile3));
            Assert.False(Directory.Exists(ignoredVideo));
        }
        finally
        {
            // Cleanup
            if (Directory.Exists(tempSourceDir)) Directory.Delete(tempSourceDir, true);
            if (Directory.Exists(tempDestDir)) Directory.Delete(tempDestDir, true);
        }
    }

    /// <summary>
    /// Test case: if no extension is provided, all files should be processed.
    /// </summary>
    [Fact]
    public void Organize_ShouldProcessAllFiles_WhenNoExtensionProvided()
    {
        string tempSourceDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        // Note: tempDestDir is unused for now; kept for future use when Organize supports destination dir
        string tempDestDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempSourceDir);
        Directory.CreateDirectory(tempDestDir);

        try
        {
            string[] testFiles =
            {
                "data1.csv",
                "report.docx",
                "slide.pptx",
                "audio.mp3"
            };

            foreach (var file in testFiles)
            {
                File.WriteAllText(Path.Combine(tempSourceDir, file), "test content");
            }

            var organizer = new FileOrganizer();

            // Act: pass null as extension (process all files)
            organizer.Organize(tempSourceDir, null, false, false, false);

            // Derive programmatically the group names
            var groups = organizer.GroupFilesByPrefix(Directory.GetFiles(tempSourceDir));
            int totalCopied = groups
                .Select(g => Directory.GetFiles(Path.Combine(tempSourceDir, g.Key)).Length)
                .Sum();

            // Assert: all files should  be processed
            Assert.Equal(testFiles.Length, totalCopied);

            foreach (var group in groups)
            {
                string groupFolder = Path.Combine(tempSourceDir, group.Key);

                // Assert: new directories for organized files exists
                Assert.True(Directory.Exists(groupFolder));

                // Assert: all files should have been copied
                string[] copiedFiles = Directory.GetFiles(groupFolder);
                Assert.Equal(1, copiedFiles.Length);
            }
        }
        finally
        {
            if (Directory.Exists(tempSourceDir)) Directory.Delete(tempSourceDir, true);
            if (Directory.Exists(tempDestDir)) Directory.Delete(tempDestDir, true);
        }
    }

    // // Region: Dry-run behavior
    // [Fact]
    // public void DryRunShouldNotModifyFiles()
    // {

    // }
}
