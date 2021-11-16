using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PressAnyKey.Data;
using PressAnyKey.Models;

namespace PressAnyKey.Controllers
{
    public class PerfilController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerfilController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var IdentityUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var IdentityUser = _context.Users.FirstOrDefault(u => u.Id == IdentityUserId);
            var usuarioLogado = _context.Usuario.FirstOrDefault(u => u.IdentityUser.Id == IdentityUserId);
            if (usuarioLogado == null)
                return RedirectToAction("Create", "Usuario");
            return View();

            /*List<Postagem> postagens = new List<Postagem>();
            try
            {
                var IdentityUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var IdentityUser = _context.Users.FirstOrDefault(u => u.Id == IdentityUserId);
                var usuarioLogado = _context.Usuario.FirstOrDefault(u => u.IdentityUser.Id == IdentityUserId);
                if (usuarioLogado == null)
                    return RedirectToAction("Create", "Usuario");
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
                return View(postagens);
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index", "Home");
            }*/
        }
    }
}