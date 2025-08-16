# ✅ Lista de Cenários de Teste para FileOrganizer
## Testes Unitários:
### 1. 🔤 Encontrar o maior prefixo comum entre duas strings (GetCommonPrefix)
Compara duas strings e encontra o prefixo comum mais longo.

- [x] Compara as strings e retorna o prefixo comum:  
`report_final_2024 (Q1).docx`, `report_final_2024 (Q1).docx` → prefixo `report_final_2024 (Q`
- [x] É necessário um mínimo de 3 caracteres em comum para ser considerado o mesmo grupo:
`aaa.txt`, `aab.txt` → prefixos `aaa` e `aab`

### 📁 2. Agrupamento de arquivos (GroupFilesByPrefix)
Testa se arquivos com nomes semelhantes são agrupados corretamente.

- [x] Agrupar arquivos com prefixo comum:  
`video-ep01.mp4`, `video-ep02.mp4` → grupo `video`
- [x] Arquivos com nomes distintos vão para grupos separados:  
`intro.mp4`, `trailer.mp4` → grupos `intro` e `trailer`
- [x] Arquivos sem padrão numérico caem em grupos individuais (ou com nome completo)
- [x] Ignora extensão ao agrupar (usa apenas o nome do arquivo)

### 📝 3. Normalização de nomes (NormalizeGroupName)
Testa se o nome da pasta é limpo conforme esperado.

- [x] Remove espaços extras  
` My Folder ` → `my-folder`
- [x] Substitui espaços e underscores por traços  
`my_folder test` → `my-folder-test`
- [x] Remove símbolos indesejados  
`Proj@ct! V1` → `projct-v1`
- [x] Converte tudo para minúsculas  
`MyProject` → `myproject`
- [x] Casos combinados (espaço, símbolo, maiúscula, underline)  
` Série_01 (Completa)` → `srie-01-completa`

## Testes de Integração:
### 1. 🔤 Encontrar o maior prefixo comum entre duas strings (GetCommonPrefix)
Compara duas strings e encontra o prefixo comum mais longo.

- [x] Compara as strings e retorna o prefixo comum:  
`report_final_2024 (Q1).docx`, `report_final_2024 (Q1).docx` → prefixo `report_final_2024 (Q`
- [x] É necessário um mínimo de 3 caracteres em comum para ser considerado o mesmo grupo:
`aaa.txt`, `aab.txt` → prefixos `aaa` e `aab`

### 📁 2. Agrupamento de arquivos (GroupFilesByPrefix)
Testa se arquivos com nomes semelhantes são agrupados corretamente.

- [x] Agrupar arquivos com prefixo comum:  
`report_final_2024 (Q1).docx`, `report_final_2024 (Q2).docx` → grupo `report_final_2024 (Q`
- [x] Arquivos com nomes distintos vão para grupos separados:  
`data1.csv`, `slide.mp4` → grupos `data1` e `slide`
- [x] Ignora extensão ao agrupar (usa apenas o nome do arquivo)
`data1.csv`, `data2.pdf` → grupo `data`

### 📝 3. Normalização de nomes (NormalizeGroupName)
Testa se o nome da pasta é limpo conforme esperado.

- [x] Remove espaços extras  
- [x] Substitui espaços e underscores por traços  
- [x] Remove símbolos indesejados  
- [x] Converte tudo para minúsculas  
` report_final_2024 (Q1).docx`, `report_final_2024 (Q2).docx` → `report-final-2024q`

### 🧪 4. Destination Directory

- [x] Files are organized into provided destination directory (if given)
- [x] Fallback to source directory if no destination is given
- [x] Create destination directory if it does not exist (optional logic)

### 📄 5. Filtro por extensão (Organize)
Testa se apenas arquivos com a extensão desejada são processados.

- [x] Se extensão for .docx, arquivos .pdf e .mp4 e outros são ignorados
- [x] Se nenhuma extensão for passada, todos os arquivos são considerados

### 🚫 6. Modo dry-run (Organize)
Garante que no modo simulação:

- [x] Nenhum arquivo é copiado ou deletado

### 💥7. Tratamento de erros por arquivo (Organize)
Garante que erros em um arquivo:

- [x] Não interrompem o processamento dos demais

### 🗑️ 8.  Exclusão de arquivos originais
Testa se os arquivos originais são excluídos apos o processamento:

- [x] Se selecionada a opção excluír arquivos originais, eles devem ser excluídos apos o processamento
- [x] Se nao selecionada a opção os arquivos devem ser mantidos
