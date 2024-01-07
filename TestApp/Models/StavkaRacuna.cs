namespace TestApp.Models
{
    public class StavkaRacuna
    {
        public int IdStavkeRacuna { get; set; }

        public string Naziv { get; set;}
        public decimal Kolicina { get; set;}

        public decimal JedinicnaCijena { get; set; }
        public int IdRacun { get; set; }
  
        public List<StavkaRacuna> stavkaRacuna { get; set; }


        public StavkaRacuna()
        {
            
        }

        public StavkaRacuna(int IdStavkeRacuna)
        {
            this.IdStavkeRacuna = IdStavkeRacuna + 1;

        }



    }
}
