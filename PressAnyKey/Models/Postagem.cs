using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PressAnyKey.Models
{
    public class Postagem
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataPostagem { get; set; }
        public ICollection<Reacao> Reacoes { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }

    }
}
