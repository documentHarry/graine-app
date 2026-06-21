import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Categorie } from '../../../services/api/models/categorie.model';
import { CategorieService } from '../../../services/api/categorie.service';
import { Variete } from '../../../services/api/models/variete.model';
import { VarieteService } from '../../../services/api/variete.service';
import { Produit } from '../../../services/api/models/produit.model';
import { ProduitService } from '../../../services/api/produit.service';
import { ProduitUpdateRequest } from '../../../services/api/models/produit-update-request.model';

@Component({
  selector: 'app-produit-modifier',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './produit-modifier.component.html',
  styleUrls: ['./produit-modifier.component.css']
})

export class ProduitModifierComponent {
  produit$: Observable<Produit>;
  categories$: Observable<Categorie[]>;
  varietes$: Observable<Variete[]>;

  produitForm: FormGroup;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private categorieService: CategorieService,
    private produitService: ProduitService,
    private varieteService: VarieteService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.produitForm = this.fb.group({
      intitule: ['', Validators.required],
      prixUnitaire: [0, [Validators.required, Validators.min(0.01)]],
      quantite: [0, [Validators.required, Validators.min(0)]],
      categorieId: [0, [Validators.required, Validators.min(1)]],
      varieteId: [0, [Validators.required, Validators.min(1)]]
    });

    this.produit$ = this.produitService.getProduit(id);
    this.categories$ = this.categorieService.getCategories();
    this.varietes$ = this.varieteService.getVarietes();

    this.produit$.subscribe(produit => {
      this.produitForm.patchValue({
        intitule: produit.intitule,
        prixUnitaire: produit.prixUnitaire,
        quantite: produit.quantite,
        categorieId: produit.categorieId,
        varieteId: produit.varieteId
      });
    });
  }

  enregistrer(produit: Produit): void {
    if (this.produitForm.invalid) {
      this.produitForm.markAllAsTouched();
      this.message = 'Veuillez remplir les champs obligatoires.';
      return;
    }

    const produitUpdate: ProduitUpdateRequest = {
      idProduit: produit.idProduit,
      intitule: this.produitForm.value.intitule.trim(),
      prixUnitaire: Number(this.produitForm.value.prixUnitaire),
      quantite: Number(this.produitForm.value.quantite),
      categorieId: Number(this.produitForm.value.categorieId),
      varieteId: Number(this.produitForm.value.varieteId)
    };

    this.produitService.updateProduit(
      produit.idProduit,
      produitUpdate
    ).subscribe({
      next: () => {
        this.router.navigate(['/produits', produit.idProduit]);
      },
      error: () => {
        this.message = this.produitService.getMessageErreurModification();
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