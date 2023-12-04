namespace DUNPLab.API.Models
{
    public class RezultatOdMasine
    {
        public int Id { get; set; }
        public string ImeIPrezimeTestera { get; set; }
        public string KodEpruvete { get; set; }
        public string DatumVreme { get; set; }
        public bool JesuLiPrebaceni { get; set; }

        public ICollection<SupstancaRezultat> SupstanceRezultati { get; set; }
    }
}

