namespace TestApp.Models
{
    public class Ducani
    {
        public int IdDucana { get; set; }
        public string NazivDucana { get; set; }

        public string AdresaDucana { get; set; }
        public decimal UkupanZbrojRacuna { get; set; }

        public List<Racun> Racuni { get; set; }

        public Ducani()
        {
            this.Racuni = new List<Racun>();
        }

       

        public void DodajRacun(Racun racun)
        {
            Racuni.Add(racun);
        }

        

        public Ducani(string nazivDucana, string adresaDucana)
        {
            this.NazivDucana= nazivDucana;
            this.AdresaDucana = adresaDucana;


        }
      
}
}
