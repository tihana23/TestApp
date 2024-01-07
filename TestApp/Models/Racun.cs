namespace TestApp.Models
{
    public class Racun
    {
        
            public int IdRacun { get; set; }
            public DateTime Datum { get; set; }
            public int IdDucana { get; set; } // ID dućana kojem račun pripada
        public decimal UkupanZbrojStavakaRacuna { get; set; }

        public bool Zakljucan { get; set; }

        public List<StavkaRacuna> StavkaRacuna { get; set; }
        public Racun()
            {
                this.StavkaRacuna = new List<StavkaRacuna>();
            }

            public Racun(int idRacun)
        {
            this.IdRacun = idRacun + 1;

        }
    }
}
