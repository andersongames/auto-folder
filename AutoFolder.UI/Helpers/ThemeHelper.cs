using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoFolder.UI.Helpers
{
    /// <summary>
    /// Provides utilities to detect and apply light/dark themes in WinForms.
    /// Supports both automatic system detection and manual override.
    /// </summary>
    public static class ThemeHelper
    {
        /// <summary>
        /// Defines available theme modes.
        /// </summary>
        public enum ThemeMode
        {
            Auto,   // Follows Windows theme
            Light,  // Always light
            Dark    // Always dark
        }

        /// <summary>
        /// Stores the current theme mode (default = Auto).
        /// </summary>
        public static ThemeMode CurrentThemeMode { get; private set; } = ThemeMode.Auto;

        /// <summary>
        /// Stores the current theme (light or dark).
        /// </summary>
        public static bool IsDarkMode { get; private set; } =
            (CurrentThemeMode == ThemeMode.Dark) ||
            (CurrentThemeMode == ThemeMode.Auto && IsSystemDarkMode());

        /// <summary>
        /// Determines if Windows is currently set to dark mode for apps.
        /// </summary>
        private static bool IsSystemDarkMode()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");

                if (key != null)
                {
                    object? value = key.GetValue("AppsUseLightTheme");
                    if (value is int intValue)
                    {
                        return intValue == 0; // 0 = Dark, 1 = Light
                    }
                }
            }
            catch
            {
                // Default to light mode if registry access fails
            }

            return false;
        }

        /// <summary>
        /// Applies the theme to the given form and all its child controls.
        /// </summary>
        public static void ApplyTheme(Form form, ThemeMode mode)
        {
            CurrentThemeMode = mode;

            bool useDark =
                (mode == ThemeMode.Dark) ||
                (mode == ThemeMode.Auto && IsSystemDarkMode());

            if (useDark)
            {
                ApplyColors(form, Color.FromArgb(45, 45, 48), Color.WhiteSmoke, useDark);
                IsDarkMode = true;
            }
            else
            {
                ApplyColors(form, SystemColors.Control, SystemColors.ControlText);
                IsDarkMode = false;
            }
        }

        /// <summary>
        /// Recursively applies colors to a control and its children.
        /// </summary>
        private static void ApplyColors(Control control, Color backColor, Color foreColor, bool darkMode = false)
        {
            if (control is Button button)
            {
                if (darkMode)
                {
                    // Dark mode button style
                    CustomizeButtonForDarkMode(button);
                }
                else
                {
                    // Reset to system default
                    button.FlatStyle = FlatStyle.Standard;
                    button.UseVisualStyleBackColor = true;
                    button.ForeColor = SystemColors.ControlText;
                    button.Paint -= null;
                }
            }
            else
            {
                control.BackColor = backColor;
                control.ForeColor = foreColor;
            }

            foreach (Control child in control.Controls)
            {
                ApplyColors(child, backColor, foreColor, darkMode);
            }
        }

        /// <summary>
        /// Configures a Button control for dark mode appearance by applying a flat style,
        /// custom background, foreground, and border colors. Also overrides the default 
        /// disabled rendering behavior to ensure the text remains visible by drawing it 
        /// with a custom gray color when the button is disabled.
        /// </summary>
        private static void CustomizeButtonForDarkMode(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.Gray;
            button.BackColor = Color.FromArgb(63, 63, 70);
            button.ForeColor = Color.WhiteSmoke;

            button.Paint += (s, e) =>
            {
                var btn = s != null ? (Button)s : null;

                // If disabled, draw custom text color
                if (btn != null && !btn.Enabled)
                {
                    TextRenderer.DrawText(
                        e.Graphics,
                        btn.Text,
                        btn.Font,
                        btn.ClientRectangle,
                        Color.Gray, // custom disabled text color
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                    );
                }
            };
        }

        /// <summary>
        /// Applies dark or light mode styling to a MenuStrip and its top-level items.
        /// Ensures proper text color so that items remain visible when selected.
        /// </summary>
        public static void ApplyMenuStripColors(MenuStrip menuStrip)
        {
            if (menuStrip == null) return;

            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                item.DropDownOpened += (_, __) => item.Checked = true;
                item.DropDownClosed += (_, __) => item.Checked = false;

                item.CheckedChanged += (_, __) =>
                {
                    if (IsDarkMode)
                    {
                        if (item.Checked)
                            item.ForeColor = SystemColors.ControlText;
                        else
                            item.ForeColor = Color.WhiteSmoke;
                    }
                };
            }
        }
    }
}
