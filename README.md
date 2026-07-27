# Gerenciamento de Faturas

API REST desenvolvida em linguagem C# e utilizando .NET Framework 4.8 para gerenciamento de faturas e seus respectivos itens.

O projeto foi desenvolvido de acordo com uma arquitetura em camadas, separando as responsabilidades entre:
- Domain (contém as entidades e seus tratamentos).
- Application (contém os casos de uso da aplicação).
- Infrastructure (concentra-se mais na persistência de dados).
- API (contém as requisições HTTP).

## Tecnologias utilizadas

- C#
- .NET Framework 4.8
- ASP.NET Web API
- Entity Framework 6.2
- SQL Server LocalDB
- Unity (para injeção de dependência)
- Swagger (Swashbuckle)
- MSTest
- Effort.EF6

# Como executar o projeto

## Pré-requisitos

- Visual Studio 2022
- .NET Framework 4.8 SDK
- SQL Server LocalDB

## Configuração

1. Clone o repositório.

2. Abra a solução no Visual Studio.

3. Verifique a Connection String no arquivo Web.config.

Exemplo:

```xml
<connectionStrings>
    <add
        name="GerenciamentoFaturasConnection"
        connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GerenciamentoFaturas;Integrated Security=True"
        providerName="System.Data.SqlClient"/>
</connectionStrings>
```

4. Execute o projeto.

5. O Swagger será aberto automaticamente.

**OBS.: o Swagger costuma demorar alguns segundos para carregar:**
- **Primeira execução após abrir o Visual Studio: 5 a 15 segundos.**
- **Execuções seguintes: 2 a 5 segundos.**

## Como executar os testes

1. Abra o Test Explorer.

2. Compile a solução.

3. Execute `Run All Tests`.

Todos os testes são executados automaticamente utilizando MSTest.

## Utilização do Effort

Os testes utilizam o pacote **Effort.EF6**, que fornece um banco de dados em memória compatível com o Entity Framework 6.

Durante a execução dos testes, um contexto temporário é criado:

```csharp
var connection = DbConnectionFactory.CreateTransient();

Context = new GerenciamentoFaturasContext(connection);
```

Dessa forma:

- nenhum dado é gravado no SQL Server.
- cada teste inicia com um banco vazio.
- os testes são independentes entre si.
- a execução é rápida e reproduzível.

Essa abordagem permite testar a camada de persistência sem depender de um banco físico.

## Premissas adotadas

Durante o desenvolvimento foram consideradas as premissas:

- Uma fatura fechada não pode ser alterada.
- Itens podem ser adicionados, atualizados e removidos apenas em faturas abertas.
- O nome do cliente é obrigatório.
- A descrição do item é obrigatória e deve possuir no mínimo três caracteres.
- Quantidade e valor unitário devem ser maiores do que zero.
- Quando o valor total do item ultrapassa R$ 1000,00, a justificativa torna-se obrigatória.
- O valor total da fatura é recalculado automaticamente sempre que um item é adicionado, atualizado ou removido.

## Decisões técnicas

Durante o desenvolvimento foram adotadas as seguintes decisões:

- Arquitetura em camadas para separação de responsabilidades.
- DTOs para desacoplar a API das entidades de domínio.
- Mappers responsáveis pelas conversões entre entidades e DTOs.
- Repositórios para abstração da persistência.
- Serviços contendo todas as regras de negócio.
- Injeção de dependência utilizando Unity.
- Swagger para documentação e testes da API.
- Exceções de domínio para representar regras específicas do negócio.
- Testes utilizando banco em memória (Effort).

## Melhorias futuras

- Paginação na consulta de faturas.
- Validação utilizando FluentValidation;
- Utilização de AutoMapper para reduzir código repetitivo.
- Documentação da API utilizando exemplos mais completos no Swagger.
