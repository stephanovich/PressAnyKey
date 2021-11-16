using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PressAnyKey.Models;

namespace PressAnyKey.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Amigo> Amigo { get; set; }
        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Feed> Feed { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Reacao> Reacao { get; set; }
        public DbSet<Postagem> Postagem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Amigo>().ToTable("Amigo");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");

            modelBuilder.Entity<Amigo>()
                .HasKey(a => new { a.IdRemetente, a.IdDestinatario });

            modelBuilder.Entity<Amigo>()
                .HasOne(a => a.UsuarioRemetente)
                .WithMany(u => u.Seguidores)
                .HasForeignKey(a => a.IdRemetente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Amigo>()
                .HasOne(a => a.UsuarioDestinatario)
                .WithMany(u => u.Seguindo)
                .HasForeignKey(a => a.IdDestinatario)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
