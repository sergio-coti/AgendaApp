# AgendaApp

Este projeto é uma API desenvolvida em ASP.NET Core 8 para gerenciar tarefas e categorias.

## Tecnologias Utilizadas

- **ASP.NET Core 8**: Framework principal utilizado para construir a API.
- **Entity Framework Core**: ORM usado para interagir com o banco de dados de forma eficiente e simplificada.
- **AutoMapper**: Biblioteca para mapeamento automático de objetos, usada para simplificar a conversão entre DTOs e entidades de domínio.
- **Swagger**: Ferramenta de documentação de API, integrada para fornecer uma interface interativa para testar os endpoints da API.

## Requisitos

- .NET 8 SDK ou superior
- Banco de dados SQL Server
- Visual Studio 2022

## Configuração do Projeto

1. Clone o repositório:
    ```bash
    git clone https://github.com/seu-usuario/task-management-api.git
    ```

2. Altere a string de conexão na classe `DataContext` localizada na pasta `Data` ou conforme a estrutura do seu projeto. O código na classe `DataContext` deve ficar assim:

    ```csharp
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=BDAgendaApp;Trusted_Connection=True;");
            }
        }

        // Demais códigos
    }
    ```

3. Execute as migrações do Entity Framework para criar o banco de dados. No Visual Studio, abra o **Console do Gerenciador de Pacotes** e rode o seguinte comando:
    ```bash
    update-database
    ```

## Documentação e Testes

A documentação da API pode ser acessada utilizando o Swagger em `http://localhost:{porta}/swagger`. Através dessa interface, é possível testar todos os endpoints da API de maneira interativa.

## Frontend - AgendaWeb

O frontend da aplicação, chamado **AgendaWeb**, foi desenvolvido para interagir com esta API. O código-fonte pode ser encontrado no seguinte repositório:
- [AgendaWeb - GitHub](https://github.com/sergio-coti/AgendaWeb)

## Autenticação - UsuariosApp

O projeto responsável pela autenticação dos usuários desta aplicação é o **UsuariosApp**, uma API separada que gerencia a autenticação e controle de acesso. O repositório está disponível em:
- [UsuariosApp - GitHub](https://github.com/sergio-coti/UsuariosApp)

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

## Licença

Este projeto está licenciado sob os termos da [MIT License](LICENSE).
