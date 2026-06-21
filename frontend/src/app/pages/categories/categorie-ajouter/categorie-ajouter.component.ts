import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CategorieService } from '../../../services/api/categorie.service';
import { CategorieCreateRequest } from '../../../services/api/models/categorie-create-request.model';

@Component({
  selector: 'app-categorie-ajouter',
  imports: [ ReactiveFormsModule, ],
  templateUrl: './categorie-ajouter.component.html',
  styleUrls: ['./categorie-ajouter.component.css']
})

export class CategorieAjouterComponent implements OnInit {

  categorieForm!: FormGroup;
  message: string | null = null;

  constructor( private fb: FormBuilder, private categorieService: CategorieService, private router: Router) { }

  ngOnInit(): void {

    this.categorieForm = this.fb.group({ 
      nomCategorie: [ '', Validators.required ],
      descriptif: [ '' ]
    });
  }

  enregistrer(): void {
    if (this.categorieForm.invalid) {
      this.categorieForm.markAllAsTouched();
      return;
    }

    const categorie: CategorieCreateRequest = this.categorieForm.value;

    this.categorieService.addCategorie(categorie)
      .subscribe({
        next: () => {
          this.router.navigate([ '/categories' ]);
        },
        error: () => {
          this.message = 'Erreur lors de la création de la catégorie.';
        }
      });
  }

  get nomCategorie() {
    return this.categorieForm.get( 'nomCategorie' );
  }
}