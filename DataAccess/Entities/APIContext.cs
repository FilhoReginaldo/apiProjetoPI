using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Efficacy.Api.DataAccess.Entities
{
    public partial class APIContext : DbContext
    {
        public APIContext()
        {
        }

        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PESSOA> PESSOA { get; set; }
        public virtual DbSet<PESSOA_ENDERECO> PESSOA_ENDERECO { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PESSOA>(entity =>
            {
                entity.Property(e => e.DataCriacao).HasColumnType("datetime");

                entity.Property(e => e.DataNascimento).HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PESSOA_ENDERECO>(entity =>
            {
                entity.Property(e => e.Bairro)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CEP)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Cidade)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Logradouro)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UF)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pessoa)
                    .WithMany(p => p.PESSOA_ENDERECO)
                    .HasForeignKey(d => d.PessoaID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PESSOA_ENDERECO_PESSOA");
            });
        }
    }
}
