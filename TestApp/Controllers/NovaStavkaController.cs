using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using TestApp.Services;

namespace TestApp.Controllers
{
    public class NovaStavkaController : Controller
    {
        public NovaStavkaServices _novaStavkaServices;

        public NovaStavkaController(NovaStavkaServices novaStavkaServices)
        {
            _novaStavkaServices = novaStavkaServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var stavke = _novaStavkaServices.GetAllStavka();
            return View(stavke);
        }
        [HttpGet]
        public IActionResult Add()
        {

            return View(new NovaStavka());
        }

     
        [HttpPost]
        public IActionResult AddNovaStavka(NovaStavka novaStavka)
        {
            
                _novaStavkaServices.AddNovaStavka(novaStavka);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var stavka = _novaStavkaServices.GetAllStavka().FirstOrDefault(s => s.IdStavke == id);
            if (stavka == null)
            {
                return NotFound();
            }
            return View(stavka);
        }


        [HttpPost]
        public IActionResult Edit(NovaStavka updatedStavka)
        {
            _novaStavkaServices.UpdateStavka(updatedStavka);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteStavka(int id)
        {
            var stavka = _novaStavkaServices.GetNovaStavkaById(id);

            if (stavka != null)
            {
                _novaStavkaServices.DeleteStavka(id);
            }
            else
            {
                // Ako stavka ne postoji, možete vratiti grešku ili preusmjeriti na drugu stranicu
                return NotFound(); // Ili neki drugi odgovarajući odgovor
            }

            // Nakon brisanja stavke, preusmjerite na stranicu koja prikazuje sve stavke
            return RedirectToAction("Index");
        }
    }
    }


