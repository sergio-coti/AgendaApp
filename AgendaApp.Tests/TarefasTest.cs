using AgendaApp.API.Models;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AgendaApp.Tests
{
    /// <summary>
    /// Classe para desenvolvimento dos testes do ENDPOINT de tarefas
    /// </summary>
    public class TarefasTest
    {
        [Fact]
        public async Task<CriarTarefaResponseModel> CadastrarTarefas_Test()
        {
            #region Gerar os dados do teste

            //instanciando a biblioteca JAVA FAKER
            var faker = new Faker("pt_BR");
            
            //Criando os dados para cadastrar a tarefa
            var request = new CriarTarefaRequestModel
            {
                Nome = faker.Lorem.Sentences(1),
                Descricao = faker.Lorem.Sentences(1),
                DataHora = faker.Date.Between(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7)),
                Prioridade = faker.Random.Int(1,3)
            };

            //converter / serializar os dados em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request),
                                Encoding.UTF8, "application/json");

            #endregion

            #region Enviando a requisição POST para a API

            //instanciando a API através da classe Program (classe de inicialização)
            var client = new WebApplicationFactory<Program>().CreateClient();
            
            //executando uma chamada para o serviço POST de cadastro de tarefas
            var result = await client.PostAsync("/api/tarefas", jsonRequest);

            #endregion

            #region Verificar o resultado

            //verificando se a resposta é igual a 201 (CREATED)
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            //lendo os dados obtidos da API
            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<CriarTarefaResponseModel>(jsonResult);

            //critérios para cada campo obtido
            response?.Id.Should().NotBeEmpty();
            response?.Nome.Should().Be(request.Nome);
            response?.Descricao.Should().Be(request.Descricao);
            response?.DataHora.Should().Be(request.DataHora);
            response?.Prioridade.Should().Be(request.Prioridade);
            response?.DataHoraCadastro.Should().NotBeNull();
            response?.Status.Should().Be(1);

            #endregion

            //retornando os dados da tarefa cadastrada
            return response;
        }

        [Fact]
        public async Task AtualizarTarefas_Test()
        {
            #region Gerar os dados do teste

            //cadastrar uma tarefa..
            var tarefa = await CadastrarTarefas_Test();

            //instanciando a biblioteca JAVA FAKER
            var faker = new Faker("pt_BR");

            //Criando os dados para atualizar a tarefa
            var request = new EditarTarefaRequestModel
            {
                Id = tarefa.Id, //ID da tarefa cadastrada
                Nome = faker.Lorem.Sentences(1),
                Descricao = faker.Lorem.Sentences(1),
                DataHora = faker.Date.Between(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7)),
                Prioridade = faker.Random.Int(1, 3)
            };

            //converter / serializar os dados em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request),
                                Encoding.UTF8, "application/json");

            #endregion

            #region Enviando a requisição PUT para a API

            //instanciando a API através da classe Program (classe de inicialização)
            var client = new WebApplicationFactory<Program>().CreateClient();

            //executando uma chamada para o serviço PUT de cadastro de tarefas
            var result = await client.PutAsync("/api/tarefas", jsonRequest);

            #endregion

            #region Verificar o resultado

            //verificando se a resposta é igual a 200 (OK)
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //lendo os dados obtidos da API
            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<EditarTarefaResponseModel>(jsonResult);

            //critérios para cada campo obtido
            response?.Id.Should().Be(request.Id);
            response?.Nome.Should().Be(request.Nome);
            response?.Descricao.Should().Be(request.Descricao);
            response?.DataHora.Should().Be(request.DataHora);
            response?.Prioridade.Should().Be(request.Prioridade);
            response?.DataHoraCadastro.Should().NotBeNull();
            response?.DataHoraUltimaAtualizacao.Should().NotBeNull();
            response?.Status.Should().Be(1);

            #endregion
        }

        [Fact]
        public async Task ExcluirTarefas_Test()
        {
            #region Gerar os dados do teste

            //cadastrar uma tarefa..
            var tarefa = await CadastrarTarefas_Test();

            #endregion

            #region Enviando a requisição DELETE para a API

            //instanciando a API através da classe Program (classe de inicialização)
            var client = new WebApplicationFactory<Program>().CreateClient();

            //executando uma chamada para o serviço DELETE de cadastro de tarefas
            var result = await client.DeleteAsync("/api/tarefas/" + tarefa.Id);

            #endregion

            #region Verificar o resultado

            //verificando se a resposta é igual a 200 (OK)
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //lendo os dados obtidos da API
            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<ExcluirTarefaResponseModel>(jsonResult);

            //critérios para cada campo obtido
            response?.Id.Should().Be(tarefa.Id);
            response?.Nome.Should().Be(tarefa.Nome);
            response?.Descricao.Should().Be(tarefa.Descricao);
            response?.DataHora.Should().Be(tarefa.DataHora);
            response?.Prioridade.Should().Be(tarefa.Prioridade);
            response?.DataHoraExclusao.Should().NotBeNull();

            #endregion
        }

        [Fact]
        public async Task ConsultarTarefasPorDatas_Test()
        {
            #region Gerar os dados do teste

            //realizar o cadastro de uma tarefa
            var tarefa = await CadastrarTarefas_Test();

            //definindo as datas para pesquisar as tarefas
            var dataInicio = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            var dataFim = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");

            #endregion

            #region Enviando a requisição GET para a API

            //instanciando a API através da classe Program (classe de inicialização)
            var client = new WebApplicationFactory<Program>().CreateClient();

            //fazendo a requisição para consultar as tarefas
            var result = await client.GetAsync("/api/tarefas/" + dataInicio + "/" + dataFim);

            #endregion

            #region Verificar o resultado

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //deserializando os dados obtidos no teste
            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<List<ConsultarTarefaResponseModel>>(jsonResult);

            //procurar a tarefa cadastrada dentro da lista obtida da API
            var tarefaObtida = response?.FirstOrDefault(t => t.Id == tarefa.Id);

            //comparando se os campos da tarefa estão corretos
            tarefaObtida?.Id.Should().Be(tarefa.Id);
            tarefaObtida?.Nome.Should().Be(tarefa.Nome);
            tarefaObtida?.Descricao.Should().Be(tarefa.Descricao);
            tarefaObtida?.DataHora.Should().Be(tarefa.DataHora);
            tarefaObtida?.Prioridade.Should().Be(tarefa.Prioridade);

            #endregion
        }

        [Fact]
        public async Task ObterTarefaPorId_Test()
        {
            #region Gerar os dados do teste

            //realizar o cadastro de uma tarefa
            var tarefa = await CadastrarTarefas_Test();

            #endregion

            #region Enviando a requisição GET para a API

            //instanciando a API através da classe Program (classe de inicialização)
            var client = new WebApplicationFactory<Program>().CreateClient();

            //fazendo a requisição para consulta a tarefa
            var result = await client.GetAsync("/api/tarefas/" + tarefa.Id);

            #endregion

            #region Verificar o resultado

            //verificando se a resposta é igual a 200 (OK)
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //lendo os dados obtidos da API
            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<ConsultarTarefaResponseModel>(jsonResult);

            //critérios para cada campo obtido
            response?.Id.Should().Be(tarefa.Id);
            response?.Nome.Should().Be(tarefa.Nome);
            response?.Descricao.Should().Be(tarefa.Descricao);
            response?.DataHora.Should().Be(tarefa.DataHora);
            response?.Prioridade.Should().Be(tarefa.Prioridade);

            #endregion
        }
    }
}
