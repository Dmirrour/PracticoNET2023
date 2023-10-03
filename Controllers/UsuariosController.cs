using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PractNET_2023.Models;
using PractNET_2023.Models.Clases;
using System.Linq;
using X.PagedList;



namespace PractNET_2023.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly equipoDbContext _context;

        public UsuariosController(equipoDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        /*public async Task<IActionResult> Index()
        {
            return _context.Usuarios != null ?
                        View(await _context.Usuarios.ToListAsync()) :
                        Problem("Entity set 'equipoDbContext.Usuarios'  is null.");
        }*/

        /*public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1; // Si no se especifica la página, mostrar la primera
            int pageSize = 10; // Define el número de elementos por página

            var pagedUsers = _context.Usuarios
                .OrderBy(u => u.Id)
                .ToPagedList(pageNumber, pageSize);

            return View(pagedUsers);
        }*/
        // GET: busqueda

        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                var allUsers = await _context.Usuarios.ToListAsync();
                return View("Index", allUsers);
            }

            var results = await _context.Usuarios
                .Where(u => EF.Functions.Like(u.Name, "%" + searchTerm + "%") || EF.Functions.Like(u.Nickname, "%" + searchTerm + "%") || EF.Functions.Like(u.Descripcion, "%" + searchTerm + "%"))
                .ToListAsync();

            return View("Index", results);
        }

        public async Task<IActionResult> Index(string searchTerm, int? page)
        {
            int pageNumber = page ?? 1; // Página predeterminada si no se especifica
            int pageSize = 10; // Número de elementos por página

            IQueryable<Usuarios> query = _context.Usuarios;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u => EF.Functions.Like(u.Name, "%" + searchTerm + "%") || EF.Functions.Like(u.Nickname, "%" + searchTerm + "%") || EF.Functions.Like(u.Descripcion, "%" + searchTerm + "%"));
            }

            var pagedUsers = query.OrderBy(u => u.Id).ToPagedList(pageNumber, pageSize);

            return View(pagedUsers);
        }





        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Nickname,FechaDeNacimiento,Descripcion")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Nickname,FechaDeNacimiento,Descripcion")] Usuarios usuarios)
        {
            if (id != usuarios.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.Id))
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
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'equipoDbContext.Usuarios'  is null.");
            }
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios != null)
            {
                _context.Usuarios.Remove(usuarios);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
