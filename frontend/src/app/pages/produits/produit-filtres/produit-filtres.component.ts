import { Component, input, model } from '@angular/core';

@Component({
  selector: 'app-produit-filtres',
  imports: [],
  templateUrl: './produit-filtres.component.html',
  styleUrls: ['./produit-filtres.component.css']
})

export class ProduitFiltresComponent {
  recherche = model<string>('');
  stockRecherche = model<string>('');
  prixMinRecherche = model<string>('');
  prixMaxRecherche = model<string>('');
  varieteRecherche = model<string>('');
  especeRecherche = model<string>('');

  varietes = input<string[]>([]);
  especes = input<string[]>([]);
  prixMinimumDisponible = input<number>(0);
  prixMaximumDisponible = input<number>(0);

  resetFiltres(): void {
    this.recherche.set('');
    this.stockRecherche.set('');
    this.prixMinRecherche.set('');
    this.prixMaxRecherche.set('');
    this.varieteRecherche.set('');
    this.especeRecherche.set('');
  }

  changerRecherche(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.recherche.set(input.value);
  }

  changerStockRecherche(event: Event): void {
    const select = event.target as HTMLSelectElement;
    this.stockRecherche.set(select.value);
  }

  changerPrixMinRecherche(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.prixMinRecherche.set(input.value);
  }

  changerPrixMaxRecherche(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.prixMaxRecherche.set(input.value);
  }

  changerVarieteRecherche(event: Event): void {
    const select = event.target as HTMLSelectElement;
    this.varieteRecherche.set(select.value);
  }

  changerEspeceRecherche(event: Event): void {
    const select = event.target as HTMLSelectElement;
    this.especeRecherche.set(select.value);
  }
}