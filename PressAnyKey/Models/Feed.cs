using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PressAnyKey.Models
{
    public class Feed
    {
        public int Id { get; set; }
        public ICollection<Postagem> Postagems { get; set; }
    }
}
