# Arquitetura EnemPrep

Este documento formaliza as regras arquiteturais estabelecidas para o projeto EnemPrep durante a **Etapa 00**.

## Camadas e Responsabilidades

* **`EnemPrep.Domain`**: Camada mais interna. Contém exclusivamente código C# (POCOs), incluindo Entidades (`Entities`), Objetos de Valor (`ValueObjects`), Eventos de Domínio (`Events`), Enums (`Enums`), Interfaces de Repositório (`Interfaces`) e Exceções Customizadas (`Exceptions`). **Regra rígida:** Não possui dependência externa alguma.
* **`EnemPrep.Application`**: Contém regras de aplicação (casos de uso). Possui pastas para `UseCases`, `DTOs`, `Interfaces` de serviços da aplicação e `Mappings`. **Regra rígida:** Depende APENAS da camada Domain.
* **`EnemPrep.Infrastructure`**: Camada de persistência e serviços externos. Contém pastas para `Persistence` (DbContext), `Configurations` (Fluent API para o EF Core) e `Repositories`. **Regra rígida:** Depende das camadas Application e Domain.
* **`EnemPrep.Api`**: Ponto de entrada da API REST. Contém pastas para `Controllers`, `Middlewares` (para o Exception Handler) e `Extensions` (para setup de DI). **Regra rígida:** Depende da Application e conserta dependências via injeção usando a Infrastructure. Não tem regras de negócio.
* **`EnemPrep.Web`**: Front-end do aluno (MVC). **Regra rígida:** Desacoplado das outras camadas, consumindo serviços via HttpClient conectando-se na API. Não possui referência para Domain, Application ou Infrastructure.

## Convenções Gerais
* O Target Framework estipulado no projeto inicialmente e fixado via `global.json` é `.NET 10.0.103` para contornar falhas de pacotes Preview do SDK na máquina.
* Toda a base de dados usará **SQL Server**, sem exceções.
* As Entidades não devem ser vazadas na API, usando sempre conversão para `DTO` no retorno ou parâmetro.
