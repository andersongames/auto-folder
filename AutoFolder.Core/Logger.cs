using System;
using System.IO;

namespace AutoFolder.Core;

/// <summary>
/// Simple file-based logger that records application events and errors.
/// </summary>
public static class Logger
{
    private static readonly string LogFilleName = "autofolder.log";

    /// <summary>
    /// Appends a timestamped log message to the log file.
    /// </summary>
    public static void Log(string message, bool alsoPrintToConsole = false)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string line = $"[{timestamp}] {message}";

        try
        {
            // Resolve the real directory of the executable (stable in single-file publish)
            string exeDirectory = Path.GetDirectoryName(Environment.ProcessPath!)!;
            string LogFillePath = Path.Combine(exeDirectory, LogFilleName);

            File.AppendAllText(LogFillePath, line + Environment.NewLine);
        }
        catch
        {
            // If logging fails, silently ignore to avoid blocking the main process
        }

        if (alsoPrintToConsole)
        {
            Console.WriteLine(message);
        }
    }
}