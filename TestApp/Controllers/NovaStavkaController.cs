using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using TestApp.Services;

namespace TestApp.Controllers
{
    public class NovaStavkaController : Controller
    {
        private NovaStavkaServices _novaStavkaServices;

        public NovaStavkaController()
        {
            _novaStavkaServices = new NovaStavkaServices();
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
            
                _novaStavkaServices.AddStavka(novaStavka);
            
            
            return View(novaStavka);
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
        public IActionResult DeleteStavke(int idStavke)
        {
            // Dohvatite stavku računa kako bi dobili ID računa prije brisanja
            var stavka = _novaStavkaServices.GetAllStavka().FirstOrDefault(s => s.IdStavke == idStavke);
          
                _novaStavkaServices.DeleteStavka(idStavke);

                // Preusmjeravanje na StavkeRacuna za određeni ID računa
              
          
                // stavka nije pronadena odi na index
                return RedirectToAction("Index");
            
        }
    }
}

