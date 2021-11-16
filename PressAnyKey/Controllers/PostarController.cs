using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PressAnyKey.Data;
using PressAnyKey.Models;

namespace PressAnyKey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostarController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Postar
        [HttpGet]
        public IEnumerable<Postagem> Get()
        {
            return null;
        }

        // GET: api/Postar/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<Postagem> Get(int id)
        {
            switch (id)
            {
                case 1:
                    {
                        List<Postagem> postagens = new List<Postagem>();

                        var IdentityUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                        var IdentityUser = _context.Users.FirstOrDefault(u => u.Id == IdentityUserId);
                        var usuarioLogado = _context.Usuario.FirstOrDefault(u => u.IdentityUser.Id == IdentityUserId);
                        var amigos = _context.Amigo.Include(a => a.UsuarioDestinatario).Include(a => a.UsuarioRemetente);
                        var feed = _context.Postagem.ToList();
                        if (usuarioLogado.Postagens != null)
                            postagens.AddRange(usuarioLogado.Postagens);

                        foreach (var amigo in amigos)
                        {
                            if (amigo.UsuarioRemetente == usuarioLogado)
                                if (amigo.UsuarioDestinatario.Postagens != null)
                                    postagens.AddRange(amigo.UsuarioDestinatario.Postagens);
                        }
                        postagens.Sort((x, y) => y.DataPostagem.CompareTo(x.DataPostagem));
                        return postagens;
                    }
                case 2:
                    {
                        List<Postagem> postagens = new List<Postagem>();

                        var IdentityUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                        var IdentityUser = _context.Users.FirstOrDefault(u => u.Id == IdentityUserId);
                        var usuarioLogado = _context.Usuario.FirstOrDefault(u => u.IdentityUser.Id == IdentityUserId);
                        var amigos = _context.Amigo.Include(a => a.UsuarioDestinatario).Include(a => a.UsuarioRemetente);
                        var feed = _context.Postagem.ToList();
                        if (usuarioLogado.Postagens != null)
                            postagens.AddRange(usuarioLogado.Postagens);

                        postagens.Sort((x, y) => y.DataPostagem.CompareTo(x.DataPostagem));
                        return postagens;
                    }
                default:
                    return null;
            }

        }

        // POST: api/Postar
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string mensagem)
        {

            var postagem = new Postagem();
            /*if (ModelState.IsValid)
            {*/
                var IdentityUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var IdentityUser = _context.Users.FirstOrDefault(u => u.Id == IdentityUserId);
                var usuarioLogado = _context.Usuario.FirstOrDefault(u => u.IdentityUser.Id == IdentityUserId);
                postagem.Mensagem = mensagem;
                postagem.Usuario = usuarioLogado;
                postagem.Comentarios = new List<Comentario>();
                postagem.Reacoes = new List<Reacao>();
                postagem.DataPostagem = DateTime.Now;
                _context.Add(postagem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Feed");
            /*}
            else
            {
                return RedirectToAction("Index", "Home");
            }*/
        }

        // PUT: api/Postar/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] int id)
        {
            var IdentityUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var IdentityUser = _context.Users.FirstOrDefault(u => u.Id == IdentityUserId);
            var usuarioLogado = _context.Usuario.FirstOrDefault(u => u.IdentityUser.Id == IdentityUserId);
            var postagem = await _context.Postagem.FindAsync(id);

            if (usuarioLogado == postagem.Usuario)
            {
                _context.Postagem.Remove(postagem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Usuario");
            }
            else
            {
                return RedirectToAction("Index", "Feed");
            }

        }
    }
}
