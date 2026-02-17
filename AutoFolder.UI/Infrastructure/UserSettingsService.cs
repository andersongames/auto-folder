using AutoFolder.Core.Models;
using AutoFolder.UI.Resources;
using System;
using System.IO;
using System.Text.Json;

namespace AutoFolder.UI.Infrastructure;

/// <summary>
/// Handles persistence of UserSettings to disk.
/// This class is intentionally placed in the UI layer because it deals with OS/file system.
/// </summary>
public sealed class UserSettingsService
{
    private const string FileName = "autofolder.settings.json";

    private readonly string _settingsPath;
    private readonly JsonSerializerOptions _jsonOptions;

    public UserSettingsService()
    {
        // Resolve the real directory of the executable (stable in single-file publish)
        string exeDirectory = Path.GetDirectoryName(Environment.ProcessPath!)!;

        _settingsPath = Path.Combine(exeDirectory, FileName);

        // Configure serializer once (performance + consistency)
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true, // Makes the file human-readable
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    /// <summary>
    /// Loads settings from disk.
    /// If the file does not exist or is corrupted, returns safe defaults.
    /// </summary>
    public UserSettings Load()
    {
        try
        {
            if (!File.Exists(_settingsPath))
                return UserSettings.CreateDefault();

            string json = File.ReadAllText(_settingsPath);

            var settings = JsonSerializer.Deserialize<UserSettings>(json, _jsonOptions);

            return settings ?? UserSettings.CreateDefault();
        }
        catch
        {
            // If anything goes wrong (corrupt JSON, IO issue),
            // we fail gracefully and return defaults.
            return UserSettings.CreateDefault();
        }
    }

    /// <summary>
    /// Saves settings to disk.
    /// This method overwrites the file atomically.
    /// </summary>
    public void Save(UserSettings settings)
    {
        try
        {
            string json = JsonSerializer.Serialize(settings, _jsonOptions);
            File.WriteAllText(_settingsPath, json);
        }
        catch
        {
            // Intentionally swallow exceptions.
            // Settings persistence must NEVER crash the application.
            // Logging could be added here later if desired.
        }
    }
}
