import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Categorie } from '../../../services/api/models/categorie.model';
import { CategorieUpdateRequest } from '../../../services/api/models/categorie-update-request.model';
import { CategorieService } from '../../../services/api/categorie.service';

@Component({
  selector: 'app-categorie-modifier',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './categorie-modifier.component.html',
  styleUrls: ['./categorie-modifier.component.css']
})

export class CategorieModifierComponent {

  categorie$: Observable<Categorie>;
  categorieForm: FormGroup;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private categorieService: CategorieService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.categorieForm = this.fb.group({
      nomCategorie: ['', Validators.required],
      descriptif: ['']
    });

    this.categorie$ = this.categorieService.getCategorie(id);

    this.categorie$.subscribe(categorie => {
      this.categorieForm.patchValue({
        nomCategorie: categorie.nomCategorie,
        descriptif: categorie.descriptif ?? ''
      });
    });
  }

  enregistrer(categorie: Categorie): void {
    if (this.categorieForm.invalid) {
      this.categorieForm.markAllAsTouched();
      this.message = 'Veuillez remplir les champs obligatoires.';
      return;
    }

    const categorieUpdate: CategorieUpdateRequest = {
      idCategorie: categorie.idCategorie,
      nomCategorie: this.categorieForm.value.nomCategorie,
      descriptif: this.categorieForm.value.descriptif
    };

    this.categorieService.updateCategorie(categorie.idCategorie, categorieUpdate).subscribe({
      next: () => {
        this.router.navigate(['/categories']);
      },
      error: () => {
        this.message = 'Erreur pendant la modification de la catégorie.';
      }
    });
  }

  get nomCategorie() {
    return this.categorieForm.get('nomCategorie');
  }
}