using TestApp.Models;

namespace TestApp.Services
{
    public class SkladisteServices
    {


        List<Skladiste> novoSkladiste = new List<Skladiste>();

        public NovaStavkaServices novaStavkaServices;

        public SkladisteServices()
        {

            SimulateDatabase();

        }

        public List<Skladiste> GetAllSkladista()
        {
            return novoSkladiste;
        }



        public Skladiste GetSkladisteById(int idStavke)
        {
            // Vraća skladište koje sadrži stavku s određenim IdStavke
            return novoSkladiste.FirstOrDefault(s => s.IdStavke == idStavke);
        }

        public void UpdateStavkeUSkladistu(int idStavke, int novaKolicina)
        {
            var stavkaUSkladistu = novoSkladiste.FirstOrDefault(s => s.IdStavke == idStavke);
            if (stavkaUSkladistu != null)
            {
                stavkaUSkladistu.KolicinaStavaka = novaKolicina;
            }
        }

        public void AddStavkaToSkladiste(Skladiste stavka)
        {
            novoSkladiste.Add(stavka);
        }
    


    public void SimulateDatabase()
        {

            Skladiste stavka1 = new Skladiste { IdSkladista =1, NazivSkladista = "Skladiste1", IdStavke = 1, KolicinaStavaka =23};
            Skladiste stavka2 = new Skladiste { IdSkladista =1, NazivSkladista = "Skladiste1", IdStavke = 2, KolicinaStavaka = 74 };
            novoSkladiste.Add(stavka1);
            novoSkladiste.Add(stavka2);
        }
    }
}
