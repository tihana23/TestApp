namespace TestApp.Models
{
    public class Skladiste
    {

        public int IdSkladista { get; set; }
        public string NazivSkladista { get; set; }

        public int IdStavke { get; set; }
        public string NazivStavke { get; set; }
        public int KolicinaStavaka { get; set; }

        public List<NovaStavka> stavka { get; set; }


        public Skladiste()
        {

        }
    }
}
