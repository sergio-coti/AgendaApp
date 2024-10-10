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
    /// Classe de mapeamento para a entidade 'Tarefa'
    /// </summary>
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            //nome da tabela
            builder.ToTable("TAREFA");

            //chave primária
            builder.HasKey(t => t.Id);

            //campo 'id'
            builder.Property(t => t.Id)
                .HasColumnName("ID"); //nome do campo

            //campo 'nome'
            builder.Property(t => t.Nome)
                .HasColumnName("NOME") //nome do campo
                .HasMaxLength(150) //max de caracteres
                .IsRequired(); //not null (obrigatório)

            //campo 'data'
            builder.Property(t => t.Data)
                .HasColumnName("DATA") //nome do campo
                .HasColumnType("date") //tipo do campo
                .IsRequired(); //not null (obrigatório)

            //campo 'hora'
            builder.Property(t => t.Hora)
                .HasColumnName("HORA") //nome do campo
                .HasColumnType("time") //tipo do campo
                .IsRequired(); //not null (obrigatório)

            //campo 'prioridade'
            builder.Property(t => t.Prioridade)
                .HasColumnName("PRIORIDADE") //nome do campo
                .IsRequired(); //not null (obrigatório)

            //campo 'categoriaid'
            builder.Property(t => t.CategoriaId)
                .HasColumnName("CATEGORIAID") //nome do campo
                .IsRequired(); //not null (obrigatório)

            //mapeamento do relacionamento (1pN)
            builder.HasOne(t => t.Categoria) //tarefa TEM 1 Categoria
                .WithMany(c => c.Tarefas) //categoria TEM MUITAS Tarefas
                .HasForeignKey(t => t.CategoriaId); //Chave estrangeira
        }
    }
}
