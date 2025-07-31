using AutoFolder.Core;

class Program
{
  static void Main()
  {
    Console.WriteLine("==== Auto Folder Console ====");
    Console.WriteLine();

    // Request the source directory path from the user
    string sourceDirectory = AskForSourceDirectory();

    // Ask for an optional extension filter (e.g., .mp4 or .pdf)
    Console.WriteLine("Enter the file extension to filter (or leave blank for all files): ");
    string? extension = Console.ReadLine()?.Trim().ToLower();

    // Ask whether to delete the original files after organizing
    Console.WriteLine("Delete original files after copy? (y/n): ");
    bool deleteOriginals = Console.ReadLine()?.Trim().ToLower() == "y";

    Console.WriteLine();
    Console.WriteLine("Starting file organization...");

    try
    {
      var organizer = new FileOrganizer();
      organizer.Organize(sourceDirectory, extension, deleteOriginals);

      Console.WriteLine();
      Console.WriteLine("✅ File organization completed successfully!");
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
}