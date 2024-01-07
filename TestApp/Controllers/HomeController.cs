using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestApp.Models;
using TestApp.Services;


namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        public DucanService ducanService;
        public RacuniServices racunService;
        public StavkaRacunaServices stavkaRacunaServices;

        
        public HomeController(DucanService ducanService, RacuniServices racunService, StavkaRacunaServices stavkaRacunaServices)
        {
            this.ducanService = ducanService;
            this.racunService = racunService;
            this.stavkaRacunaServices = stavkaRacunaServices;
        }
        //testtzu
        [HttpGet]
        public IActionResult Index()
        {
            var ducani = ducanService.GetAllDucani();
            foreach (var ducan in ducani)
            {
                var racuni = racunService.GetRacuniByDucanId(ducan.IdDucana);
                ducan.UkupanZbrojRacuna = racuni.Count;
            }
            return View(ducani);
        }

        [HttpGet]
        public IActionResult PopisRacuna(int id)
        {
           
            var racuni = racunService.GetRacuniByDucanId(id);
            var ducan = ducanService.GetDucanById(id);

            foreach (var racun in racuni)
            {
                var stavke = stavkaRacunaServices.GetStavkaRacunaByRacunId(racun.IdRacun);
                racun.UkupanZbrojStavakaRacuna = stavke.Count; //brojim stavke
            }
            ViewBag.IdDucana = id;
            ViewBag.NazivDucana = ducan != null ? ducan.NazivDucana : "Nepoznati dućan";
            ViewBag.AdresaDucana = ducan.AdresaDucana;
            return View(racuni);
        }

        [HttpGet]
        public IActionResult StavkeRacuna(int id)
        {
            var stavkeRacuna = stavkaRacunaServices.GetStavkaRacunaByRacunId(id);
            ViewBag.IdRacun = id;

            var racun = racunService.GetAllRacuni().FirstOrDefault(r => r.IdRacun == id);
            ViewBag.RacunZakljucan = racun.Zakljucan;

            
                var ducan = ducanService.GetDucanById(racun.IdDucana);
                ViewBag.IdDucana = racun.IdDucana;
            ViewBag.NazivDucana = ducan.NazivDucana;
            ViewBag.AdresaDucana = ducan.AdresaDucana;
          

            return View(stavkeRacuna);
        }

        [HttpPost]
        public IActionResult PopisRacunaPoDucanu(int IdDucana)
        {

          
            var racuni = racunService.GetRacuniByDucanId(IdDucana);
           
            
            return View("PopisRacuna", racuni);
        }
   
        //Novi ducan


        [HttpGet]
        public IActionResult NoviDucan()
        {
            return View(new Ducani());
        }

        [HttpPost]
        public IActionResult DodajNoviDucan(Ducani ducan)
        {
            ducanService.AddDucan(ducan);
            return RedirectToAction("Index");
        }

        //Novi Racun
        [HttpGet]
        public IActionResult NoviRacun(int idDucana)
        {
            var model = new Racun
            {
                IdDucana = idDucana,
                Datum = DateTime.Now // danasnji datum i vrijeme
            };
            var ducan = ducanService.GetDucanById(idDucana);
            ViewBag.IdDucana = idDucana;
            ViewBag.NazivDucana = ducan.NazivDucana;
            ViewBag.AdresaDucana = ducan.AdresaDucana;
            return View(model);
        }

        [HttpPost]
        public IActionResult DodajNoviRacun(Racun racun)
        {
            racunService.AddRacun(racun);
            return RedirectToAction("PopisRacuna", new { id = racun.IdDucana });
        }

        //Nove stavke
        [HttpGet]
        [HttpGet]
        public IActionResult NoveStavkeRacuna(int IdRacun) // Ovdje ime parametra mora odgovarati onome u URL-u
        {
            var model = new StavkaRacuna { IdRacun = IdRacun };
            return View(model);
        }
        [HttpPost]
       
        public IActionResult DodajNovuStavkuRacuna(StavkaRacuna stavkaRacuna)
        {
            //stavkaRacuna.IdRacun = 2;
            stavkaRacunaServices.AddStavkeRacuna(stavkaRacuna);
            //tu moram paziti na preusmjeravanje na ovaj id- on se gleda iz StavkeRacuna
            return RedirectToAction("StavkeRacuna", new { id = stavkaRacuna.IdRacun });
            

          
        }

        //Zakljucavnanje racuna + micanje dodavanja novih stavaka te editiranje stavaka
        [HttpPost]
        public IActionResult ZakljucajRacun(int id)
        {
            var racun = racunService.GetAllRacuni().FirstOrDefault(r => r.IdRacun == id);

            if (racun != null)
            {
                racunService.ZakljucajRacun(id);

                // Preusmjeravanje na popis računa dućana
                return RedirectToAction("PopisRacuna", new { id = racun.IdDucana });
            }
            else
            {
         //ak nema preusmjeri me na index stranicu
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult OtkljucajRacun(int id)
        {
            var racun = racunService.GetAllRacuni().FirstOrDefault(r => r.IdRacun == id);

            if (racun != null)
            {
                racunService.OtkljucajRacun(id);

                // Preusmjeravanje na popis računa dućana
                return RedirectToAction("PopisRacuna", new { id = racun.IdDucana });
            }
            else
            {
                //ak nema preusmjeri me na index stranicu
                return RedirectToAction("Index");
            }
        }
        //brisanje ducana + racuna + stavaka
        [HttpPost]
        public IActionResult DeleteDucan(int idDucana)
        {
            // Brisanje svih računa povezanih s dućanom
            var racuniZaBrisanje = racunService.GetRacuniByDucanId(idDucana);
            foreach (var racun in racuniZaBrisanje)
            {
                // Brisanje svih stavki povezanih s računom
                var stavkeZaBrisanje = stavkaRacunaServices.GetStavkaRacunaByRacunId(racun.IdRacun);
                foreach (var stavka in stavkeZaBrisanje)
                {
                    stavkaRacunaServices.DeleteStavkaRacuna(stavka.IdStavkeRacuna);
                }

                // Brisanje računa
                racunService.DeleteRacun(racun.IdRacun);
            }

            // Brisanje dućana
            ducanService.DeleteDucan(idDucana);

            return RedirectToAction("Index");
        }

        //brisanje racuna + stavaka
        [HttpPost]
        public IActionResult DeleteRacun(int idRacuna)
        {
            var racun = racunService.GetAllRacuni().FirstOrDefault(r => r.IdRacun == idRacuna);
            var stavkeZaBrisanje = stavkaRacunaServices.GetStavkaRacunaByRacunId(racun.IdRacun);
            foreach (var stavka in stavkeZaBrisanje)
            {
                stavkaRacunaServices.DeleteStavkaRacuna(stavka.IdStavkeRacuna);
            }
            racunService.DeleteRacun(idRacuna);
            return RedirectToAction("PopisRacuna", new { id = racun.IdDucana});
        }

        //brisanje stavke
        [HttpPost]
        public IActionResult DeleteStavkaRacuna(int idStavkeRacuna)
        {
            // Dohvatite stavku računa kako bi dobili ID računa prije brisanja
            var stavka = stavkaRacunaServices.GetAllStavkeRacuna().FirstOrDefault(s => s.IdStavkeRacuna == idStavkeRacuna);
            if (stavka != null)
            {
                stavkaRacunaServices.DeleteStavkaRacuna(idStavkeRacuna);

                // Preusmjeravanje na StavkeRacuna za određeni ID računa
                return RedirectToAction("StavkeRacuna", new { id = stavka.IdRacun });
            }
            else
            {
                // stavka nije pronadena odi na index
                return RedirectToAction("Index");
            }
        }

        //Edit stavaka po racunima po odredenom ducanu

        [HttpGet]
        public IActionResult EditStavke(int id)
        {
            var stavka = stavkaRacunaServices.GetAllStavkeRacuna().FirstOrDefault(s => s.IdStavkeRacuna == id);
            if (stavka == null)
            {
                return NotFound(); 
            }

            var stavkeRacuna = stavkaRacunaServices.GetStavkaRacunaByRacunId(id);
            ViewBag.IdRacun = id;
            
            var racun = racunService.GetAllRacuni().FirstOrDefault(r => r.IdRacun == id);
          

            /*Provjeriti zašto ovo ne radi na novo dodanom racunu - baca gresku da je racun null?????
          var ducan = ducanService.GetDucanById(racun.IdDucana);
          ViewBag.IdDucana = racun.IdDucana;
           ViewBag.NazivDucana = ducan.NazivDucana;
            ViewBag.AdresaDucana = ducan.AdresaDucana; */
            return View(stavka);
        }

        [HttpPost]
        public IActionResult UpdateStavkaRacuna(StavkaRacuna stavka)
        {
           
                stavkaRacunaServices.UpdateStavkaRacuna(stavka);
                return RedirectToAction("StavkeRacuna", new { id = stavka.IdRacun });
          
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}