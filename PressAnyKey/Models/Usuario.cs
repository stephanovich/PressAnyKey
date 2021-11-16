using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PressAnyKey.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public ICollection<Amigo> Seguidores { get; set; }
        public ICollection<Amigo> Seguindo { get; set; }
        public ICollection<Postagem> Postagens { get; set; }
        public ICollection<Jogo> Jogos { get; set; }
    }
}