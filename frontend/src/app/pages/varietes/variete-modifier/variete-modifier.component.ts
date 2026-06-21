import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Espece } from '../../../services/api/models/espece.model';
import { EspeceService } from '../../../services/api/espece.service';
import { Variete } from '../../../services/api/models/variete.model';
import { VarieteService } from '../../../services/api/variete.service';
import { VarieteUpdateRequest } from '../../../services/api/models/variete-update-request.model';

@Component({
  selector: 'app-variete-modifier',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './variete-modifier.component.html',
  styleUrls: ['./variete-modifier.component.css']
})
export class VarieteModifierComponent {
  variete$: Observable<Variete>;
  especes$: Observable<Espece[]>;

  varieteForm: FormGroup;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private especeService: EspeceService,
    private varieteService: VarieteService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));

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

    this.variete$ = this.varieteService.getVariete(id);
    this.especes$ = this.especeService.getEspeces();

    this.variete$.subscribe(variete => {
      this.remplirFormulaire(variete);
    });
  }

  remplirFormulaire(variete: Variete): void {
    this.varieteForm.patchValue({
      especeId: variete.especeId,
      nom: variete.nom,
      descriptif: variete.descriptif ?? '',
      bio: variete.bio ?? 0,
      cycleJours: variete.cycleJours,
      couleurLegume: variete.couleurLegume ?? '',
      tailleFixeLegume: variete.tailleFixeLegume,
      tailleMinLegume: variete.tailleMinLegume,
      tailleMaxLegume: variete.tailleMaxLegume,
      espacementEntreLesPlants: variete.espacementEntreLesPlants,
      espacementEntreLesLignes: variete.espacementEntreLesLignes,
      typeEnsoleillement: variete.typeEnsoleillement ?? '',
      typeFeuillage: variete.typeFeuillage ?? '',
      hauteurAdulteMin: variete.hauteurAdulteMin,
      hauteurAdulteMax: variete.hauteurAdulteMax,
      dureeDeGermination: variete.dureeDeGermination ?? '',
      temperatureMinDeGermination: variete.temperatureMinDeGermination,
      cycleDeVie: variete.cycleDeVie ?? '',
      rusticitePlante: variete.rusticitePlante ?? '',
      dateSemisMin: variete.dateSemisMin ?? '',
      dateSemisMax: variete.dateSemisMax ?? '',
      dureeAvantRecolte: variete.dureeAvantRecolte ?? '',
      typeDeSol: variete.typeDeSol ?? '',
      conseilPlantation: variete.conseilPlantation ?? ''
    });
  }

  enregistrer(variete: Variete): void {
    if (this.varieteForm.invalid) {
      this.varieteForm.markAllAsTouched();
      this.message = 'Veuillez remplir les champs obligatoires.';
      return;
    }

    const varieteUpdate: VarieteUpdateRequest = this.convertirFormulaire(variete.idVariete);

    this.varieteService.updateVariete(variete.idVariete, varieteUpdate).subscribe({
      next: () => {
        this.router.navigate(['/varietes', variete.idVariete]);
      },
      error: () => {
        this.message = this.varieteService.getMessageErreurModification();
      }
    });
  }

  convertirFormulaire(idVariete: number): VarieteUpdateRequest {
    const valeurFormulaire = this.varieteForm.value;

    return {
      idVariete,
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