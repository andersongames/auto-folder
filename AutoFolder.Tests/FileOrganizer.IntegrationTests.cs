using System.Diagnostics;
using AutoFolder.Core;

namespace AutoFolder.Tests;

/// <summary>
/// Integration tests for the FileOrganizer class, specifically the organize method.
/// </summary>
public class FileOrganizerIntegrationTests
{
    /// <summary>
    /// Test case:
    /// destinationDirectory: null -> if no destination directory is provided, the source  directory must be used.
    /// extensionFilter: null -> if no extension is provided, all files should be processed.
    /// deleteOriginals: false -> if false, the original file must be kept.
    /// normalizeGroupNames: false -> if false, source directory names should not be normalized.
    /// dryRun: false -> if false, no files are copied or deleted.
    /// </summary>
    [Fact]
    [Trait("Category", "Integration")]
    public void Organize_AllNullAndFalse()
    {
        string tempSourceDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        string tempDestDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempSourceDir);
        Directory.CreateDirectory(tempDestDir);

        try
        {
            string[] testFiles =
            {
                "data1.csv",
                "report_final_2024 (Q1).docx",
                "report_final_2024 (Q2).docx",
                "slide.pptx",
                "audio.mp3"
            };

            foreach (var file in testFiles)
            {
                File.WriteAllText(Path.Combine(tempSourceDir, file), "test content");
            }

            var organizer = new FileOrganizer();

            // Act
            organizer.Organize(
                sourceDirectory: tempSourceDir,
                destinationDirectory: null,
                extensionFilter: null,
                deleteOriginals: false,
                normalizeGroupNames: false,
                dryRun: false
            );

            // Derive programmatically the group names
            var groups = organizer.GroupFilesByPrefix(Directory.GetFiles(tempSourceDir));

            // Assert: all files should  be processed
            int totalCopied = groups
                .Select(g => Directory.GetFiles(Path.Combine(tempSourceDir, g.Key)).Length)
                .Sum();
            Assert.Equal(testFiles.Length, totalCopied);

            // Debug
            string[] groupNames = groups.Keys.ToArray();
            Assert.False(true, $"Fail on purpose {groupNames[0]} {groupNames[1]} {groupNames[2]} {groupNames[3]} {groupNames[4]}");

            foreach (var group in groups)
            {
                string groupFolder = Path.Combine(tempSourceDir, group.Key);

                // Assert: new directories for organized files exists
                Assert.True(Directory.Exists(groupFolder));

                // Assert: all files should have been copied
                string[] copiedFiles = Directory.GetFiles(groupFolder);
                Assert.Single(copiedFiles);
            }
        }
        finally
        {
            if (Directory.Exists(tempSourceDir)) Directory.Delete(tempSourceDir, true);
            if (Directory.Exists(tempDestDir)) Directory.Delete(tempDestDir, true);
        }
    }
}
