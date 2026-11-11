# FinTrack API - Sistema de Gestão Financeira Pessoal 💰

API REST desenvolvida em **.NET 9** seguindo os princípios de **Clean Architecture**, criada como parte do desafio técnico do treinamento da **NTT Data**.

---

## 🏗️ Arquitetura do Projeto
A solução está dividida em 4 camadas independentes:
1. **FinTrack.Core**: Contém as Entidades de Domínio, DTOs e as regras de validação (`FluentValidation`).
2. **FinTrack.Infrastructure**: Configuração do Entity Framework Core, mapeamentos e contexto do banco de dados SQLite.
3. **FinTrack.Api**: Controladores REST, injeção de dependências, tratamento de serialização JSON e documentação OpenAPI.
4. **FinTrack.Tests**: Testes unitários e de integração automatizados utilizando **xUnit** (com cobertura de testes de 90%+).

---

## 🛠️ Tecnologias Utilizadas
* **.NET 9** / **ASP.NET Core**
* **Entity Framework Core (SQLite)**
* **FluentValidation**
* **xUnit** (Testes Unitários e de Integração)
* **OpenAPI Nativo** (.NET)
* **Docker & Docker Compose**

---

## 📄 Documentação Técnica e OpenAPI

A documentação interativa e os contratos da API podem ser acessados diretamente pela especificação OpenAPI nativa da aplicação:
* **Endpoint JSON OpenAPI**: `/openapi/v1.json`

### Endpoints Disponíveis

#### 🏷️ Categorias (`/api/v1/categorias`)
* `GET /api/v1/categorias` - Retorna todas as categorias cadastradas.
* `GET /api/v1/categorias/{id}` - Retorna os detalhes de uma categoria específica.
* `POST /api/v1/categorias` - Cadastra uma nova categoria.
* `DELETE /api/v1/categorias/{id}` - Remove uma categoria.

#### 🏦 Contas (`/api/v1/contas`)
* `GET /api/v1/contas` - Retorna todas as contas cadastradas.
* `GET /api/v1/contas/{id}` - Retorna os detalhes de uma conta específica.
* `POST /api/v1/contas` - Cadastra uma nova conta.
* `PUT /api/v1/contas/{id}` - Atualiza os dados de uma conta.
* `DELETE /api/v1/contas/{id}` - Remove uma conta (desde que não possua transações vinculadas).

#### 💳 Transações (`/api/v1/transacoes`)
* `GET /api/v1/transacoes` - Lista todas as transações financeiras com suas respectivas contas e categorias.
* `GET /api/v1/transacoes/{id}` - Retorna uma transação específica pelo ID.
* `POST /api/v1/transacoes` - Cria uma nova transação financeira (validada via FluentValidation e verificação de existência de conta/categoria).
* `DELETE /api/v1/transacoes/{id}` - Exclui uma transação existente.

---

## 📮 Postman Collection
Para facilitar os testes de integração e a demonstração da API, a collection estruturada por pastas (`Categorias`, `Contas` e `Transações`) está pronta para uso. 
* Certifique-se de configurar a variável de ambiente `baseUrl` (ex: `http://localhost:5000`) no Postman antes de executar as requisições.

---

## 🚀 Como Executar o Projeto

### 1. Executando via Docker Compose (Recomendado)
Para rodar a aplicação em um container isolado em .NET 9:
```bash
docker compose up --build
```

### 2. Executando Localmente (.NET CLI)
Na raiz da solução, execute os comandos:
```bash
# Restaurar dependências e rodar os testes
dotnet test

# Executar a API
dotnet run --project src/FinTrack.Api/FinTrack.Api.csproj
```

---

## 🧪 Executando os Testes e Relatório de Cobertura
Para rodar os testes e gerar o relatório HTML de cobertura de código (acima de 90%):
```bash
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```
