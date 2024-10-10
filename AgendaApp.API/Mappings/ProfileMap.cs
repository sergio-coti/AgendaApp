using AgendaApp.API.Dtos;
using AgendaApp.Data.Entities;
using AutoMapper;

namespace AgendaApp.API.Mappings
{
    /// <summary>
    /// Classe para mapeamento de transferência de dados entre
    /// as classes de entidade e os DTOs (requests e responses).
    /// </summary>
    public class ProfileMap : Profile
    {
        //método construtor -> ctor + [tab]
        public ProfileMap()
        {
            //copiar os dados da classe 'Categoria' para a classe 'CategoriaResponseDto'
            CreateMap<Categoria, CategoriaResponseDto>();

            //copiar os dados da classe 'TarefaRequestDto' para a classe 'Tarefa'
            CreateMap<TarefaRequestDto, Tarefa>();

            //copiar os dados da classe 'Tarefa' para 'TarefaResponseDto'
            CreateMap<Tarefa, TarefaResponseDto>();
        }
    }
}
