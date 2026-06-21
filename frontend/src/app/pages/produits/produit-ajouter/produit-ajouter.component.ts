import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { Categorie } from '../../../services/api/models/categorie.model';
import { CategorieService } from '../../../services/api/categorie.service';
import { Variete } from '../../../services/api/models/variete.model';
import { VarieteService } from '../../../services/api/variete.service';
import { ProduitService } from '../../../services/api/produit.service';
import { ProduitCreateRequest } from '../../../services/api/models/produit-create-request.model';

@Component({
  selector: 'app-produit-ajouter',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './produit-ajouter.component.html',
  styleUrls: ['./produit-ajouter.component.css']
})

export class ProduitAjouterComponent {
  produitForm: FormGroup;

  categories$: Observable<Categorie[]>;
  varietes$: Observable<Variete[]>;

  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private categorieService: CategorieService,
    private varieteService: VarieteService,
    private produitService: ProduitService,
    private router: Router
  ) {
    this.produitForm = this.fb.group({
      intitule: ['', Validators.required],
      prixUnitaire: [0, [Validators.required, Validators.min(0.01)]],
      quantite: [0, [Validators.required, Validators.min(0)]],
      categorieId: [0, [Validators.required, Validators.min(1)]],
      varieteId: [0, [Validators.required, Validators.min(1)]]
    });

    this.categories$ = this.categorieService.getCategories();
    this.varietes$ = this.varieteService.getVarietes();
  }

  enregistrer(): void {
    if (this.produitForm.invalid) {
      this.produitForm.markAllAsTouched();
      this.message = 'Veuillez remplir les champs obligatoires.';
      return;
    }

    const produit: ProduitCreateRequest = {
      intitule: this.produitForm.value.intitule.trim(),
      prixUnitaire: Number(this.produitForm.value.prixUnitaire),
      quantite: Number(this.produitForm.value.quantite),
      categorieId: Number(this.produitForm.value.categorieId),
      varieteId: Number(this.produitForm.value.varieteId)
    };

    this.produitService.addProduit(produit).subscribe({
      next: () => {
        this.router.navigate(['/produits']);
      },
      error: () => {
        this.message = this.produitService.getMessageErreurCreation();
      }
    });
  }

  get intitule() {
    return this.produitForm.get('intitule');
  }

  get prixUnitaire() {
    return this.produitForm.get('prixUnitaire');
  }

  get quantite() {
    return this.produitForm.get('quantite');
  }

  get categorieId() {
    return this.produitForm.get('categorieId');
  }

  get varieteId() {
    return this.produitForm.get('varieteId');
  }
}