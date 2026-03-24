import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ProduitService } from '../../services/produit.service';
import { ProduitStats, Produit } from '../../models/produit';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class DashboardComponent implements OnInit {
  stats: ProduitStats | null = null;
  alertes: Produit[] = [];

  constructor(
    private produitService: ProduitService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.produitService.getStats().subscribe(s => {
      this.stats = s;
      this.cdr.detectChanges();
    });
    this.produitService.getAlertes().subscribe(a => {
      this.alertes = a;
      this.cdr.detectChanges();
    });
  }
}