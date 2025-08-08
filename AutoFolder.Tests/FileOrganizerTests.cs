using AutoFolder.Core;

namespace AutoFolder.Tests;

/// <summary>
/// Integration tests for the FileOrganizer class, specifically the organize method.
/// </summary>
public class FileOrganizerTests
{
    // Region: Destination directory

    /// <summary>
    /// Test case: Files should be organized in the given destination directory.
    /// </summary>
    [Fact]
    public void Organize_ShouldPlaceFilesInDestinationDirectory_WhenProvided()
    {
        // Arrange
        string tempSourceDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        string tempDestDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        Directory.CreateDirectory(tempSourceDir);
        Directory.CreateDirectory(tempDestDir);

        try
        {
            string[] testFiles =
            {
                "report_q1.docx",
                "report_q2.docx",
                "summary.pdf"
            };

            foreach (var file in testFiles)
            {
                File.WriteAllText(Path.Combine(tempSourceDir, file), "sample");
            }

            var organizer = new FileOrganizer();

            // Act
            organizer.Organize(
                sourceDirectory: tempSourceDir,
                destinationDirectory: tempDestDir,
                extensionFilter: ".docx",
                deleteOriginals: false,
                normalizeGroupNames: false,
                dryRun: false
            );

            // Assert: original files must still be in source
            Assert.Equal(testFiles.Length, Directory.GetFiles(tempSourceDir).Length);

            // Assert: only .docx files were copied to destination
            var destFiles = Directory.GetFiles(tempDestDir, "*", SearchOption.AllDirectories);
            Assert.All(destFiles, f => Assert.EndsWith(".docx", f));
            Assert.Equal(2, destFiles.Length);

            // Assert: destination folders created correctly
            Assert.Contains(destFiles, f => Path.GetFileName(f).Contains("report_q1"));
        }
        finally
        {
            Directory.Delete(tempSourceDir, true);
            Directory.Delete(tempDestDir, true);
        }
    }

    /// <summary>
    /// Test case: Non-existent destination directory must be created.
    /// </summary>
    [Fact]
    public void Organize_ShouldCreateDestinationDirectory_IfItDoesNotExist()
    {
        // Arrange
        string tempSourceDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        string tempDestDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        Directory.CreateDirectory(tempSourceDir);

        string testFile = "notes.txt";
        File.WriteAllText(Path.Combine(tempSourceDir, testFile), "notes...");

        var organizer = new FileOrganizer();

        // Act
        organizer.Organize(
            sourceDirectory: tempSourceDir,
            destinationDirectory: tempDestDir,
            extensionFilter: null,
            deleteOriginals: false,
            normalizeGroupNames: false,
            dryRun: false
        );

        // Assert
        Assert.True(Directory.Exists(tempDestDir));
        string[] copied = Directory.GetFiles(tempDestDir, "*", SearchOption.AllDirectories);
        Assert.Single(copied);
        Assert.EndsWith("notes.txt", copied[0]);

        // Cleanup
        Directory.Delete(tempSourceDir, true);
        Directory.Delete(tempDestDir, true);
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
            organizer.Organize(tempSourceDir, null, ".pdf", false, false, false);

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
            organizer.Organize(tempSourceDir, null, null, false, false, false);

            // Derive programmatically the group names
            var groups = organizer.GroupFilesByPrefix(Directory.GetFiles(tempSourceDir));

            // Assert: all files should  be processed
            int totalCopied = groups
                .Select(g => Directory.GetFiles(Path.Combine(tempSourceDir, g.Key)).Length)
                .Sum();
            Assert.Equal(testFiles.Length, totalCopied);

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

    // Region: Deleting original files
    /// <summary>
    /// Test case: If the delete original files option is selected, they must be deleted after processing.
    /// </summary>
    [Fact]
    public void Organize_ShouldDeleteOriginalFiles_WhenDeleteOriginalsIsTrue()
    {
        // Arrange
        string tempSourceDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempSourceDir);

        string[] testFiles =
        {
            "alpha_test1.txt",
            "alpha_test2.txt",
            "beta_data.csv"
        };

        foreach (var file in testFiles)
        {
            File.WriteAllText(Path.Combine(tempSourceDir, file), "mock data");
        }

        var organizer = new FileOrganizer();

        // Act: deleteOriginals = true
        organizer.Organize(
            sourceDirectory: tempSourceDir,
            destinationDirectory: null,
            extensionFilter: null,
            deleteOriginals: true,
            normalizeGroupNames: false,
            dryRun: false
        );

        // Assert: original files must no longer exist in the source directory
        foreach (var file in testFiles)
        {
            string filePath = Path.Combine(tempSourceDir, file);
            Assert.False(File.Exists(filePath), $"Expected file '{file}' to be deleted from source directory.");
        }

        // Assert: copied files must exist in the group folders
        var groupDirs = Directory.GetDirectories(tempSourceDir);
        Assert.True(groupDirs.Length >= 1, "Expected at least one group directory to be created.");

        int totalCopied = groupDirs.Sum(dir => Directory.GetFiles(dir).Length);
        Assert.Equal(testFiles.Length, totalCopied);

        // Cleanup
        Directory.Delete(tempSourceDir, true);
    }    

    // Region: Dry-run behavior
    [Fact]
    public void DryRunShouldNotModifyFiles()
    {

    }
}
