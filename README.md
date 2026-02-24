#  DojoManager 

> **Gestão Inteligente para Academias de Luta.**
> 
> O DojoManager é uma plataforma completa para o gerenciamento de atletas, fluxo financeiro e evolução técnica, desenvolvida para simplificar a rotina de centros de treinamento.

---

## Tecnologias e Ferramentas ##

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET 8](https://img.shields.io/badge/.NET%208-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![React](https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-38B2AC?style=for-the-badge&logo=tailwind-css&logoColor=white)

---

##  Funcionalidades

###  Painel de Controle (Dashboard 📊)
* **Visão Geral**: Acompanhamento de atletas ativos, inativos e arrecadação total.
* **Gráfico de Faturamento**: Visualização mensal da performance financeira usando **Recharts**.

###  Gestão de Atletas
* **Matrícula Inteligente**: Cadastro com máscaras de dados automáticas para WhatsApp `(00) 0000-000`.
* **Sistema de Graduação**: Lógica específica para **Jiu-Jitsu**, permitindo a evolução por graus e troca automática de faixa.
* **Filtro Avançado**: Busca rápida por nome para localizar atletas no tatame.

###  Gestão Financeira
* **Fluxo de Caixa**: Monitoramento de mensalidades pagas e pendentes.
* **Navegação por Período**: Filtro simplificado por meses para auditoria financeira histórica.

---

##  Arquitetura do Projeto

O projeto segue princípios de **Clean Code** e **SOLID**, garantindo manutenção facilitada:

1.  **Backend (API)**: Desenvolvido em .NET 8 com **Entity Framework Core**, utilizando o padrão Repository para abstração do banco SQL Server.
2.  **Frontend (Web)**: Single Page Application (SPA) construída com React e Vite, focada em UX/UI responsiva com Tailwind CSS.

---

##  Como Rodar o Projeto

### Pré-requisitos
* SDK do .NET 8
* Node.js (v18+)
* SQL Server Express/LocalDB

### 1. Configurando o Banco de Dados
No arquivo `appsettings.json` da API, configure sua Connection String e execute as migrations:
```bash
dotnet ef database update
