using AgendaApp.API.Dtos;
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
    public class CategoriasController : ControllerBase
    {
        //atributo
        private readonly IMapper _mapper;

        //método construtor para inicializar os atributos da classe
        //este construtor realiza a 'injeção de dependência' para cada atributo
        public CategoriasController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //realizando a consulta de categorias no banco de dados
            var categoriaRepository = new CategoriaRepository();

            //usando o AutoMapper para copiar a lista de categorias para uma lista do DTO
            var result = _mapper.Map<List<CategoriaResponseDto>>(categoriaRepository.GetAll());
            
            //retornando a lista de objetos DTO
            return Ok(result);
        }
    }
}
