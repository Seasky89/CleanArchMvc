# CleanArchMvc - ASP.NET Core com Clean Architecture

Este projeto é uma implementação de **Clean Architecture** utilizando **ASP.NET Core MVC**, **Entity Framework Core**, **Identity** e **MySQL** como banco de dados.

Ele segue uma separação clara em camadas, facilitando testes, manutenção e extensibilidade.

---

## 🧱 Estrutura da Solution

A solução contém os seguintes projetos:

- **CleanArchMvc.Domain**  
  Entidades, interfaces, regras de negócio.

- **CleanArchMvc.Application**  
  Casos de uso, DTOs, serviços de aplicação e validações.

- **CleanArchMvc.Infra.Data**  
  Acesso a dados (EF Core), repositórios, migrations e Identity.

- **CleanArchMvc.Infra.IoC**  
  Configuração de Injeção de Dependência.

- **CleanArchMvc.WebUI**  
  Interface web (Controllers, Views, autenticação e rotas).

- **CleanArchMvc.Domain.Tests**  
  Testes unitários focados no domínio.

---

## 🛠 Tecnologias utilizadas

- ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Identity
- MySQL (via Pomelo)
- AutoMapper
- FluentValidation
- xUnit

---

## 📦 Configuração do ambiente

### 1. Restaurar dependências

```bash
dotnet restore
```

### 2. Ajustar o arquivo de configuração

Edite:
```bash
CleanArchMvc.WebUI/appsettings.Development.json
```

e configure a connection string:

```json
"ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=cleanarchmvc;user=root;password=1234;"
}
```

### 3. Aplicar migrations

```bash
dotnet ef database update --project CleanArchMvc.Infra.Data --startup-project CleanArchMvc.WebUI
```

### 4. Rodar o sistema

```bash
dotnet run --project CleanArchMvc.WebUI
```

---

## 🔐 Autenticação

O projeto utiliza Identity integrado ao MySQL, com tabelas criadas automaticamente pelas migrations:

- AspNetUsers
- AspNetRoles
- AspNetUserRoles
- AspNetUserClaims
- etc.

---

## 🧪 Testes

Para rodar os testes do domínio:

```bash
dotnet test CleanArchMvc.Domain.Tests
```

---

## 📚 Padrões adotados

- Clean Architecture
- Repository Pattern
- CQRS (se utilizado)
- Injeção de Dependência centralizada
- Uso de DTOs e ViewModels

---

## 📄 Licença

Este projeto está sob a licença MIT.

---
