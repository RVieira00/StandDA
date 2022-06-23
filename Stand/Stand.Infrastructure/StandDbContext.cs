using Microsoft.EntityFrameworkCore;
using Stand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stand.Infrastructure
{
    public class StandDbContext : DbContext
    {
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<ViaturaExtra> ViaturaExtras { get; set; }
        public DbSet<Viatura> Viaturas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        public string DbPath { get; set; }

        public StandDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}Stand.db";

            //Path Castro:  "C:\Users\diogo\AppData\Local\Packages\4f369d7b-7498-4290-9f4a-0f13977bc4aa_vnf7eeyar8jxt\LocalState"
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Marca:
                //Atributo Nome -> Único
            modelBuilder.Entity<Marca>()
                .HasIndex(x => x.Nome).IsUnique();
            modelBuilder.Entity<Marca>()
                .Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);

            //Tipo:
                //Atributo Nome -> Único
            modelBuilder.Entity<Tipo>()
                .HasIndex(x => x.Nome).IsUnique();
            modelBuilder.Entity<Tipo>()
                .Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);

            //Extra:
                //Atributo Nome -> Único
            modelBuilder.Entity<Extra>()
                .HasIndex(x => x.Nome).IsUnique();
            modelBuilder.Entity<Extra>()
                .Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);

            //Cliente
                //Atributos: Nome Endereco CartaConducao Telemovel
            modelBuilder.Entity<Cliente>()
                .Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Cliente>()
                .Property(x => x.Endereco)
                .IsRequired()
                .HasMaxLength(75);
            modelBuilder.Entity<Cliente>()
                .Property(x => x.CartaConducao)
                .IsRequired()
                .HasMaxLength(13);
            modelBuilder.Entity<Cliente>()
                .Property(x => x.CartaConducao)
                .IsRequired()
                .HasMaxLength(10);

            //Funcionario
                //Atributos: Email  Password Nome isAdmin
            modelBuilder.Entity<Funcionario>()
                .HasIndex(x => x.Email)
                .IsUnique();
            modelBuilder.Entity<Funcionario>()
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Funcionario>()
                .Property(x => x.Password)
                .HasMaxLength(50);
            modelBuilder.Entity<Funcionario>()
               .Property(x => x.Nome)
               .IsRequired()
               .HasMaxLength(50);
            modelBuilder.Entity<Funcionario>()
                .Property(x => x.isAdmin)
                .IsRequired();

            modelBuilder.Entity<Funcionario>()
                .HasData(new Funcionario{ 
                    Id = 1, 
                    Email = "admin@admin.com", 
                    Password = "admin", 
                    Nome = "admin",
                    isAdmin = true });

            //Venda
                //Atributos: Preco data
            modelBuilder.Entity<Venda>()
                .Property(x => x.Preco)
                .IsRequired();
            modelBuilder.Entity<Venda>()
                .Property(x => x.data)
                .IsRequired();

            //ViaturaExtra
            //Atributos ViaturaID   ExtraID
            modelBuilder.Entity<ViaturaExtra>()
                .Property(x => x.ViaturaId)
                .IsRequired();
            modelBuilder.Entity<ViaturaExtra>()
                .Property(x => x.ExtraId)
                .IsRequired();

            /*Viatura  
                Atributos: Matricula(Único) Modelo Cor Cilindrada Ano PrecoBasse */
            modelBuilder.Entity<Viatura>()
                .HasIndex(x => x.Matricula)
                .IsUnique();
            modelBuilder.Entity<Viatura>()
                .Property(x => x.Matricula)
                .IsRequired()
                .HasMaxLength(8);
            modelBuilder.Entity<Viatura>()
                .Property(x => x.Modelo)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Viatura>()
                .Property(x => x.Cor)
                .IsRequired()
                .HasMaxLength(50);





            //FOREIGN KEY

                //Marca - Viatura
            modelBuilder.Entity<Viatura>()
                .HasOne(m => m.Marca)
                .WithMany(v => v.Viaturas)
                .HasForeignKey(p => p.MarcaId)
                .OnDelete(DeleteBehavior.Cascade);

                //Tipo - Viatura
            modelBuilder.Entity<Viatura>()
                .HasOne(t => t.Tipo)
                .WithMany(v => v.Viaturas)
                .HasForeignKey(p => p.TipoId)
                .OnDelete(DeleteBehavior.Restrict);


            //Tabela Associativa
                //ViaturaExtra - Viatura
            modelBuilder.Entity<ViaturaExtra>()
                .HasOne(t => t.Viatura)
                .WithMany(v => v.ViaturasExtras)
                .HasForeignKey(p => p.ViaturaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ViaturaExtra>()
                .HasOne(e => e.Extra)
                .WithMany(v => v.ViaturasExtras)
                .HasForeignKey(p => p.ExtraId)
                .OnDelete(DeleteBehavior.Cascade);


                //Funcionario - Venda
            modelBuilder.Entity<Venda>()
                .HasOne(t => t.Funcionario)
                .WithMany(v => v.Vendas)
                .HasForeignKey(f => f.FuncionarioId)
                .OnDelete(DeleteBehavior.Restrict);

                //Cliente - Venda
            //modelBuilder.Entity<Venda>()
            //    .HasOne(t => t.Cliente)
            //    .WithMany(v => v.Vendas)
            //    //.HasForeignKey(c => c.Cliente.Id)
            //    .OnDelete(DeleteBehavior.Restrict);

            //Relação 1 - 1
                //Venda - Viatura
            modelBuilder.Entity<Venda>()
                .HasOne(t => t.Viatura)
                .WithOne(v => v.Venda)
                //.HasForeignKey(c => c.ViaturaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
