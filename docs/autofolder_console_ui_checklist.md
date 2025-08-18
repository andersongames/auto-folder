# ✅ Roadmap - AutoFolder.UI (WinForms)

## 🔹 Infraestrutura inicial
- [ ] Criar projeto **WinForms** chamado `AutoFolder.UI` e adicioná-lo à solution.
- [ ] Adicionar referência ao projeto **AutoFolder.Core**.
- [ ] Configurar namespace e convenções de pastas (Forms, Services, etc).

## 🔹 Interface básica (Form principal)
- [ ] Adicionar campo de seleção do **diretório de origem** (TextBox + Button "Browse").
- [ ] Adicionar campo opcional para **diretório de destino** (TextBox + Button "Browse").
- [ ] Adicionar campo opcional para **extensão de filtro** (TextBox, ex: `.mp4`).
- [ ] Adicionar **checkboxes** para opções:
  - [ ] "Delete originals after copy"
  - [ ] "Normalize group names"
  - [ ] "Dry-run (simulation only)"
- [ ] Adicionar botão **Run** para executar a organização.

## 🔹 Experiência do usuário
- [ ] Adicionar **barra de progresso**.
- [ ] Adicionar área de **logs/status** (TextBox multilinha ou ListBox).
- [ ] Exibir **MessageBox** em caso de erros críticos.

## 🔹 Integração com Core
- [ ] Conectar UI com `FileOrganizer.Organize()`.
- [ ] Redirecionar mensagens do `Logger` também para a interface (além de arquivo/console).
- [ ] Validar entradas do usuário (diretórios existem, extensão válida, etc).

## 🔹 Qualidade e refinamento
- [ ] Tratar exceções não previstas (mostrar erro amigável).
- [ ] Adicionar **ícone** e nomear o Form como "AutoFolder".
- [ ] Testar em cenários reais (diferentes extensões, dry-run, etc).
- [ ] Empacotar release standalone com `dotnet publish`.

## 🔹 (Opcional / Futuro)
- [ ] Melhorar layout com TableLayoutPanel ou FlowLayoutPanel.
- [ ] Adicionar menu "Settings" para preferências.
- [ ] Criar instalador (MSIX ou Setup).
