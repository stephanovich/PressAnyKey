using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PressAnyKey.Models
{
    public class Amigo
    {
        public int IdRemetente { get; set; }
        public int IdDestinatario { get; set; }
        public Usuario UsuarioRemetente { get; set; }
        public Usuario UsuarioDestinatario { get; set; }
    }
}
