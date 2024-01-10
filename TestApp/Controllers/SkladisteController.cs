using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using TestApp.Services;

namespace TestApp.Controllers
{
    public class SkladisteController : Controller
    {
        public  SkladisteServices _skladisteServices;
        public NovaStavkaServices _novaStavkaServices;

        public SkladisteController(SkladisteServices skladisteServices, NovaStavkaServices novaStavkaServices)
        {
            _skladisteServices = skladisteServices;
            _novaStavkaServices = novaStavkaServices;
        }

        public IActionResult Index()
        {
            var skladista = _skladisteServices.GetAllSkladista().Where(s => s.KolicinaStavaka > 0).ToList();
            var sveStavke = _novaStavkaServices.GetAllStavka();

            foreach (var skladiste in skladista)
            {
                skladiste.NazivStavke = sveStavke.FirstOrDefault(stavka => stavka.IdStavke == skladiste.IdStavke)?.Naziv ?? "N/A";
            }

            ViewBag.NazivStavke = skladista.ToDictionary(s => s.IdStavke, s => s.NazivStavke);

            return View(skladista);
        }

        public IActionResult Add()
        {
            var sveStavke = _novaStavkaServices.GetAllStavka();
            var skladištaStavke = _skladisteServices.GetAllSkladista();

            var dostupneStavke = sveStavke.Where(stavka =>
                                    !skladištaStavke.Any(s => s.IdStavke == stavka.IdStavke && s.KolicinaStavaka > 0))
                                    .ToList();

            ViewData["DostupneStavke"] = dostupneStavke;

            return View();
        }
        [HttpPost]
        public IActionResult Add(Skladiste stavka)
        {
            _skladisteServices.AddStavkaToSkladiste(stavka);
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("Skladiste/Edit/{idStavke}")]
        public IActionResult Edit(int idStavke)
        {
            var stavka = _skladisteServices.GetSkladisteById(idStavke);
            if (stavka == null)
            {
                return NotFound();
            }

            return View(stavka);
        }

        [HttpPost]
        [Route("Skladiste/Edit/{idStavke}")]
        public IActionResult Edit(int idStavke, Skladiste updatedStavka)
        {
            var stavka = _skladisteServices.GetSkladisteById(idStavke);
            if (stavka == null)
            {
                return NotFound();
            }

            _skladisteServices.UpdateStavkeUSkladistu(idStavke, updatedStavka.KolicinaStavaka);
            return RedirectToAction("Index");
        }
    }
}