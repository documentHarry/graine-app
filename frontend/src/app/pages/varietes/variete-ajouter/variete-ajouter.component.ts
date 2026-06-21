import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { Espece } from '../../../services/api/models/espece.model';
import { EspeceService } from '../../../services/api/espece.service';
import { VarieteService } from '../../../services/api/variete.service';
import { VarieteCreateRequest } from '../../../services/api/models/variete-create-request.model';

@Component({
  selector: 'app-variete-ajouter',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './variete-ajouter.component.html',
  styleUrls: ['./variete-ajouter.component.css']
})
export class VarieteAjouterComponent {
  varieteForm: FormGroup;
  especes$: Observable<Espece[]>;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private especeService: EspeceService,
    private varieteService: VarieteService,
    private router: Router
  ) {
    this.varieteForm = this.fb.group({
      especeId: [0, [Validators.required, Validators.min(1)]],
      nom: ['', Validators.required],
      descriptif: [''],
      bio: [0, Validators.required],
      cycleJours: [null],
      couleurLegume: [''],
      tailleFixeLegume: [null],
      tailleMinLegume: [null],
      tailleMaxLegume: [null],
      espacementEntreLesPlants: [null],
      espacementEntreLesLignes: [null],
      typeEnsoleillement: [''],
      typeFeuillage: [''],
      hauteurAdulteMin: [null],
      hauteurAdulteMax: [null],
      dureeDeGermination: [''],
      temperatureMinDeGermination: [null],
      cycleDeVie: [''],
      rusticitePlante: [''],
      dateSemisMin: [''],
      dateSemisMax: [''],
      dureeAvantRecolte: [''],
      typeDeSol: [''],
      conseilPlantation: ['']
    });

    this.especes$ = this.especeService.getEspeces();
  }

  enregistrer(): void {
    if (this.varieteForm.invalid) {
      this.varieteForm.markAllAsTouched();
      this.message = 'Veuillez remplir les champs obligatoires.';
      return;
    }

    const variete: VarieteCreateRequest = this.convertirFormulaire();

    this.varieteService.addVariete(variete).subscribe({
      next: () => {
        this.router.navigate(['/varietes']);
      },
      error: (error) => {
        console.error('Erreur création variété', {
          error,
          formulaire: this.varieteForm.value,
          variete
        });

        this.message = this.varieteService.getMessageErreurCreation();
      }
    });
  }

  convertirFormulaire(): VarieteCreateRequest {
    const valeurFormulaire = this.varieteForm.value;

    return {
      especeId: Number(valeurFormulaire.especeId),
      nom: valeurFormulaire.nom?.trim() ?? '',
      descriptif: valeurFormulaire.descriptif?.trim() || null,
      bio: Number(valeurFormulaire.bio),
      cycleJours: valeurFormulaire.cycleJours,
      couleurLegume: valeurFormulaire.couleurLegume?.trim() || null,
      tailleFixeLegume: valeurFormulaire.tailleFixeLegume,
      tailleMinLegume: valeurFormulaire.tailleMinLegume,
      tailleMaxLegume: valeurFormulaire.tailleMaxLegume,
      espacementEntreLesPlants: valeurFormulaire.espacementEntreLesPlants,
      espacementEntreLesLignes: valeurFormulaire.espacementEntreLesLignes,
      typeEnsoleillement: valeurFormulaire.typeEnsoleillement?.trim() || null,
      typeFeuillage: valeurFormulaire.typeFeuillage?.trim() || null,
      hauteurAdulteMin: valeurFormulaire.hauteurAdulteMin,
      hauteurAdulteMax: valeurFormulaire.hauteurAdulteMax,
      dureeDeGermination: valeurFormulaire.dureeDeGermination?.trim() || null,
      temperatureMinDeGermination: valeurFormulaire.temperatureMinDeGermination,
      cycleDeVie: valeurFormulaire.cycleDeVie?.trim() || null,
      rusticitePlante: valeurFormulaire.rusticitePlante?.trim() || null,
      dateSemisMin: valeurFormulaire.dateSemisMin?.trim() || null,
      dateSemisMax: valeurFormulaire.dateSemisMax?.trim() || null,
      dureeAvantRecolte: valeurFormulaire.dureeAvantRecolte?.trim() || null,
      typeDeSol: valeurFormulaire.typeDeSol?.trim() || null,
      conseilPlantation: valeurFormulaire.conseilPlantation?.trim() || null
    };
  }

  get especeId() {
    return this.varieteForm.get('especeId');
  }

  get nom() {
    return this.varieteForm.get('nom');
  }
}