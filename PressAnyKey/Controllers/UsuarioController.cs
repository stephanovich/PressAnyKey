using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PressAnyKey.Data;
using PressAnyKey.Models;

namespace PressAnyKey.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _context.Usuario.ToListAsync();
            var IdentityUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var IdentityUser = _context.Users.FirstOrDefault(u => u.Id == IdentityUserId);
            var usuarioLogado = _context.Usuario.FirstOrDefault(u => u.IdentityUser.Id == IdentityUserId);
            var remover = lista.FirstOrDefault(x => x.Id == usuarioLogado.Id);
            var amizade = _context.Amigo.Include(a => a.UsuarioDestinatario).Include(a => a.UsuarioRemetente);

            foreach (var a in amizade)
            {
                if (a.IdRemetente == usuarioLogado.Id)
                {
                    lista.Remove(a.UsuarioDestinatario);
                }
            }
            lista.Remove(remover);
            return View(lista);
        }

        /*        // GET: Usuario/Details/5
                public async Task<IActionResult> Details(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var usuario = await _context.Usuario
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (usuario == null)
                    {
                        return NotFound();
                    }

                    return View(usuario);
                }*/

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sobrenome,DataNascimento")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var IdentityUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var IdentityUser = _context.Users.FirstOrDefault(u => u.Id == IdentityUserId);
                var usuarioLogado = _context.Usuario.FirstOrDefault(u => u.IdentityUser.Id == IdentityUserId);
                usuario.IdentityUser = IdentityUser;
                if (usuarioLogado == null)
                {
                    _context.Add(usuario);
                }
                else
                {
                    usuarioLogado.Nome = usuario.Nome;
                    usuarioLogado.Sobrenome = usuario.Sobrenome;

                    _context.Entry(usuarioLogado).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Feed");
            }
            return View(usuario);
        }

        /* GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,DataNascimento")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }*/
    }
}
