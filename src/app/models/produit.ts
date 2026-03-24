export interface Produit {
  id: number;
  nom: string;
  description?: string;
  categorie: string;
  fournisseur?: string;
  stock: number;
  seuilAlerte: number;
  prix: number;
  dateAjout: string;
}

export interface ProduitStats {
  totalProduits: number;
  enAlerte: number;
  enRupture: number;
  valeurTotale: number;
}