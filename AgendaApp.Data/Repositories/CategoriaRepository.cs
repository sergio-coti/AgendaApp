using AgendaApp.Data.Contexts;
using AgendaApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Data.Repositories
{
    /// <summary>
    /// Classe para operações de repositório de dados de Categoria.
    /// </summary>
    public class CategoriaRepository
    {
        //Método para consultar todas as categorias
        public List<Categoria> GetAll()
        {
            //abrindo conexão com o banco de dados
            using (var dataContext = new DataContext())
            {
                return dataContext //conexão com o banco de dados
                    .Set<Categoria>() //consulta na tabela de categoria
                    .OrderBy(c => c.Descricao) //ordernar pelo campo 'descrição'
                    .ToList(); //retornar uma lista com todos os registros
            }
        }
    }
}
