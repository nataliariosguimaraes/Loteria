🎯 Sistema de Apostas – Mega-Sena (POC)

Este repositório contém uma Proof of Concept (POC) de um sistema backend para registro de apostas da Mega-Sena, desenvolvido em .NET Core, com foco em modelagem de domínio, arquitetura limpa e uso consciente de IA como apoio ao desenvolvimento.

O projeto foi construído partindo de documentação formal (requisitos e especificação técnica) até a implementação de um backend funcional, seguindo boas práticas de engenharia de software.

📌 Objetivo da POC

O objetivo principal deste projeto não é simular um sistema real de loteria, mas servir como um exercício prático para:

Trabalhar a partir de requisitos bem definidos

Validar decisões de arquitetura

Modelar regras reais de domínio

Explorar o uso de IA (Codex + ChatGPT) como ferramenta de apoio ao raciocínio técnico

Manter clareza de responsabilidades entre camadas

🧠 Abordagem

O desenvolvimento seguiu o fluxo:

Definição do Documento de Requisitos (SRS)

Criação do Documento Técnico de Construção

Quebra do trabalho em tarefas pequenas

Implementação incremental

Validação com testes

A IA foi utilizada como apoio, principalmente para:

organizar ideias

validar raciocínios

acelerar decisões técnicas

Ela não substituiu decisões de arquitetura, regras de negócio ou entendimento do domínio.


🏗️ Arquitetura

O projeto segue os princípios da Clean Architecture, com separação clara de responsabilidades:

Loteria.sln
├─ src
│  ├─ Loteria.Api
│  ├─ Loteria.Application
│  ├─ Loteria.Domain
│  └─ Loteria.Infrastructure
└─ tests
   ├─ Loteria.UnitTests
   └─ Loteria.IntegrationTests


Camadas

API: Controllers e exposição REST

Application: Casos de uso, DTOs e contratos

Domain: Entidades, regras de negócio e invariantes

Infrastructure: Persistência e detalhes técnicos

🎲 Regras de Negócio Implementadas

A POC cobre regras reais da Mega-Sena:

Dezenas válidas de 1 a 60

Quantidade mínima de 6 e máxima de 20 dezenas

Proibição de dezenas repetidas

Aposta imutável após registro

Funcionalidades

Surpresinha 🎲
Geração automática de dezenas pelo sistema

Teimosinha 🔁
Participação automática em concursos consecutivos
Valores permitidos: 2, 3, 4, 6, 8, 9, 12

🌐 API

API REST simples e direta

Controllers finos

Validações centralizadas

Comunicação via JSON

💾 Persistência

Entity Framework Core

Banco de dados InMemory

Abordagem Code First

Persistência isolada na camada de infraestrutura

🧪 Testes

O projeto inclui:

Testes Unitários

Regras de domínio

Casos de uso

Testes de Integração

Endpoints da API

Persistência em memória

🚀 Tecnologias Utilizadas

.NET Core 8+

ASP.NET Core

Entity Framework Core

InMemory Database

xUnit

Clean Architecture

SOLID

DDD (como ferramenta de modelagem)

IA (Codex + ChatGPT) como apoio ao desenvolvimento
