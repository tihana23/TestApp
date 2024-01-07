using TestApp.Models;



namespace TestApp.Services
{
    public class DucanService
    {
        public List<Ducani> ducani = new List<Ducani>();
        public RacuniServices racunService;
        public DucanService()
        {
            this.racunService = new RacuniServices();
            // Inicijalizacija s testnim podacima
            SimulateDatabase();
        }

        public List<Ducani> GetAllDucani()
        {
        
            return ducani;
        }



            public Ducani GetDucanById(int id)
        {
            return ducani.FirstOrDefault(d => d.IdDucana == id);
        }

        
        public void AddDucan(Ducani noviDucan)
        {
            if (ducani.Count == 0)
            {
                noviDucan.IdDucana = 1; // Ako je lista prazna, počnite s ID 1
            }
            else
            {
                noviDucan.IdDucana = ducani.Max(d => d.IdDucana) + 1; //  povećaj najveći postojeći ID za 1
            }
            ducani.Add(noviDucan);
        }

        public void Update(int idDucan, Ducani noviDucan) {

            var trenutniDucan = GetDucanById(idDucan);
            trenutniDucan = noviDucan;
            ducani.Remove(trenutniDucan);
            ducani.Add(noviDucan);
        
        }


        //brisanje ducana
        public void DeleteDucan(int idDucana)
        {
            var ducan = ducani.FirstOrDefault(d => d.IdDucana == idDucana);
            if (ducan != null)
            {
                ducani.Remove(ducan);
            }
        }


        /*
         public Ducani GetDucanByIdBroj(int id)
         {
             var ducan = ducani.FirstOrDefault(d => d.IdDucana == id);
             if (ducan != null)
             {
                 var racuni = racunService.GetRacuniByDucanId(ducan.IdDucana);
                 ducan.UkupanZbrojRacuna = racuni.Count;
             }
             return ducan;
         }
         */


        private void SimulateDatabase() {

            Ducani ducani1 = new Ducani()

            {
                IdDucana = 1,
                NazivDucana = "Konzum",
                AdresaDucana = "Sarajevska 22",
                

            };

            Ducani ducani2 = new Ducani()

            {
                IdDucana = 2,
                NazivDucana = "Konzum",
                AdresaDucana = "Radnicka 322"

            };

            Ducani ducani3 = new Ducani()

            {
                IdDucana = 3,
                NazivDucana = "Studenac",
                AdresaDucana = "Karla Metikose 2"

            };

            ducani.Add(ducani1);
            ducani.Add(ducani2);
            ducani.Add(ducani3);
          

           

        }

    }






}
