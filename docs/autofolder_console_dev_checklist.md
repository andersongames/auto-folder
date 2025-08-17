### ✅ Development Checklist - AutoFolder Console

- [x] Ask for source directory with `Console.ReadLine()`
- [x] Check if directory exists with `Directory.Exists()`
- [x] Ask for optional extension for filtering (e.g., `.mp4`, `.pdf`, or empty for all)
- [x] List files with `Directory.GetFiles()`
- [x] Filter files by extension (if provided)
- [x] Group files by the longest common prefix in their names
- [x] Create folders for each group with `Directory.CreateDirectory()`
- [x] Copy files with `File.Copy()`
- [x] Ask if original files should be deleted
- [x] Delete original files with `File.Delete()`
- [x] Show status messages for each copied group
- [x] Handle I/O errors with `try/catch`
- [x] Test with different file types and names
- [x] Allow running in dry-run mode (simulation, no files are copied or deleted)
- [x] Allow selecting a destination directory