using AgendaApp.Data.Enums;

namespace AgendaApp.API.Dtos
{
    /// <summary>
    /// Modelo de dados para consulta de tarefas na API
    /// </summary>
    public class TarefaResponseDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Data { get; set; }
        public string? Hora { get; set; }
        public string? Prioridade { get; set; }
        public CategoriaResponseDto? Categoria { get; set; }
    }
}
