
# 📄 Documento de Desenvolvimento: AutoFolder

## 🧾 Nome do Projeto
**AutoFolder**

---

## 📝 Descrição Geral

**AutoFolder** é uma aplicação desktop para Windows desenvolvida em **C#** com interface gráfica, cujo objetivo é **organizar automaticamente arquivos** com base em padrões nos nomes dos arquivos. Ele identifica arquivos relacionados por prefixo, agrupa em pastas dedicadas e pode, opcionalmente, apagar os originais após a cópia bem-sucedida.

O usuário pode escolher organizar **arquivos de uma extensão específica** (como `.mp4`, `.pdf`, `.docx`) ou **todos os arquivos** no diretório de origem.

---

## 🎯 Objetivos Principais

1. Automatizar a organização de arquivos por nome.
2. Reduzir o esforço manual de agrupamento e movimentação de arquivos.
3. Proporcionar uma interface intuitiva para usuários comuns de Windows 10 e 11.
4. Garantir a integridade dos arquivos copiados antes de excluir os originais.

---

## 📌 Funcionalidades Principais

### 1. **Leitura de Arquivos**
- Identifica arquivos com uma extensão definida pelo usuário (ex: `.pdf`, `.docx`, `.mp4`), ou todos os arquivos se nenhuma extensão for definida.
- Não acessa subdiretórios — apenas a raiz da pasta.

### 2. **Agrupamento por Nome**
- Agrupa arquivos com base em um prefixo comum no nome (antes de um padrão numérico ou identificador).
- Detecta múltiplas "coleções" no mesmo diretório.

### 3. **Criação de Pastas**
- Cria automaticamente uma pasta para cada grupo identificado.
- A pasta pode ser criada:
  - No diretório de origem, ou
  - Em um diretório de destino especificado pelo usuário.

### 4. **Cópia de Arquivos**
- Copia todos os arquivos pertencentes a uma coleção para sua respectiva pasta.

### 5. **Exclusão de Originais (opcional)**
- Caso o usuário selecione essa opção, os arquivos originais são excluídos após a cópia bem-sucedida.

### 6. **Interface do Usuário**
- Interface gráfica via Windows Forms ou WPF.
- Componentes:
  - Seletor de diretório de origem
  - Seletor de diretório de destino
  - Campo para especificar extensão ou opção "todas as extensões"
  - Checkbox: “Apagar arquivos originais após cópia”
  - Botão “Executar”
  - Barra de progresso com feedback visual e textual

---

## 🖥️ Plataforma

- **Sistema Operacional Suportado**:  
  - Windows 10  
  - Windows 11

- **Tecnologia Base**:  
  - **Linguagem**: C#  
  - **Framework**: .NET 6 ou superior  
  - **Interface Gráfica**: Windows Forms (WinForms) ou WPF

---

## 📂 Exemplo de Caso de Uso

**Diretório de origem:**  
`C:/downloads`

**Arquivos existentes:**  
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

**Resultado esperado:**
- Criação das pastas:
  - `C:/downloads/project-alpha-part/`
  - `C:/downloads/report-2023-q/`
  - `C:/downloads/invoice/`
  - `C:/downloads/task-list-v/`
  - `C:/downloads/notes-summary-v/`
- Arquivos copiados para suas respectivas pastas.
- Arquivos originais removidos, se a opção estiver ativada.

---

## ✅ Requisitos Funcionais (RF)

- **RF01**: Permitir ao usuário selecionar o diretório de origem.
- **RF02**: Permitir ao usuário escolher uma extensão de arquivo ou optar por incluir todas.
- **RF03**: Identificar padrões de nome e agrupar arquivos com prefixo comum.
- **RF04**: Criar pasta correspondente a cada grupo.
- **RF05**: Copiar arquivos agrupados para suas respectivas pastas.
- **RF06**: Permitir ao usuário selecionar se deseja apagar arquivos originais.
- **RF07**: Apresentar barra de progresso com porcentagem e status.
- **RF08**: Lidar com falhas de cópia sem apagar arquivos originais.

---

## 🔒 Requisitos Não Funcionais (RNF)

- **RNF01**: A aplicação deve funcionar sem necessidade de conexão com a internet.
- **RNF02**: A aplicação deve operar apenas em ambiente Windows.
- **RNF03**: Interface responsiva e acessível para resoluções a partir de 1366x768.
- **RNF04**: Mensagens claras de erro devem ser exibidas em caso de problemas de permissão, disco cheio ou falha de cópia.

---

## 📅 Etapas de Desenvolvimento

1. Setup do projeto C# com interface
2. Leitura e agrupamento de arquivos
3. Cópia e estruturação de pastas
4. Barra de progresso e feedback visual
5. Lógica de exclusão com validação
6. Validação de inputs e erros
7. Testes funcionais
8. Distribuição (compilador + empacotador .exe)
9. Design de ícone e splash screen
10. Publicação
