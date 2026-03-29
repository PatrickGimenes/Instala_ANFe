# Instala_ANFe

Instalador automatizado da aplicação ANFe, desenvolvido em C# com Windows Forms.

O objetivo do projeto é simplificar e padronizar o processo de instalação do sistema, garantindo consistência, automação e redução de erros manuais.

---

# Funcionalidades

O instalador realiza automaticamente:

- Criação da estrutura de diretórios
- Download do pacote da aplicação (.zip)
- Extração dos arquivos
- Organização e movimentação dos arquivos
- Configuração do banco de dados
  - Criação de tabelas
  - Inserção de dados iniciais (seed)
- Instalação do serviço Windows (ANFeService)
- Inicialização do serviço

---

# Tecnologias utilizadas

- C# (.NET 8)
- Windows Forms
- Npgsql (PostgreSQL)
- System.Data.SqlClient (SQL Server)
- Arquitetura baseada em:
  - Factory Pattern
  - Step Pattern (pipeline de instalação)

---

# Arquitetura

O projeto foi estruturado utilizando o padrão de execução por etapas (`IInstallStep`), permitindo desacoplamento e fácil manutenção.

### Fluxo de execução:

Criação dos diretórios
↓
Download do .zip
↓
Extração dos arquivos
↓
Configuração do banco
↓
Instalação do serviço
↓
Inicialização

---

# Como executar

1. Clone o repositório:

```
   git clone https://github.com/PatrickGimenes/Instala_ANFe.git
```

2. Abra no Visual Studio

3. Configure os arquivos SQL na pasta:

```
/Scripts
 ├── 01_create_tables.sql
 └── 02_seed_data.sql
```

4. Execute o projeto como Administrador

---

# Banco de dados suportado

- [x] PostgreSQL
- [x] SQL Server

---

# Segurança

- Execução com transações no banco
- Rollback automático em caso de erro
- Validação de entrada do usuário

---

# Logs

O sistema possui logging para:

- Download
- Extração
- Banco de dados
- Instalação do serviço

---

# TODO / Pendências

## Alta prioridade

- [ ] Criar sistema de versionamento de banco (migrations)
- [ ] Detectar se o serviço já está instalado
- [ ] Validar se o banco já possui estrutura antes de criar
- [ ] Melhorar tratamento de erros na UI
- [ ] Adicionar feedback visual por etapa (status detalhado)

## Média prioridade

- [ ] Suporte a atualização da aplicação
- [ ] Tela estilo wizard (Próximo / Voltar)
- [ ] Mostrar progresso detalhado (ex: arquivo atual sendo extraído)
- [ ] Validar dependências do sistema (.NET, permissões, etc)

## Baixa prioridade / melhorias

- [ ] Embutir scripts SQL no executável
- [ ] Criar desinstalador

- [ ] Melhorar UI/UX (layout profissional)

---

# 📄 Licença

Este projeto é de uso interno.
