using TestApp.Models;


namespace TestApp.Services
{
    public class RacuniServices
    {


          public List<Racun> racuni = new List<Racun>();
                public readonly StavkaRacunaServices stavkaRacunaServices;


        public RacuniServices()
        {
            this.stavkaRacunaServices = new StavkaRacunaServices();
                   
                    SimulateDatabase();
                }

        public List<Racun> GetAllRacuni()
        {
            return racuni;
        }

        public List<Racun> GetRacuniByDucanId(int idDucana)
        {
            return racuni.Where(r => r.IdDucana == idDucana).ToList();
        }


        public void AddRacun(Racun noviRacun)
        {
            if (racuni.Count == 0)
            {
                noviRacun.IdRacun = 1; // Ako je lista prazna, počnite s ID 1
            }
            else
            {
                noviRacun.IdRacun = racuni.Max(d => d.IdRacun) + 1; // povecaj postojeći ID za 1
            }
            racuni.Add(noviRacun);
        }

        public void ZakljucajRacun(int idRacun)
        {
            var racun = racuni.FirstOrDefault(r => r.IdRacun == idRacun);
            if (racun != null)
            {
                racun.Zakljucan = true;
            }
        }

        public void OtkljucajRacun(int idRacun)
        {
            var racun = racuni.FirstOrDefault(r => r.IdRacun == idRacun);
            if (racun != null)
            {
                racun.Zakljucan = false;
            }
        }
        public void DeleteRacun(int idRacuna)
        {
            var racun = racuni.FirstOrDefault(r => r.IdRacun == idRacuna);
            if (racun != null)
            {
                racuni.Remove(racun);
            }
        }
      
        private void SimulateDatabase()
            {
                

                Racun racun1 = new Racun { IdRacun = 1, Datum = System.DateTime.Now, IdDucana = 1 };
                Racun racun2 = new Racun { IdRacun = 2, Datum = System.DateTime.Now, IdDucana = 1 };
                Racun racun3 = new Racun { IdRacun = 3, Datum = System.DateTime.Now, IdDucana = 1 };
                Racun racun4 = new Racun { IdRacun = 4, Datum = System.DateTime.Now, IdDucana = 2 };

                racuni.Add(racun1);
                racuni.Add(racun2);
                racuni.Add(racun3);
                racuni.Add(racun4);
                }
        }
    }

