using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Data.Entities
{
    /// <summary>
    /// Modelo de entidade para Categoria
    /// </summary>
    public class Categoria
    {
        #region Propriedades

        public Guid Id { get; set; }
        public string? Descricao { get; set; }

        #endregion

        #region Relacionamentos

        public List<Tarefa>? Tarefas { get; set; }

        #endregion
    }
}
