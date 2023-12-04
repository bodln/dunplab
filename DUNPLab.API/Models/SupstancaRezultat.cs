namespace DUNPLab.API.Models
{
    public class SupstancaRezultat
    {
        public int Id { get; set; }
        public string OznakaSupstance { get; set; }
        public double Vrednost { get; set; }
        public bool JeLiBiloGreske { get; set; }
        public int IdRezultataOdMasine { get; set; }
        public RezultatOdMasine RezultatOdMasine { get; set; }
    }
}
