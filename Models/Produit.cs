namespace GestionStock.API.Models
{
    public class Produit
    {
        public int Id {get; set;}
        public string Nom { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Categorie { get; set; } = string.Empty;
        public string? Fournisseur { get; set; }
        public int Stock { get; set; }
        public int SeuilAlerte { get; set; }  // alerte si stock < seuil
        public decimal Prix { get; set; }
        public DateTime DateAjout { get; set; } = DateTime.UtcNow;

    }
}