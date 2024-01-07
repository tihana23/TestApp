namespace TestApp.Models
{
    public class NovaStavka
    {
        public int IdStavke{ get; set; }

        public string Naziv { get; set; }
    
        public decimal JedinicnaCijena { get; set; }
  

        public List<NovaStavka> stavka { get; set; }


        public NovaStavka()
        {

        }

        public NovaStavka(int IdStavke)
        {
            this.IdStavke = IdStavke + 1;

        }

        public NovaStavka(string nazivStavke, decimal jedinicnaCijena)
        {
            this.Naziv = nazivStavke;
            this.JedinicnaCijena = jedinicnaCijena;


        }

    }
}
  