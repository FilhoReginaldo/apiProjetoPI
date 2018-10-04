using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Efficacy.Api.DataAcess.Entities
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

        public virtual DbSet<CERVEJA> CERVEJA { get; set; }
        public virtual DbSet<FAMILIA> FAMILIA { get; set; }
        public virtual DbSet<MARCA> MARCA { get; set; }
        public virtual DbSet<PESSOA> PESSOA { get; set; }
        public virtual DbSet<PESSOA_CERVEJA> PESSOA_CERVEJA { get; set; }
        public virtual DbSet<PESSOA_ENDERECO> PESSOA_ENDERECO { get; set; }
        public virtual DbSet<TIPO> TIPO { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(local);Database=ProjetoAPI;User Id=sa;Password=suporte");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CERVEJA>(entity =>
            {
                entity.Property(e => e.Amargor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Teor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.CERVEJA)
                    .HasForeignKey(d => d.MarcaID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CERVEJA_MARCA");

                entity.HasOne(d => d.Tipo)
                    .WithMany(p => p.CERVEJA)
                    .HasForeignKey(d => d.TipoID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CERVEJA_TIPO");
            });

            modelBuilder.Entity<FAMILIA>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MARCA>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Origem)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

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

            modelBuilder.Entity<PESSOA_CERVEJA>(entity =>
            {
                entity.HasOne(d => d.Cerveja)
                    .WithMany(p => p.PESSOA_CERVEJA)
                    .HasForeignKey(d => d.CervejaID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PESSOA_CERVEJA_ID");

                entity.HasOne(d => d.Pessoa)
                    .WithMany(p => p.PESSOA_CERVEJA)
                    .HasForeignKey(d => d.PessoaID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PESSOA_CERVEJA");
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

            modelBuilder.Entity<TIPO>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Origem)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Familia)
                    .WithMany(p => p.TIPO)
                    .HasForeignKey(d => d.FamiliaID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FAMILIA");
            });
        }
    }
}
