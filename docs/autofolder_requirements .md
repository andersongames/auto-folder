
# 📄 Requirements Document

## 🧾 Project Name
**AutoFolder**

---

## 📝 Description

**AutoFolder** is a desktop application for Windows, developed in **C#** with a graphical user interface, which aims to **automatically organize files** based on patterns in their names. It identifies related files by prefix, groups them into dedicated folders, and can, optionally, delete the originals after a successful copy.

The user can choose to organize **files of a specific extension** (such as `.mp4`, `.pdf`, `.docx`) or **all files** in the source directory.

---

## 🎯 Main Objectives

1. Automate file organization by name.
2. Reduce the manual effort of grouping and moving files.
3. Provide an intuitive interface for common Windows 10 and 11 users.
4. Ensure the integrity of copied files before deleting the originals.

---

## 📌 Main Features

### 1. **File Reading**
- Identifies files with a user-defined extension (e.g., `.pdf`, `.docx`, `.mp4`), or all files if no extension is defined.
- Does not access subdirectories—only the root of the folder.

### 2. **Grouping by Name**
- Groups files based on a common prefix in the name.
- Detects multiple "collections" in the same directory.

### 3. **Folder Creation**
- Automatically creates a folder for each identified group.
- The folder can be created:
  - In the source directory, or
  - In a destination directory specified by the user.

### 4. **File Copying**
- Copies all files belonging to a collection to their respective folder.

### 5. **Deletion of Originals (optional)**
- If the user selects this option, the original files are deleted after a successful copy.

### 6. **User Interface**
- Graphical interface via Windows Forms or WPF.
- Components:
  - Source directory selector
  - Destination directory selector
  - Field to specify extension or "all extensions" option
  - Checkbox: "Delete original files after copy"
  - "Execute" button
  - Progress bar with visual and textual feedback

---

## 🖥️ Platform

- **Supported Operating System**:
  - Windows 10
  - Windows 11

- **Base Technology**:
  - **Language**: C#
  - **Framework**: .NET 6 or higher
  - **Graphical Interface**: Windows Forms (WinForms) or WPF

---

## 📂 Use Case Example

**Source Directory:**
`C:/downloads`

**Existing Files:**
```
project-alpha-part-01.docx
project-alpha-part-02.docx
project-alpha-part-03.docx
report-2023-q1.pdf
report-2023-q2.pdf
invoice-jan.pdf
invoice-feb.pdf
task-list-v1.txt
task-list-v2.txt
notes-summary-v1.txt 
```

**Expected Result:**
- Creation of folders:
  - `C:/downloads/project-alpha-part/`
  - `C:/downloads/report-2023-q/`
  - `C:/downloads/invoice/`
  - `C:/downloads/task-list-v/`
  - `C:/downloads/notes-summary-v/`
- Files copied to their respective folders.
- Original files removed if the option is activated.

---

## ✅ Functional Requirements (FR)

- **FR01**: Allow the user to select the source directory.
- **FR02**: Allow the user to choose a file extension or opt to include all.
- **FR03**: Identify name patterns and group files with a common prefix.
- **FR04**: Create a folder corresponding to each group.
- **FR05**: Copy grouped files to their respective folders.
- **FR06**: Allow the user to select whether to delete original files.
- **FR07**: Display a progress bar with percentage and status.
- **FR08**: Handle copy failures without deleting original files.

---

## 🔒 Non-Functional Requirements (NFR)

- **NFR01**: The application must function without an internet connection.
- **NFR02**: The application must operate only in a Windows environment.
- **NFR03**: Responsive and accessible interface for resolutions starting at 1366x768.
- **NFR04**: Clear error messages must be displayed in case of permission issues, full disk, or copy failure.

---

## 📅 Development Stages

1. Setup of the C# project with interface
2. File reading and grouping
3. Copying and folder structuring
4. Progress bar and visual feedback
5. Deletion logic with validation
6. Input and error validation
7. Functional tests
8. Distribution (compiler + .exe packager)
9. Icon and splash screen design
10. Publication
