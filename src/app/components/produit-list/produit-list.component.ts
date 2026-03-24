import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProduitService } from '../../services/produit.service';
import { Produit } from '../../models/produit';

@Component({
  selector: 'app-produit-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './produit-list.html',
  styleUrl: './produit-list.scss'
})
export class ProduitListComponent implements OnInit {
  produits: Produit[] = [];
  search = '';
  categorieFiltre = '';
  categories = ['Électronique', 'Périphériques', 'Fournitures', 'Mobilier'];
  showModal = false;
  isEdit = false;
  produitForm: Partial<Produit> = {};

  constructor(
    public produitService: ProduitService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() { this.loadProduits(); }

  loadProduits() {
    this.produitService.getProduits(this.search, this.categorieFiltre)
      .subscribe(p => {
        this.produits = p;
        this.cdr.detectChanges();
      });
  }

  onSearch() { this.loadProduits(); }

  getStatut(p: Produit): string {
    if (p.stock === 0) return 'rupture';
    if (p.stock <= p.seuilAlerte) return 'alerte';
    return 'ok';
  }

  ouvrirAjout() {
    this.isEdit = false;
    this.produitForm = { stock: 0, seuilAlerte: 5, prix: 0 };
    this.showModal = true;
  }

  ouvrirEdit(p: Produit) {
    this.isEdit = true;
    this.produitForm = { ...p };
    this.showModal = true;
  }

  fermerModal() { this.showModal = false; }

  sauvegarder() {
    if (this.isEdit && this.produitForm.id) {
      this.produitService.updateProduit(this.produitForm.id, this.produitForm)
        .subscribe(() => { this.fermerModal(); this.loadProduits(); });
    } else {
      this.produitService.createProduit(this.produitForm)
        .subscribe(() => { this.fermerModal(); this.loadProduits(); });
    }
  }

  supprimer(id: number) {
    if (confirm('Supprimer ce produit ?')) {
      this.produitService.deleteProduit(id)
        .subscribe(() => this.loadProduits());
    }
  }
}