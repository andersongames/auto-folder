using System;
using System.IO;

namespace AutoFolder.Core;

/// <summary>
/// Simple file-based logger that records application events and errors.
/// </summary>
public static class Logger
{
    private static readonly string LogFillePath = "autofolder.log";

    /// <summary>
    /// Appends a timestamped log message to the log file.
    /// </summary>
    public static void Log(string message, bool alsoPrintToConsole = false)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string line = $"[{timestamp}] {message}";

        try
        {
            File.AppendAllText(LogFillePath, line + Environment.NewLine);
        }
        catch
        {
            // If logging fails, silently ignore to avoid blocking the main process
        }

        if (alsoPrintToConsole)
        {
            Console.WriteLine(line);
        }
    }
}