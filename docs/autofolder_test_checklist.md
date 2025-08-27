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

### 📂 4. Path validation  (IsPathSyntacticallyValid)
Tests if the given path is being validated correctly.

**1. General cases:**
- [x] Returns **false** if path is empty string `""` .
- [x] Returns **false** if the path is all whitespace `" "`.
- [x] Returns **true** for a valid, simple directory path `"C:\\Users\\Test"`.
- [x] Returns **true** for a valid relative path `"..\\folder\\file.txt"`.

**2. Invalid characters:**
- [x] Returns **false** if the path contains any invalid character. `Path.GetInvalidPathChars()` (ex: `"C:\\Inva|id\\path"`).
- [x] Returns **false** for disallowed special characters `"C:\\Test<Folder>"`.

**3. Invalid characters in file name:**
- [x] Returns **false** if the file name contains invalid characters `"C:\\Test\\file?.txt"`.
- [x] Returns **true** if the file name is valid `"C:\\Test\\file.txt"`.

**4. Edge and special cases:**
- [x] Returns **true** if the path points to only a drive `"C:\\"`.
- [x] Returns **true** for a valid UNC path `"\\\\server\\share\\folder"`.
- [x] Returns **false** for an invalid UNC path `"\\\\server\\sh|are\\folder"`.
- [x] Returns **true** for valid long names within the limit `"C:\\folder\\subfolder\\file123456789.txt"`.
- [x] Returns **true** for paths with valid spaces `"C:\\My Documents\\file.txt"`.

**5. No existence verification:**
- [x] Returns **true** even if the directory does not exist  `"C:\\NonExistent\\folder"`, as long as the path is syntactically valid.
- [x] Returns **true** even if the file does not exist `"C:\\folder\\ghost.txt"`, if there are no invalid characters.

### 📄 4. File extenssion normalization (NormalizeFileExtension)
Tests if the file extension is being normalized as expected.

**1. Valid entries without a starting point:**
- [x] Returns `.txt` when input is `txt`.
- [x] Returns `.pdf` when input is `pdf`.
- [x] Returns `.docx` when input is `docx`.

**2. Entradas válidas já com ponto inicial**
- [x] Returns `.txt` when input is `.txt`.
- [x] Returns `.config` when input is `.config`.
- [x] Returns `.PDF` (keeps upper/lower case) when input is `.PDF`.

**3. Tratamento de espaços**
- [x] Returns `.txt` when input is  ` txt ` (with spaces before/after).
- [x] Returns `.png` when input is  ` png ` (with spaces).

**4. Casos especiais**
- [x] Returns `.` when input is  `.` (just one dot).
- [x] Returns `..` when input is  `..`.
- [x] Returns `.hiddenfile` when input is  `hiddenfile` (interpretation as a valid extension).

**5. Entradas vazias ou inválidas**
- [x] Throws exception or returns `.` when input is `""`.
- [x] Throws exception or returns `.` if the input is only spaces (` `).

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

- [ ] Validate if provided directoryiis valid
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