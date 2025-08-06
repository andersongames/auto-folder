## ✅ Lista de Cenários de Teste para FileOrganizer
### 📁 1. Agrupamento de arquivos (GroupFilesByPrefix)
Testa se arquivos com nomes semelhantes são agrupados corretamente.

- [x] Agrupar arquivos com prefixo comum:  
`video-ep01.mp4`, `video-ep02.mp4` → grupo `video`
- [x] Arquivos com nomes distintos vão para grupos separados:  
`intro.mp4`, `trailer.mp4` → grupos `intro` e `trailer`
- [x] Arquivos sem padrão numérico caem em grupos individuais (ou com nome completo)
- [x] Ignora extensão ao agrupar (usa apenas o nome do arquivo)

### 🧪  2. Destination Directory

- [ ] Files are organized into provided destination directory (if given)
- [x] Fallback to source directory if no destination is given
- [ ] Prevent deletion of original files if source == destination
- [x] Create destination directory if it does not exist (optional logic)

### 📝 3. Normalização de nomes (NormalizeGroupName)
Testa se o nome da pasta é limpo conforme esperado.

- [x] Remove espaços extras  
` My Folder ` → `my-folder`
- [x] Substitui espaços e underscores por traços  
`my_folder test` → `my-folder-test`
- [x] Remove símbolos indesejados  
`Proj@ct! V1` → `projct-v1`
- [x] Normalization allows final allowed symbols (e.g. `!`, `+`, `)`)  
`Final Version!` → `final-version!`
- [x] Converte tudo para minúsculas  
`MyProject` → `myproject`
- [x] Casos combinados (espaço, símbolo, maiúscula, underline)  
` Série_01 (Completa)` → `srie-01-completa`

### 📄 4. Filtro por extensão (Organize)
Testa se apenas arquivos com a extensão desejada são processados.

- [x] Se extensão for .pdf, arquivos .docx e .mp4 são ignorados
- [x] Se nenhuma extensão for passada, todos os arquivos são considerados

### 🚫 5. Modo dry-run (Organize)
Garante que no modo simulação:

- [ ] Nenhum arquivo é copiado ou deletado
- [ ] Ações simuladas são reportadas corretamente

Esses testes geralmente requerem uso de arquivos reais ou mocks com File.Copy, File.Delete — podemos simular com arquivos temporários.

### 💥 6. Tratamento de erros por arquivo (Organize)
Garante que erros em um arquivo:

- [ ] Não interrompem o processamento dos demais
- [ ] São registrados corretamente no log ou saída

### 🗑️ 7.  Exclusão de arquivos originais
Testa se os arquivos originais são excluídos apos o processamento:

- [ ] Se selecionada a opção excluír arquivos originais, eles devem ser excluídos apos o processamento
- [ ] Se nao selecionada a opção os arquivos devem ser mantidos
