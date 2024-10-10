using AgendaApp.API.Dtos;
using AgendaApp.Data.Entities;
using AgendaApp.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        //atributos do tipo 'readonly'
        private readonly IMapper _mapper;

        //método construtor para realizar a injeção de dependência dos atributos
        public TarefasController(IMapper mapper)
        {
            _mapper = mapper;
        }    

        [HttpPost]
        public IActionResult Post(TarefaRequestDto request)
        {
            try
            {
                //copiar os campos da classe dto para a entidade
                var tarefa = _mapper.Map<Tarefa>(request);
                //gerando o Id para a tarefa (gravação no banco de dados)
                tarefa.Id = Guid.NewGuid();

                //gravar a tarefa no banco de dados
                var tarefaRepository = new TarefaRepository();
                tarefaRepository.Save(tarefa);

                //HTTP 201 (CREATED)
                return StatusCode(201, new { mensagem = "Tarefa cadastrada com sucesso." });
            }
            catch(Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = "Falha ao cadastra tarefa: " + e.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, TarefaRequestDto request)
        {
            try
            {
                //consultando a tarefa no repositório através do id
                var tarefaRepository = new TarefaRepository();
                var tarefa = tarefaRepository.GetById(id);

                //verificando se a tarefa não foi encontrada
                if (tarefa == null)
                    //HTTP 404 (NOT FOUND)
                    return StatusCode(404, new { mensagem = "Tarefa não encontrada para edição. Verifique o ID informado." });

                //utilizando o automapper para copiar os dados que serão atualizados
                tarefa.Categoria = null; //apagando o vínculo com a categoria
                _mapper.Map(request, tarefa);
                
                //atualizando no banco de dados
                tarefaRepository.Update(tarefa);

                //HTTP 200 (OK)
                return StatusCode(200, new { mensagem = "Tarefa atualizada com sucesso." });
            }
            catch(Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = "Falha ao atualizar tarefa: " + e.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                //consultando a tarefa no repositório através do id
                var tarefaRepository = new TarefaRepository();
                var tarefa = tarefaRepository.GetById(id);

                //verificando se o registro não foi encontrado
                if (tarefa == null)
                    //HTTP 404 (NOT FOUND)
                    return StatusCode(404, new { mensagem = "Tarefa não encontrada para exclusão. Verifique o ID informado." });

                //excluindo a tarefa
                tarefaRepository.Delete(tarefa);

                //HTTP 200 (OK)
                return StatusCode(200, new { mensagem = "Tarefa excluída com sucesso." });
            }
            catch(Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = "Falha ao excluir tarefa: " + e.Message });
            }
        }

        [HttpGet("{dataMin}/{dataMax}")]
        public IActionResult Get(DateTime dataMin, DateTime dataMax)
        {
            try
            {
                //consultando as tarefas no banco de dados através do período de datas
                var tarefaRepository = new TarefaRepository();
                var tarefas = tarefaRepository.GetByDatas(dataMin, dataMax);

                //copiando os dados obtida da consulta para uma lista da classe DTO
                var response = _mapper.Map<List<TarefaResponseDto>>(tarefas);

                //HTTP 200 (OK)
                return StatusCode(200, response);
            }
            catch(Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                //consultando a tarefa no banco de dados através do ID
                var tarefaRepository = new TarefaRepository();
                var tarefa = tarefaRepository.GetById(id);

                //verificando o registro não foi encontrado
                if (tarefa == null)
                    return StatusCode(204); //HTTP 204 (NO CONTENT)

                //copiar os dados da tarefa para o objeto dto
                var response = _mapper.Map<TarefaResponseDto>(tarefa);

                //HTTP 200 (OK)
                return StatusCode(200, response);
            }
            catch(Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

    }
}
