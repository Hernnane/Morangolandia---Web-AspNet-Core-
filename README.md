# 🍓 MorangoWeb3 - Plataforma de Receitas Online  

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-6.0-blue?style=for-the-badge&logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-red?style=for-the-badge&logo=microsoftsqlserver)
![C#](https://img.shields.io/badge/C%23-Programming_Language-blueviolet?style=for-the-badge&logo=csharp)

## 📌 Sobre o Projeto  

**MorangoWeb3** é uma plataforma web desenvolvida em **ASP.NET Core MVC** que permite aos usuários cadastrar, salvar e gerenciar receitas culinárias. O projeto oferece um sistema de autenticação personalizado, upload de imagens para receitas e uma interface dinâmica para facilitar a experiência do usuário.  

## 🚀 Tecnologias Utilizadas  

- 🖥 **Back-end:** ASP.NET Core MVC  
- 🎨 **Front-end:** Razor Pages + Bootstrap  
- 🗄 **Banco de Dados:** SQL Server  
- 🏗 **ORM:** Entity Framework Core  
- ✅ **Validação:** FluentValidation  
- 🔐 **Autenticação:** Sistema próprio (sem Identity)  
- 🖼 **Gerenciamento de Imagens:** Upload via IFormFile e conversão para PNG  

---

## 🔥 Funcionalidades  

✔ **Autenticação & Perfil**  
- Cadastro e login sem uso do **UserManager**  
- Validação de senha com requisitos de segurança  
- Edição de perfil (incluindo foto e apelido)  

✔ **Sistema de Receitas**  
- Criar, editar e excluir receitas  
- Armazenamento de imagens de receitas no sistema de arquivos  
- Conversão automática de imagens para PNG  

✔ **Salvamento de Receitas**  
- Qualquer usuário pode salvar receitas publicadas por outros usuários  
- Listagem dinâmica das receitas salvas  

✔ **Banco de Dados & Relacionamentos**  
- Modelagem das tabelas com **Foreign Keys**  
- Integração com Entity Framework Core  

✔ **Segurança & Validação**  
- Uso do **FluentValidation** para garantir entrada de dados segura  

---

