using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PressAnyKey.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }
        public Usuario Usuario { get; set; }
        public Postagem Postagem { get; set; }
    }
}
