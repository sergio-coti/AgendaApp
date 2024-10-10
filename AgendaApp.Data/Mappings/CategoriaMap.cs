using AgendaApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento para a entidade 'Categoria'
    /// </summary>
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            //nome da tabela
            builder.ToTable("CATEGORIA");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);

            //mapeamento do campo 'id'
            builder.Property(c => c.Id)
                .HasColumnName("ID"); //nome da coluna

            //mapeamento do campo 'descrição'
            builder.Property(c => c.Descricao)
                .HasColumnName("DESCRICAO") //nome da coluna
                .HasMaxLength(50) //max de caracteres
                .IsRequired(); //not null (required)
        }
    }
}
