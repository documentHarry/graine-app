import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable, map } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Categorie } from '../../../services/api/models/categorie.model';
import { CategorieService } from '../../../services/api/categorie.service';

@Component({
  selector: 'app-categorie-supprimer',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './categorie-supprimer.component.html',
  styleUrls: ['./categorie-supprimer.component.css']
})

export class CategorieSupprimerComponent {
  categorie$: Observable<Categorie>;
  categories$: Observable<Categorie[]>;

  reaffectationForm: FormGroup;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private categorieService: CategorieService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.reaffectationForm = this.fb.group({
      categorieDestinationId: [0, [Validators.required, Validators.min(1)]]
    });

    this.categorie$ = this.categorieService.getCategorie(id);

    this.categories$ = this.categorieService.getCategories().pipe(
      map(categories =>
        categories.filter(categorie => categorie.idCategorie !== id)
      )
    );
  }

  supprimerCategorie(categorie: Categorie): void {
    const confirmation = confirm('Voulez-vous vraiment supprimer cette catégorie ?');

    if (!confirmation) {
      return;
    }

    this.categorieService.deleteCategorie(categorie.idCategorie).subscribe({
      next: () => {
        this.router.navigate(['/categories']);
      },
      error: () => {
        this.message = 'Erreur pendant la suppression de la catégorie.';
      }
    });
  }

  supprimerAvecReaffectation(categorie: Categorie): void {
    if (this.reaffectationForm.invalid) {
      this.reaffectationForm.markAllAsTouched();
      this.message = 'Veuillez choisir une catégorie de réaffectation.';
      return;
    }

    const idDestination = Number(this.reaffectationForm.value.categorieDestinationId);

    const confirmation = confirm('Les produits seront réaffectés à la catégorie choisie. Confirmer la suppression ?');

    if (!confirmation) {
      return;
    }

    this.categorieService.deleteCategorieWithReaffectation(categorie.idCategorie, idDestination).subscribe({
      next: () => {
        this.router.navigate(['/categories']);
      },
      error: () => {
        this.message = 'Erreur pendant la réaffectation.';
      }
    });
  }

  annuler(): void {
    this.router.navigate(['/categories']);
  }

  get categorieDestinationId() {
    return this.reaffectationForm.get('categorieDestinationId');
  }
}