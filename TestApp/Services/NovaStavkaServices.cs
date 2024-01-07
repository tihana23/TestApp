using TestApp.Models;

namespace TestApp.Services
{
    public class NovaStavkaServices
    {
        List<NovaStavka> novaStavkas = new List<NovaStavka>();

        public NovaStavkaServices() {

            SimulateDatabase();

        }


        public List<NovaStavka> GetAllStavka()
        {

            return novaStavkas;
        }

        public NovaStavka GetNovaStavkaById(int id)
        {
            return novaStavkas.FirstOrDefault(d => d.IdStavke == id);
        }

        public void AddStavka(NovaStavka novaStavka)
        { 
            if (novaStavkas.Count == 0)
            {
                novaStavka.IdStavke = 1; // Ako je lista prazna, počnite s ID 1
            }
            else
            {
                novaStavka.IdStavke = novaStavkas.Max(d => d.IdStavke) + 1; //  povećaj najveći postojeći ID za 1
            }
            novaStavkas.Add(novaStavka);
        }


        public void DeleteStavka(int idStavke)
        {
            var stavka = novaStavkas.FirstOrDefault(s => s.IdStavke == idStavke);
            if (stavka != null)
            {
                novaStavkas.Remove(stavka);
            }
        }

        public void UpdateStavka(NovaStavka updatedStavka)
        {
            var stavka = novaStavkas.FirstOrDefault(s => s.IdStavke == updatedStavka.IdStavke);
            if (stavka != null)
            {
                stavka.Naziv = updatedStavka.Naziv;
                stavka.JedinicnaCijena = updatedStavka.JedinicnaCijena;
            }
        }

        private void SimulateDatabase()
        {


            NovaStavka stavka1 = new NovaStavka { IdStavke = 1, Naziv = "Kruhic", JedinicnaCijena =22 };



            novaStavkas.Add(stavka1);
        }
    }
}

