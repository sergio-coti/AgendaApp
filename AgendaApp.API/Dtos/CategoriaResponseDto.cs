namespace AgendaApp.API.Dtos
{
    /// <summary>
    /// Modelo de dados para consulta de categorias na API
    /// </summary>
    public class CategoriaResponseDto
    {
        public Guid Id { get; set; }
        public string? Descricao { get; set; }
    }
}
