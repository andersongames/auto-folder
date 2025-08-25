namespace AutoFolder.UI.Resources
{
    /// <summary>
    /// Centralized UI messages used across the application.
    /// Keeping them in one place improves maintainability
    /// and allows future localization support.
    /// </summary>
    internal static class UiMessages
    {
        public const string InvalidSource = "Please select a valid source directory.";
        public const string InvalidDestination = "Please enter a valid destination directory (or leave it empty).";
        public const string Starting = "Starting organization...";
        public const string OrganizationCompleted = "Organization complete. Check the log for details.";
        public const string SimulationCompleted = "Simulation completed. Check the log for details.";
        public const string OperationCanceled = "Operation canceled by user.";
        public const string DestinationNotFound = "Destination directory not found. Do you want to create it?";
        public const string DirectoryNotFound = "Directory not found";
        public const string DirectorySelection = "Select a folder";
        public const string Status = "Status:";
        public const string CopyStage = "Copying:";
        public const string DeleteStage = "Deleting:";
    }
}