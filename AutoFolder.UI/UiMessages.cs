namespace AutoFolder.UI
{
    /// <summary>
    /// Centralized UI messages used across the application.
    /// Keeping them in one place improves maintainability
    /// and allows future localization support.
    /// </summary>
    internal static class UiMessages
    {
        public const string InvalidSource = "Please select a valid source directory.";
        public const string InvalidDestination = "Please enter a valid folder path (or leave it empty).";
        public const string Starting = "Starting organization...";
        public const string Completed = "Organization completed.";
    }
}