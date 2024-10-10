using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Data.Models
{
    /// <summary>
    /// Modelo de dados para consulta de tarefas por prioridade
    /// </summary>
    public class TarefaPrioridadeModel
    {
        public string? Prioridade { get; set; }
        public int QtdTarefas { get; set; }
    }
}
