import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Observable } from 'rxjs';

import { CategorieService } from '../../services/api/categorie.service';
import { Categorie } from '../../services/api/models/categorie.model';
import { AuthService } from '../../services/api/auth.service';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})

export class CategoriesComponent {
  categories$: Observable<Categorie[]>;

  rechercheNom = '';
  rechercheDescriptif = '';
  error: string | null = null;

  constructor(
    private categorieService: CategorieService,
    public authService: AuthService) {
    this.categories$ = this.categorieService.getCategories();
  }

  filtrerCategories(categories: Categorie[]): Categorie[] {
    const nom = this.rechercheNom.toLowerCase().trim();
    const descriptif = this.rechercheDescriptif.toLowerCase().trim();

    return categories.filter(categorie => {
      const correspondNom =
        nom === '' ||
        categorie.nomCategorie.toLowerCase().includes(nom);

      const correspondDescriptif =
        descriptif === '' ||
        (categorie.descriptif ?? '').toLowerCase().includes(descriptif);

      return correspondNom && correspondDescriptif;
    });
  }
}