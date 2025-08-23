# ✅ Roadmap - AutoFolder.UI (WinForms)

## 🔹 Initial Infrastructure
- [x] Create **WinForms** project named `AutoFolder.UI` and add it to the solution.
- [x] Add reference to the **AutoFolder.Core** project.
- [x] Configure namespace and folder conventions (Forms, Services, etc.).

## 🔹 Basic Interface (Main Form)
- [x] Add field for **source directory** selection (TextBox + "Browse" Button).
- [x] Add optional field for **destination directory** (TextBox + "Browse" Button).
- [x] Add optional field for **filter extension** (TextBox, e.g., `.mp4`).
- [x] Add **checkboxes** for options:
  - [x] "Delete originals after copy"
  - [x] "Normalize group names"
  - [x] "Dry-run (simulation only)"
- [x] Add **Run** button to execute the organization.

## 🔹 User Experience
- [x] Add **progress bar**.
- [x] Add **logs/status** area (Multiline TextBox or ListBox).
- [x] Display **MessageBox** in case of critical errors.

## 🔹 Core Integration
- [x] Connect UI with `FileOrganizer.Organize()`.
- [x] Redirect `Logger` messages also to the interface (in addition to file/console).
- [x] Validate user inputs (directories exist, valid extension, etc.).

## 🔹 Quality and Refinement
- [x] Handle unforeseen exceptions (show user-friendly error).
- [ ] Add **icon** and name the Form "AutoFolder".
- [ ] Test in real scenarios (different extensions, dry-run, etc.).
- [ ] Package standalone release with `dotnet publish`.

## 🔹 (Optional / Future)
- [x] Improve layout with TableLayoutPanel or FlowLayoutPanel.
- [ ] Add "Settings" menu for preferences.
- [ ] Create an installer (MSIX or Setup).
- [ ] Update the README.md to include the AutoFolder.UI project