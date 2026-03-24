import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Produit, ProduitStats } from '../models/produit';

@Injectable({ providedIn: 'root' })
export class ProduitService {
  private apiUrl = 'http://localhost:5161/api/Produits';

  constructor(private http: HttpClient) {}

  getProduits(search?: string, categorie?: string): Observable<Produit[]> {
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    if (categorie) params = params.set('categorie', categorie);
    return this.http.get<Produit[]>(this.apiUrl, { params });
  }

  getProduit(id: number): Observable<Produit> {
    return this.http.get<Produit>(`${this.apiUrl}/${id}`);
  }

  getStats(): Observable<ProduitStats> {
    return this.http.get<ProduitStats>(`${this.apiUrl}/stats`);
  }

  getAlertes(): Observable<Produit[]> {
    return this.http.get<Produit[]>(`${this.apiUrl}/alertes`);
  }

  createProduit(produit: Partial<Produit>): Observable<Produit> {
    return this.http.post<Produit>(this.apiUrl, produit);
  }

  updateProduit(id: number, produit: Partial<Produit>): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, produit);
  }

  deleteProduit(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  exportExcel(): void {
  window.open(`${this.apiUrl}/export`, '_blank');
}
}