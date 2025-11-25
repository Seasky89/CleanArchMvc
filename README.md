# CleanArchMvc - ASP.NET Core com Clean Architecture

Este projeto é uma implementação prática dos princípios de **Clean Architecture**, utilizando:

- ASP.NET Core MVC (WebUI)
- ASP.NET Core Web API (API)
- Entity Framework Core
- ASP.NET Identity (MySQL, via Pomelo)
- JWT (JSON Web Token) para autenticação na API
- AutoMapper
- Injeção de Dependência centralizada
- Testes automatizados no domínio

A arquitetura foi projetada para ser modular, escalável e fácil de manter.

---

## 🧱 Estrutura da Solution

A solução **CleanArchMvc** contém os seguintes projetos:

### **Camada de Domínio**
- **CleanArchMvc.Domain**  
  Entidades, Value Objects, interfaces e regras de negócio puras.

- **CleanArchMvc.Domain.Tests**  
  Testes unitários das entidades e regras de negócio.

### **Camada de Aplicação**
- **CleanArchMvc.Application**  
  Casos de uso, DTOs, serviços de aplicação, validações e AutoMapper.

### **Camada de Infraestrutura**
- **CleanArchMvc.Infra.Data**  
  Acesso a dados com Entity Framework Core, repositórios, Context e Migrations.  
  Também contém integração com **Identity + MySQL**.

- **CleanArchMvc.Infra.IoC**  
  Configuração central de injeção de dependência para todas as camadas.

### **Camada de Apresentação**
- **CleanArchMvc.WebUI**  
  Interface MVC para interação do usuário (views, controllers, autenticação Identity tradicional).

- **CleanArchMvc.API**  
  API RESTful para consumo externo, utilizando **JWT** para autenticação.

---

## 🛠 Tecnologias utilizadas

- **ASP.NET Core MVC**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **ASP.NET Identity**
- **JWT Authentication**
- **MySQL com Pomelo**
- **AutoMapper**
- **Dependency Injection**
- **xUnit**

---

## ⚙️ Configuração do ambiente

### 1. Restaurar dependências
```bash
dotnet restore
```

### 2. Configurar a connection string
Use **User Secrets** no ambiente de desenvolvimento:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=localhost;userid=developer;password=SUA_SENHA;database=cleanarchdb1" --project CleanArchMvc.WebUI
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=localhost;userid=developer;password=SUA_SENHA;database=cleanarchdb1" --project CleanArchMvc.API
dotnet user-secrets set "Jwt:SecretKey" "CHAVE_SECRETA_COM_PREFERENCIA_64_BYTES" --project CleanArchMvc.API
```
O arquivo `appsettings.json`/`appsettings.Development.json` do projeto `CleanArchMvc.WebUI` deve conter:
```json
"ConnectionStrings": {
  "DefaultConnection": ""
}
```
O arquivo `appsettings.json`/`appsettings.Development.json` do projeto `CleanArchMvc.API` deve conter:
```json
"Jwt": {
  "Issuer": "EMISSOR",
  "Audience": "AUDIENCIA"
}
```


---

## 🗄️ Migrations (EF Core)
Para aplicar migrations:
```bash
dotnet ef database update --project CleanArchMvc.Infra.Data --startup-project CleanArchMvc.WebUI
```

Para gerar migrations:
```bash
dotnet ef migrations add NomeDaMigration --project CleanArchMvc.Infra.Data --startup-project CleanArchMvc.WebUI
```

---

## 🔐 Autenticação
### **WebUI (MVC)**
A autenticação ocorre via **ASP.NET Identity**, integrada ao MySQL.

### **API (JWT)**
A API utiliza **JWT (JSON Web Token)** para autenticação.
O fluxo é:

1. Usuário envia email + senha para `/api/Token/LoginUser`
2. O backend valida via Identity
3. Um JWT é emitido com:
   - ID do usuário
   - Claims configuradas
   - Expiração
4. O cliente usa o token no header:
```makefile
Authorization: Bearer <token>
```
5. As rotas protegidas exigem `[Authorize]`.

**Exemplos comuns da API :**
- **GET /api/Products** → listar produtos
- **POST /api/Products** → cadastrar produto
- **POST /api/Token/LoginUser** → retorna o JWT

---

## 🚀 Executar o sistema
### **WebUI:**
```bash
dotnet run --project CleanArchMvc.WebUI
```
### **API:**
```bash
dotnet run --project CleanArchMvc.API
```


---

## 🧪 Executar testes
```bash
dotnet test CleanArchMvc.Domain.Tests
```

---

## 📚 Padrões adotados
- Clean Architecture
- Dependency Injection
- Repositórios e serviços de aplicação
- Segregação clara entre camadas
- Testes no domínio
- DTOs + AutoMapper
- Identity + JWT
- WebAPI + MVC no mesmo ecossistema

---

## 📄 Licença
Este projeto está licenciado sob MIT.

---
