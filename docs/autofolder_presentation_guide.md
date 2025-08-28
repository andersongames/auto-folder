# AutoFolder Project

## 1. 📄 Project Summary / Objective

**AutoFolder** is a Windows-based utility (with both **Console** and **WinForms UI**) designed to automatically organize files into subdirectories based on common filename prefixes. The project’s primary goal is to provide users with a simple, efficient, and user-friendly way to declutter directories by grouping related files without manual effort.

The solution currently consists of three main projects:
- **AutoFolder.Core** – Business logic, file organization, helpers, and utilities.
- **AutoFolder.ConsoleApp** – Console-based interface for quick usage.
- **AutoFolder.UI** – Windows Forms application with progress bar, theming (light/dark/auto), menu options, error dialogs, and cancellation support.
- **AutoFolder.Package** – Packaging project to generate MSIX installers for distribution.

Key features:
- File grouping by prefix.
- Configurable destination directory.
- Dry-run mode for previewing results.
- Logging to both file/console and UI.
- Progress bar for feedback.
- Cancellation of running operations.
- Error handling with user-friendly messages.
- Light/Dark mode with auto-detection.
- Menu strip for navigation and settings.

---

## 2. 🔮 Guide for Presenting the Project

This walkthrough can be used to demonstrate the current functionality of AutoFolder:

### ⚙️ Core Functionality
- Select a **source directory** and optionally a **destination directory**.
- Run the **organizer** to group files by prefix.
- If the destination folder does not exist, prompt to create it (with validation).
- Observe how files are moved into new subfolders.
- Use **dry-run mode** to simulate without affecting the filesystem.

### 🖥️ Console Application
- Start the Console App.
- Demonstrate input prompts (source/destination).
- Show validation for invalid paths.
- Display logs and final report.

### 🎨 Windows Forms Application (UI)
- Launch the UI.
- Show:
  - Source and destination input fields.
  - Progress bar updating during execution.
  - Ability to cancel the operation.
  - Error handling via **MessageBox** dialogs.
  - Logging messages displayed inside the UI.
- Open the **menu strip** to demonstrate:
  - Closing the application.
  - Switching between light, dark, and auto theme.
  - "About" dialog with program details.

### 📦 Installer
- Present the **MSIX packaging** project.
- Highlight how the app can be published as a standalone `.exe` or as an installer.
- Mention DPI-awareness and icon customization.

---

## 3. 📝 Technical Evaluation (Senior Developer Perspective)

### ✅ Strengths
- **Separation of concerns**: clear division between Core logic, Console UI, WinForms UI, and Packaging.
- **Reusability**: Core library encapsulates file organization logic, allowing multiple frontends.
- **Robust validation**: helper methods like `IsPathSyntacticallyValid` ensure safe input handling.
- **Error handling**: consistent user-friendly messages with option for detailed error dialogs.
- **User experience**: progress bar, cancellation support, theming, and accessible menu strip improve usability.
- **Extensibility**: architecture supports future addition of more UI frontends or CLI options.
- **Distribution-ready**: MSIX packaging allows professional installation and deployment.

### ⚠️ Opportunities for Improvement
- Current UI is functional but could be modernized (e.g., WPF or MAUI) for better scalability and accessibility.
- Logging could be expanded with log levels (info, warning, error).
- Dry-run output could be visualized in the UI for better previewing.
- Internationalization (i18n) support could be added for multilingual usage.

### 💬 Conclusion
> AutoFolder demonstrates strong engineering practices: clear layering, robust input validation, and thoughtful UX improvements. The balance between console simplicity and a more polished Windows Forms interface makes it flexible for different audiences. With further improvements in testing, modernization of the UI, and advanced logging/reporting, AutoFolder can become a solid and professional-grade utility.

