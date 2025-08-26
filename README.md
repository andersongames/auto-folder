# AutoFolder

AutoFolder is a utility tool written in **C# (.NET 8)** that helps organize files in a directory by grouping them into folders based on their filename patterns.  
It can optionally filter by extension, normalize folder names, delete original files, run in dry-run mode (simulation only), and log all operations.

---

## 📂 Projects in this Solution

The solution currently contains **3 projects**:

1. **AutoFolder.Core**  
   - Contains the main logic (`FileOrganizer` and helpers).  
   - Independent of UI, reusable in other apps.

2. **AutoFolder.ConsoleApp**  
   - Console interface for interacting with AutoFolder.  
   - Accepts user input (directory, extension, flags).  
   - Uses the core library internally.

3. **AutoFolder.UI**  
   - Windows Forms graphical interface.  
   - Provides a more user-friendly way to configure and run AutoFolder.  
   - Supports progress bar, cancellation, logging messages inside the UI, theming (light/dark/auto), and accessible menu options.

43. **AutoFolder.Tests**  
   - Unit and integration tests.  
   - Uses **xUnit** to validate all functionality.

---

## 📝 Example Use Case

Suppose the folder `C:\Downloads` contains:

```
report_final_2024 (Q1).docx
report_final_2024 (Q2).docx
resume_2024 (Q1).docx
resume_2024 (Q2).docx
notes.txt
```

After running AutoFolder with default settings:

- A folder `report_final_2024 (Q` is created and both reports are moved inside.  
- A folder `resume_2024 (Q` is created and both resumes are moved inside.  
- `notes.txt` is moved to folder `notes`.

Resulting structure:

```
C:\Downloads
  ├── report_final_2024 (Q)
  │     ├── report_final_2024 (Q1).docx
  │     └── report_final_2024 (Q2).docx
  ├── resume_2024 (Q)
  │     ├── resume_2024 (Q1).docx
  │     └── resume_2024 (Q2).docx
  └── notes
        └── notes.txt
```

---

## ⚙️ Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (for building and running locally).  
- Windows 10/11 (tested), but logic is cross-platform.  
- Git (for cloning the repository).  

For end-users, you can publish a **standalone executable** (no runtime needed).

---

## 🚀 Running Locally

Clone the repository:

```bash
git clone https://github.com/your-username/auto-folder.git
cd auto-folder
```

Restore dependencies:

```bash
dotnet restore
```

Run the console app:

```bash
dotnet run --project AutoFolder.ConsoleApp
```

Run tests:

```bash
dotnet test
```

---

## 📦 Publishing Standalone Executable

### ConsoleApp

To publish a self-contained `.exe` for Windows 10/11 (x64):

```bash
dotnet publish AutoFolder.ConsoleApp -c Release -r win10-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true /p:PublishTrimmed=true
```

The executable will be available at:

```bash
/AutoFolder.ConsoleApp/bin/Release/net6.0/win10-x64/publish/AutoFolder.ConsoleApp.exe
```

### AutoFolder.UI

To generate a standalone `.exe` for **AutoFolder.UI** (no installer, no dependency on .NET runtime):

```bash
dotnet publish AutoFolder.UI -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true
```

The executable will be available at:

```bash
AutoFolder.UI\bin\Release\net8.0-windows\win-x64\publish\AutoFolder.UI.exe
```

To generate a installer `.msix` for **AutoFolder.UI**:

```bash
dotnet publish AutoFolder.UI -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:GenerateAppxPackageOnBuild=true
```
---

## 🔧 Useful Commands

- Build solution:
  ```bash
  dotnet build
  ```

- Run console app:
  ```bash
  dotnet run --project AutoFolder.ConsoleApp
  ```

- Run all tests:
  ```bash
  dotnet test
  ```

- Run unit tests only:
  ```bash
  dotnet test --filter Category=Unit
  ```

- Run integration tests only:
  ```bash
  dotnet test --filter Category=Integration
  ```

- Clean build artifacts:
  ```bash
  dotnet clean
  ```

---

## 🧪 Testing

The solution uses **xUnit** for testing.  
Tests are divided into:

- **Unit Tests**: small, isolated (e.g., `GroupFilesByPrefix`, `NormalizeGroupName`).  
- **Integration Tests**: verify the end-to-end behavior of `FileOrganizer.Organize`.

---

## 📜 License

This project is licensed under the **MIT License**.  
You are free to use, modify, and distribute it with attribution.
