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
    public class AmigoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AmigoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Amigo
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Amigo.Include(a => a.UsuarioDestinatario).Include(a => a.UsuarioRemetente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Amigo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amigo = await _context.Amigo
                .Include(a => a.UsuarioDestinatario)
                .Include(a => a.UsuarioRemetente)
                .FirstOrDefaultAsync(m => m.IdRemetente == id);
            if (amigo == null)
            {
                return NotFound();
            }

            return View(amigo);
        }

        // GET: Amigo/Create
        public IActionResult Create()
        {
            ViewData["IdDestinatario"] = new SelectList(_context.Usuario, "Id", "Id");
            ViewData["IdRemetente"] = new SelectList(_context.Usuario, "Id", "Id");
            return View();
        }

        // POST: Amigo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRemetente,IdDestinatario")] Amigo amigo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDestinatario"] = new SelectList(_context.Usuario, "Id", "Id", amigo.IdDestinatario);
            ViewData["IdRemetente"] = new SelectList(_context.Usuario, "Id", "Id", amigo.IdRemetente);
            return View(amigo);
        }

        // GET: Amigo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amigo = await _context.Amigo.FindAsync(id);
            if (amigo == null)
            {
                return NotFound();
            }
            ViewData["IdDestinatario"] = new SelectList(_context.Usuario, "Id", "Id", amigo.IdDestinatario);
            ViewData["IdRemetente"] = new SelectList(_context.Usuario, "Id", "Id", amigo.IdRemetente);
            return View(amigo);
        }

        // POST: Amigo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRemetente,IdDestinatario")] Amigo amigo)
        {
            if (id != amigo.IdRemetente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amigo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmigoExists(amigo.IdRemetente))
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
            ViewData["IdDestinatario"] = new SelectList(_context.Usuario, "Id", "Id", amigo.IdDestinatario);
            ViewData["IdRemetente"] = new SelectList(_context.Usuario, "Id", "Id", amigo.IdRemetente);
            return View(amigo);
        }

        // GET: Amigo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amigo = await _context.Amigo
                .Include(a => a.UsuarioDestinatario)
                .Include(a => a.UsuarioRemetente)
                .FirstOrDefaultAsync(m => m.IdDestinatario == id);
            if (amigo == null)
            {
                return NotFound();
            }

            return View(amigo);
        }

        // POST: Amigo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var IdentityUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var IdentityUser = _context.Users.FirstOrDefault(u => u.Id == IdentityUserId);
            var usuarioLogado = _context.Usuario.FirstOrDefault(u => u.IdentityUser.Id == IdentityUserId);
            var amigo = await _context.Amigo.FindAsync(usuarioLogado.Id, id);
            _context.Amigo.Remove(amigo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Feed");
        }

        private bool AmigoExists(int id)
        {
            return _context.Amigo.Any(e => e.IdRemetente == id);
        }
        public async Task<IActionResult> Seguir(int id)
        {
            var amigo = new Amigo();
            if (ModelState.IsValid)
            {
                var IdentityUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var IdentityUser = _context.Users.FirstOrDefault(u => u.Id == IdentityUserId);
                var usuarioLogado = _context.Usuario.FirstOrDefault(u => u.IdentityUser.Id == IdentityUserId);
                var usuarioAmigo = _context.Usuario.FirstOrDefault(u => u.Id == id);
                amigo.IdRemetente = usuarioLogado.Id;
                amigo.IdDestinatario = id;
                amigo.UsuarioRemetente = usuarioLogado;
                amigo.UsuarioDestinatario = usuarioAmigo;
                _context.Add(amigo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Feed");
            }
            return View(amigo);
        }
        public IActionResult ListAmigo()
        {
            var IdentityUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var IdentityUser = _context.Users.FirstOrDefault(u => u.Id == IdentityUserId);
            var usuarioLogado = _context.Usuario.FirstOrDefault(u => u.IdentityUser.Id == IdentityUserId);
            var applicationDbContext = _context.Amigo.Include(a => a.UsuarioDestinatario).Include(a => a.UsuarioRemetente);
            ICollection<Usuario> usuarios = new List<Usuario>();
            foreach (var a in applicationDbContext)
                if (a.UsuarioRemetente == usuarioLogado)
                    usuarios.Add(a.UsuarioDestinatario);

            return View(usuarios);
        }
    }
}