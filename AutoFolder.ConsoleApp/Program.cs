using System.Text;
using AutoFolder.Core;

class Program
{
  static void Main()
  {
    // Set console output encoding to UTF-8 for emoji support
    Console.OutputEncoding = Encoding.UTF8;

    Console.WriteLine("==== Auto Folder Console ====");
    Console.WriteLine();

    // Request the source directory path from the user
    string sourceDirectory = AskForSourceDirectory();

    // Request the destination directory path from the user
    string? destinationDirectory = AskForDestinationDirectory();

    // Ask for an optional extension filter (e.g., .mp4 or .pdf)
    Console.WriteLine("Enter the file extension to filter (or leave blank for all files): ");
    string? extension = Console.ReadLine()?.Trim().ToLower();

    // Ask whether to delete the original files after organizing
    Console.WriteLine("Delete original files after copy? (y/n): ");
    bool deleteOriginals = Console.ReadLine()?.Trim().ToLower() == "y";

    // Ask for normalize folder names
    Console.Write("Normalize group folder names? (remove spaces/symbols, use lowercase) (y/n): ");
    bool normalizeGroupNames = Console.ReadLine()?.Trim().ToLower() == "y";

    // Ask for dry-run mode
    Console.Write("Simulate actions only (dry-run mode)? (y/n): ");
    bool dryRun = Console.ReadLine()?.Trim().ToLower() == "y";

    Console.WriteLine();
    Console.WriteLine(dryRun ? "Starting dry-run simulation..." : "Starting file organization...");

    try
    {
      var organizer = new FileOrganizer();

      // First execution (could be dry-run or real)
      organizer.Organize(sourceDirectory, destinationDirectory, extension, deleteOriginals, normalizeGroupNames, dryRun);

      Console.WriteLine();

      // If it was a dry-run, ask if user wants to run the operation for real
      if (dryRun)
      {
        // Inform the user that no actual file operations were performed (if dry-run mode)
        Console.WriteLine("🔍 Dry-run complete. No files were copied or deleted.");
        Console.WriteLine();

        // Ask the user if he wants to perform the operations for real
        Console.Write("Execute now for real using the same options? (y/n): ");
        bool confirmRealRun = Console.ReadLine()?.Trim().ToLower() == "y";

        if (confirmRealRun)
        {
          Console.WriteLine();
          Console.WriteLine("Executing for real...");
          organizer.Organize(sourceDirectory, destinationDirectory, extension, deleteOriginals, normalizeGroupNames, dryRun = false);
          Console.WriteLine();
          Console.WriteLine("✅ File organization completed successfully!");
        }
        else
        {
          Console.WriteLine();
          Console.WriteLine("🚫 Operation cancelled by user.");
        }
      }
      else
      {
        Console.WriteLine();
        Console.WriteLine("✅ File organization completed successfully!");
      }
    }
    catch (Exception ex)
    {
      // If something unexpected happens, show the error message
      Console.WriteLine($"❌ Error: {ex.Message}");
    }
  }

  /// <summary>
  /// Asks the user to input a directory path and validates its existence.
  /// Keeps prompting until a valid directory is entered.
  /// </summary>
  /// <returns>Absolute path to the valid source directory</returns>
  static string AskForSourceDirectory()
  {
    while (true)
    {
      Console.Write("Enter the path to the source directory: ");
      string? input = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(input))
      {
        Console.WriteLine("⚠️  Directory path cannot be empty.");
        continue;
      }

      string path = input.Trim().Trim('"');

      // Check if the provided directory actually exists
      if (Directory.Exists(path))
      {
        return path;
      }
      else
      {
        Console.WriteLine("❌ Directory not found. Please try again.");
      }
    }
  }
  
  /// <summary>
  /// Asks the user to input a directory path and validates its existence.
  /// Keeps prompting until a valid directory is entered.
  /// </summary>
  /// <returns>Absolute path to the valid destination directory</returns>
  static string? AskForDestinationDirectory()
  {
    while (true)
    {
      Console.Write("Enter the path to the destination directory (or leave blank to use the source directory): ");
      string? input = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(input))
      {
        return null;
      }

      string path = input.Trim().Trim('"');

      // Check if the provided directory actually exists
      if (Directory.Exists(path))
      {
        return path;
      }
      else
      {
        Console.WriteLine("❌ Directory not found. Please try again.");
      }
    }
  }
}