using TestApp.Models;

namespace TestApp.Services
{
    public class StavkaRacunaServices
    {

            public List<StavkaRacuna> stavkaRacuna = new List<StavkaRacuna>();

        public StavkaRacunaServices()
        {
            SimulateDatabase();
        }

        public List<StavkaRacuna> GetAllStavkeRacuna()
            {
                return stavkaRacuna;
            }
            public List<StavkaRacuna> GetStavkaRacunaByRacunId(int racunId)
            {
                return stavkaRacuna.Where(r => r.IdRacun == racunId).ToList();
            }

        public void AddStavkeRacuna(StavkaRacuna novaStavka)
        {
            if (stavkaRacuna.Any())
            {
                novaStavka.IdStavkeRacuna = stavkaRacuna.Max(s => s.IdStavkeRacuna) + 1; // Automatsko povećanje ID-a
            }
            else
            {
                novaStavka.IdStavkeRacuna = 1; // Prva stavka u listi
            }

            stavkaRacuna.Add(novaStavka); // Dodavanje nove stavke u listu
        
    }
        public void DeleteStavkaRacuna(int idStavkeRacuna)
        {
            var stavka = stavkaRacuna.FirstOrDefault(s => s.IdStavkeRacuna == idStavkeRacuna);
            if (stavka != null)
            {
                stavkaRacuna.Remove(stavka);
            }
        }

        public void UpdateStavkaRacuna(StavkaRacuna updatedStavka)
        {
            var stavka = stavkaRacuna.FirstOrDefault(s => s.IdStavkeRacuna == updatedStavka.IdStavkeRacuna);
            if (stavka != null)
            {
                // Ažuriranje podataka stavke
                stavka.Naziv = updatedStavka.Naziv;
                stavka.Kolicina = updatedStavka.Kolicina;
                stavka.JedinicnaCijena = updatedStavka.JedinicnaCijena;
               
            }
        }

      
        public void SimulateDatabase()
            {
                

            StavkaRacuna stavka1 = new StavkaRacuna { IdRacun = 1, IdStavkeRacuna =1,Naziv="Brašno",Kolicina=5,JedinicnaCijena=2 };
            StavkaRacuna stavka2 = new StavkaRacuna { IdRacun = 1, IdStavkeRacuna = 2, Naziv = "Pizza", Kolicina = 7, JedinicnaCijena = 4 };
            StavkaRacuna stavka3 = new StavkaRacuna { IdRacun = 1, IdStavkeRacuna = 3, Naziv = "Kruh", Kolicina = 11, JedinicnaCijena = 12 };

            stavkaRacuna.Add(stavka1);
            stavkaRacuna.Add(stavka2);
            stavkaRacuna.Add(stavka3);

        }
        }
    }

