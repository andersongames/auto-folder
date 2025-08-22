using System;
using System.Windows.Forms;

namespace AutoFolder.UI.Infrastructure
{
    /// <summary>
    /// Helper to standardize how critical errors are displayed to the user.
    /// Encapsulates MessageBox logic in one place.
    /// </summary>
    internal static class ErrorDialog
    {
        /// <summary>
        /// Shows a critical error dialog with a standard title and icon.
        /// </summary>
        /// <param name="owner">Parent form (optional, may be null)</param>
        /// <param name="message">Error message to display</param>
        public static void ShowCritical(IWin32Window? owner, string message)
        {
            MessageBox.Show(
                owner,
                message,
                "AutoFolder - Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        /// <summary>
        /// Shows a warning dialog for validation or recoverable issues.
        /// </summary>
        public static void ShowWarning(IWin32Window? owner, string message)
        {
            MessageBox.Show(
                owner,
                message,
                "AutoFolder - Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
    }
}
