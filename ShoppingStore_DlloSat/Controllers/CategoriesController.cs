using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingStore_DlloSat.DAL;
using ShoppingStore_DlloSat.DAL.Entities;

namespace ShoppingStore_DlloSat.Controllers
{
    public class CategoriesController : Controller
    {

        //Se crea el parámetro readonly que es el que se va a manejar para el resto de la clase: _context 
        private readonly DataBaseContext _context;

        //Patrón de diseño llamado INYECCIÓN DE DEPENDECIAS
        public CategoriesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()//Métodos es igual a acciones del controlador
        {
            return _context.Categories != null ?
                        View(await _context.Categories.ToListAsync()) ://El método ToListAsync() sirve para consultar una lista
                        Problem("Entity set 'DataBaseContext.Categories'  is null.");//IF ternario. El signo ? = ENTONCES, los : = SINO 
        }

        // GET: Categories/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);//Esta es la línea que trae los 20 M de Categories
            //El método FirstOrDefaultAsync() sirve para consultar un objeto y se usa con notación landa (c => c.Id == id)
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create (Obtener) //GET = SELECT
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create (Crear) //POST = CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)//Captura el objeto category con sus propiedades
        {
            if (ModelState.IsValid)
            {
                try
                {
                    category.Id = Guid.NewGuid();
                    category.CreateDate = DateTime.Now;//Aquí se automatiza el CreateDate de un objeto
                    _context.Add(category);//Método Add() crea una BD
                    await _context.SaveChangesAsync();//Aquí va a la capa MODEL y GUARDA la categoría en la tabla Categories
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una categoría con el mismo nombre");
                    }
                }
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);//Aquí voy a la BD y traigo esa categoría                        ssssscon ese id
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category.ModifieDate = DateTime.Now;//Se automatiza la fecha de moficación de la tabla Categories
                    _context.Update(category);//Método Update() actualiza obj en BD
                    await _context.SaveChangesAsync();  //Aquí se hace el update en BD
                                                        //También va la capa MODEL y GUARDA la categoría en la tabla Categories
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una categoría con el mismo nombre");
                    }
                }
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'DataBaseContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
            {
                _context.Categories.Remove(category);//El método Remove () es para eliminar la categoría
            }

            await _context.SaveChangesAsync();//Se elimina la categoría en la BD 
            return RedirectToAction(nameof(Index));//Se redirecciona al index de categoría
        }

        private bool CategoryExists(Guid id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
