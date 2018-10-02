using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Efficacy.Api.DataAccess
{
    public partial class ProjetoAPIContext : DbContext
    {
        public ProjetoAPIContext()
        {
        }

        public ProjetoAPIContext(DbContextOptions<ProjetoAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PESSOA> PESSOA { get; set; }
        public virtual DbSet<PESSOA_ENDERECO> PESSOA_ENDERECO { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PESSOA>(entity =>
            {
                entity.Property(e => e.Celular)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cpf_Cnpj)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DataAlteracao).HasColumnType("datetime");

                entity.Property(e => e.DataCriacao).HasColumnType("datetime");

                entity.Property(e => e.DataNascimento).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RazaoSocial)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .HasMaxLength(20)
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

                entity.Property(e => e.Numero)
                    .HasMaxLength(50)
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
