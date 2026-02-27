namespace AutoFolder.Core.Models;

/// <summary>
/// Represents persisted user preferences.
/// This class must remain infrastructure-agnostic (no file system logic here).
/// It is only a data container shared between layers.
/// </summary>
public class UserSettings
{
    /// <summary>
    /// Last used source directory.
    /// </summary>
    public string? SourceDirectory { get; set; }

    /// <summary>
    /// Last used destination directory.
    /// </summary>
    public string? DestinationDirectory { get; set; }

    /// <summary>
    /// Last used extension filter (e.g. ".mp4").
    /// </summary>
    public string? ExtensionFilter { get; set; }

    /// <summary>
    /// Whether originals should be deleted after copy.
    /// </summary>
    public bool DeleteOriginals { get; set; }

    /// <summary>
    /// Whether group names should be normalized.
    /// </summary>
    public bool NormalizeGroupNames { get; set; }

    /// <summary>
    /// Whether the last execution was in dry-run mode.
    /// </summary>
    public bool DryRun { get; set; }

    /// <summary>
    /// Whether to includdee subdirectoriees.
    /// </summary>
    public bool IncludeSubdirectories { get; set; }

    /// <summary>
    /// Returns default settings when no persisted file exists.
    /// Centralizing defaults avoids spreading magic values in the UI.
    /// </summary>
    public static UserSettings CreateDefault() => new()
    {
        SourceDirectory = null,
        DestinationDirectory = null,
        ExtensionFilter = null,
        DeleteOriginals = false,
        NormalizeGroupNames = true,
        DryRun = false,
        IncludeSubdirectories = false,
    };
}
