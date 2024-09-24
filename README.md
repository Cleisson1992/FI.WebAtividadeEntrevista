# WebAtividadeEntrevista

## Visão Geral

**WebAtividadeEntrevista** é uma aplicação web projetada para gerenciar clientes e seus beneficiários. A aplicação permite que os usuários cadastrem, atualizem e gerenciem clientes, garantindo a integridade e validação dos dados.

## Funcionalidades

### Gerenciamento de Clientes
- **Cadastro/Alteração de Clientes**: 
  - Inclusão de um campo obrigatório para CPF (Cadastro de Pessoas Físicas) durante o cadastro de clientes.
  - O CPF é validado quanto ao formato correto (999.999.999-99) e unicidade no banco de dados.
- **Integração com o Banco de Dados**: 
  - As informações do cliente, incluindo CPF, são armazenadas na tabela `CLIENTES`.

### Gerenciamento de Beneficiários
- **Cadastro de Beneficiários**:
  - Um botão denominado "Beneficiários" permite que os usuários gerenciem beneficiários associados a clientes.
  - Um formulário em pop-up permite a adição do CPF e Nome para cada beneficiário.
  - O CPF do beneficiário também deve estar no formato correto e ser único por cliente.
  - Um grid exibe todos os beneficiários de um cliente, permitindo atualizações e exclusões.
- **Integração com o Banco de Dados**: 
  - Os beneficiários são armazenados na tabela `BENEFICIARIOS`, com campos para ID, CPF, Nome e ID do Cliente.

## Estrutura do Banco de Dados

### Tabela CLIENTES
- `ID`: Chave Primária
- `NOME`: Nome do cliente
- `SOBRENOME`: Sobrenome do cliente
- `CPF`: CPF do cliente
- `EMAIL`: E-mail do cliente
- `TELEFONE`: Telefone do cliente
- `ENDERECO`: Detalhes do endereço do cliente

### Tabela BENEFICIARIOS
- `ID`: Chave Primária
- `CPF`: CPF do beneficiário
- `NOME`: Nome do beneficiário
- `IDCLIENTE`: Chave estrangeira referenciando a tabela `CLIENTES`

## Requisitos

- A aplicação é desenvolvida em ASP.NET MVC e utiliza um banco de dados interno localizado em `~\FI.WebAtividadeEntrevista\App_Data`
