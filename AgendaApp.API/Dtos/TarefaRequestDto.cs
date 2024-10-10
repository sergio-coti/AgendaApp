using System.ComponentModel.DataAnnotations;

namespace AgendaApp.API.Dtos
{
    /// <summary>
    /// Modelo de dados para cadastro / edição de tarefa
    /// </summary>
    public class TarefaRequestDto
    {
        [MaxLength(150, ErrorMessage = "Informe o nome da tarefa como no máximo {1} caracteres.")]
        [MinLength(6, ErrorMessage = "Informe o nome da tarefa com pelo menos {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome da tarefa.")]
        public string? Nome { get; set; }

        [RegularExpression(@"\d{4}-\d{2}-\d{2}", 
            ErrorMessage = "Informe a data da tarefa no formato 'YYYY-MM-DD'.")]
        [Required(ErrorMessage = "Por favor, informe a data da tarefa.")]
        public string? Data { get; set; }

        [RegularExpression(@"\d{2}:\d{2}", 
            ErrorMessage = "Informe a hora da tarefa no formado 'HH:MM'.")]
        [Required(ErrorMessage = "Por favor, informe a hora da tarefa.")]
        public string? Hora { get; set; }

        [Range(1,3, ErrorMessage = "Por favor, informe a prioridade com valores 1, 2 ou 3.")]
        [Required(ErrorMessage = "Por favor, informe a prioridade da tarefa.")]
        public int? Prioridade { get; set; }

        [Required(ErrorMessage = "Por favor, informe a categoria da tarefa.")]
        public Guid? CategoriaId { get; set; }
    }
}
