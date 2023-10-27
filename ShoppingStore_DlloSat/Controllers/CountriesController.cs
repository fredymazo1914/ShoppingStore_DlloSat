using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingStore_DlloSat.DAL;
using ShoppingStore_DlloSat.DAL.Entities;

namespace ShoppingStore_DlloSat.Controllers
{
    public class CountriesController : Controller
    {
        //Se crea el parámetro readonly que es el que se va a manejar para el resto de la clase: _context 
        private readonly DataBaseContext _context;

        //Patrón de diseño llamado INYECCIÓN DE DEPENDECIAS
        public CountriesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()//Métodos es igual a acciones del controlador
        {
              return _context.Countries != null ? 
                          View(await _context.Countries.ToListAsync()) ://El método ToListAsync() sirve para consultar una lista
                          Problem("Entity set 'DataBaseContext.Countries'  is null.");//IF ternario. El signo ? = ENTONCES, los : = SINO 
        }

        // GET: Countries/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);//Esta es la línea que trae los 20 M de Countries
            //El método FirstOrDefaultAsync() sirve para consultar un objeto y se usa con notación landa (c => c.Id == id)
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create (Obtener) //GET = SELECT
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create (Crear) //POST = CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Country country)//Captura el objeto country con sus propiedades
        {
            if (ModelState.IsValid)
            {
                try
                {
                    country.Id = Guid.NewGuid();
                    country.CreateDate = DateTime.Now;//Aquí se automatiza el CreateDate de un objeto
                    _context.Add(country);//Método Add() crea una BD
                    await _context.SaveChangesAsync();//Aquí va a la capa MODEL y GUARDA el país en la tabla Countries
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if(ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre");
                    }
                }
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);//Aquí voy a la BD y traigo ese país con ese id
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
       [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    country.ModifieDate = DateTime.Now;//Se automatiza la fecha de moficación de la tabla Countries
                    _context.Update(country);//Método Update() actualiza obj en BD
                    await _context.SaveChangesAsync();  //Aquí se hace el update en BD
                                                        //También va la capa MODEL y GUARDA el país en la tabla Countries
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre");
                    }
                }
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'DataBaseContext.Countries'  is null.");
            }
            var country = await _context.Countries.FindAsync(id);
            
            if (country != null)
            {
                _context.Countries.Remove(country);//El método Remove () es para eliminar el país
            }
            
            await _context.SaveChangesAsync();//Se elimina el país en la BD 
            return RedirectToAction(nameof(Index));//Se redirecciona al index de país
        }

        private bool CountryExists(Guid id)
        {
          return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
