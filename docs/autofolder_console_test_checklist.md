# ✅ Test Scenario Checklist for FileOrganizer
## Unit Tests:
### 1. 🔤 Find the longest common prefix between two strings (GetCommonPrefix)
Compares two strings and finds the longest common prefix.

- [x] Compares the strings and returns the common prefix:
`report_final_2024 (Q1).docx`, `report_final_2024 (Q1).docx` → prefix `report_final_2024 (Q`
- [x] A minimum of 3 common characters is required to be considered the same group:
`aaa.txt`, `aab.txt` → prefixes `aaa` and `aab`

### 📁 2. File grouping (GroupFilesByPrefix)
Tests if files with similar names are grouped correctly.

- [x] Group files with a common prefix:
`video-ep01.mp4`, `video-ep02.mp4` → group `video`
- [x] Files with distinct names go into separate groups:
`intro.mp4`, `trailer.mp4` → groups `intro` and `trailer`
- [x] Files without a numerical pattern fall into individual groups (or with full name)
- [x] Ignores extension when grouping (uses only the filename)

### 📝 3. Name normalization (NormalizeGroupName)
Tests if the folder name is cleaned as expected.

- [x] Removes extra spaces
` My Folder ` → `my-folder`
- [x] Replaces spaces and underscores with hyphens
`my_folder test` → `my-folder-test`
- [x] Removes unwanted symbols
`Proj@ct! V1` → `projct-v1`
- [x] Converts everything to lowercase
`MyProject` → `myproject`
- [x] Combined cases (space, symbol, uppercase, underline)
` Série_01 (Completa)` → `srie-01-completa`

## Integration Tests:
### 1. 🔤 Find the longest common prefix between two strings (GetCommonPrefix)
Compares two strings and finds the longest common prefix.

- [x] Compares the strings and returns the common prefix:
`report_final_2024 (Q1).docx`, `report_final_2024 (Q1).docx` → prefix `report_final_2024 (Q`
- [x] A minimum of 3 common characters is required to be considered the same group:
`aaa.txt`, `aab.txt` → prefixes `aaa` and `aab`

### 📁 2. File grouping (GroupFilesByPrefix)
Tests if files with similar names are grouped correctly.

- [x] Group files with a common prefix:
`report_final_2024 (Q1).docx`, `report_final_2024 (Q2).docx` → group `report_final_2024 (Q`
- [x] Files with distinct names go into separate groups:
`data1.csv`, `slide.mp4` → groups `data1` and `slide`
- [x] Ignores extension when grouping (uses only the filename)
`data1.csv`, `data2.pdf` → group `data`

### 📝 3. Name normalization (NormalizeGroupName)
Tests if the folder name is cleaned as expected.

- [x] Removes extra spaces
- [x] Replaces spaces and underscores with hyphens
- [x] Removes unwanted symbols
- [x] Converts everything to lowercase
` report_final_2024 (Q1).docx`, `report_final_2024 (Q2).docx` → `report-final-2024q`

### 🧪 4. Destination Directory

- [x] Files are organized into provided destination directory (if given)
- [x] Fallback to source directory if no destination is given
- [x] Create destination directory if it does not exist (optional logic)

### 📄 5. Filter by extension (Organize)
Tests if only files with the desired extension are processed.

- [x] If extension is .docx, .pdf and .mp4 files and others are ignored
- [x] If no extension is passed, all files are considered

### 🚫 6. Dry-run mode (Organize)
Ensures that in simulation mode:

- [x] No files are copied or deleted

### 💥7. Error handling per file (Organize)
Ensures that errors in a file:

- [x] Do not interrupt the processing of others

### 🗑️ 8. Deletion of original files
Tests if the original files are deleted after processing:

- [x] If the option to delete original files is selected, they must be deleted after processing
- [x] If the option is not selected, the files must be kept