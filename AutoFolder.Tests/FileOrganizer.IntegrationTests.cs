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
        string tempDestDir = tempSourceDir;
        Directory.CreateDirectory(tempSourceDir);
        Directory.CreateDirectory(tempDestDir);

        try
        {
            string[] testFiles =
            {
                "data1.csv",
                "data2.pdf",
                "slide.pptx",
                "audio.mp3",
                "report_final_2024 (Q1).docx",
                "report_final_2024 (Q2).docx",
                "aaa.txt",
                "aab.txt",
            };

            var expectedGroups = new Dictionary<string, List<string>>()
            {
                {"data", new List<string>()},
                {"slide", new List<string>()},
                {"audio", new List<string>()},
                {"report_final_2024 (Q", new List<string>()},
                {"aaa", new List<string>()},
                {"aab", new List<string>()},
            };
            expectedGroups["data"].Add("data1.csv");
            expectedGroups["data"].Add("data2.pdf");
            expectedGroups["slide"].Add("slide.pptx");
            expectedGroups["audio"].Add("audio.mp3");
            expectedGroups["report_final_2024 (Q"].Add("report_final_2024 (Q1).docx");
            expectedGroups["report_final_2024 (Q"].Add("report_final_2024 (Q2).docx");
            expectedGroups["aaa"].Add("aaa.txt");
            expectedGroups["aab"].Add("aab.txt");

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

            string[] result = Directory.GetFileSystemEntries(tempDestDir);

            foreach (var group in expectedGroups)
            {
                string groupFolder = Path.Combine(tempDestDir, group.Key);

                // Assert: expected group
                Assert.True(Directory.Exists(groupFolder));

                // Assert: all files should have been copied
                string[] copiedFiles = Directory.GetFiles(groupFolder);
                string[] copiedFileNames = copiedFiles.Select(f => Path.GetFileName(f)).ToArray();
                string[] expectedFiles = group.Value.ToArray();
                Assert.Equal(expectedFiles, copiedFileNames);
            }

            foreach (var file in testFiles)
            {
                // Assert: the original files are not deleted
                string originalFilePath = Path.Combine(tempSourceDir, file);
                Assert.True(File.Exists(originalFilePath));
            }

            // Assert: all files should be processed, expect groups + orininal files
            Assert.Equal(expectedGroups.Count + testFiles.Length, result.Length);
        }
        finally
        {
            if (Directory.Exists(tempSourceDir)) Directory.Delete(tempSourceDir, true);
            if (Directory.Exists(tempDestDir)) Directory.Delete(tempDestDir, true);
        }
    }
}
