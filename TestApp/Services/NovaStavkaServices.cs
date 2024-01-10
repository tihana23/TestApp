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

        public void AddNovaStavka(NovaStavka novaStavka)
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


        public void DeleteStavka(int id)
        {
            var stavka = novaStavkas.FirstOrDefault(s => s.IdStavke == id);
            
                novaStavkas.Remove(stavka);
            
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

        public void SimulateDatabase()
        {


            NovaStavka stavka1 = new NovaStavka { IdStavke = 1, Naziv = "Kruhic", JedinicnaCijena =22 };
            NovaStavka stavka2 = new NovaStavka { IdStavke = 2, Naziv = "jaja", JedinicnaCijena = 27};
            NovaStavka stavka3 = new NovaStavka { IdStavke = 3, Naziv = "Maslac", JedinicnaCijena = 27 };

            novaStavkas.Add(stavka1);
            novaStavkas.Add(stavka2);
            novaStavkas.Add(stavka3);
        }
}

}