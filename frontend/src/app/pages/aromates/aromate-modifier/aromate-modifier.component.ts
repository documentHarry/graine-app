import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Aromate } from '../../../services/api/models/aromate.model';
import { AromateService } from '../../../services/api/aromate.service';
import { AromateUpdateRequest } from '../../../services/api/models/aromate-update-request.model';

import { Variete } from '../../../services/api/models/variete.model';
import { VarieteService } from '../../../services/api/variete.service';

import { ProprieteMedicinale } from '../../../services/api/models/propriete-medicinale.model';
import { ProprieteMedicinaleService } from '../../../services/api/propriete-medicinale.service';

@Component({
  selector: 'app-aromate-modifier',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './aromate-modifier.component.html',
  styleUrls: ['./aromate-modifier.component.css']
})

export class AromateModifierComponent {
  aromate$: Observable<Aromate>;
  varietes$: Observable<Variete[]>;
  proprietesMedicinales$: Observable<ProprieteMedicinale[]>;

  aromateForm: FormGroup;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private aromateService: AromateService,
    private varieteService: VarieteService,
    private proprieteMedicinaleService: ProprieteMedicinaleService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.aromateForm = this.fb.group({
      varieteId: [0, [Validators.required, Validators.min(1)]],
      partieUtilisee: [''],
      propriete: [''],
      usageCulinaire: [''],
      proprietesIds: [[]]
    });

    this.aromate$ = this.aromateService.getAromate(id);
    this.varietes$ = this.varieteService.getVarietes();
    this.proprietesMedicinales$ =
      this.proprieteMedicinaleService.getProprietesMedicinales();

    this.aromate$.subscribe(aromate => {
      this.aromateForm.patchValue({
        varieteId: aromate.varieteId,
        partieUtilisee: aromate.partieUtilisee ?? '',
        propriete: aromate.propriete ?? '',
        usageCulinaire: aromate.usageCulinaire ?? '',
        proprietesIds: this.aromateService.getProprietesSelectionneesDepuisAromate(aromate)
      });
    });
  }

  estProprieteSelectionnee(idPropriete: number): boolean {
    const ids = this.aromateForm.value.proprietesIds ?? [];
    return ids.includes(idPropriete);
  }

  changerPropriete(event: Event, idPropriete: number): void {
    const checkbox = event.target as HTMLInputElement;
    const ids = [...(this.aromateForm.value.proprietesIds ?? [])];

    if (checkbox.checked) {
      ids.push(idPropriete);
    }
    else {
      const index = ids.indexOf(idPropriete);

      if (index >= 0) {
        ids.splice(index, 1);
      }
    }

    this.aromateForm.patchValue({
      proprietesIds: ids
    });
  }

  enregistrer(aromate: Aromate): void {
    if (this.aromateForm.invalid) {
      this.aromateForm.markAllAsTouched();
      this.message = 'Veuillez remplir les champs obligatoires.';
      return;
    }

    const aromateUpdate: AromateUpdateRequest = {
      idAromate: aromate.idAromate,
      varieteId: Number(this.aromateForm.value.varieteId),
      partieUtilisee: this.aromateForm.value.partieUtilisee?.trim() || null,
      propriete: this.aromateForm.value.propriete?.trim() || null,
      usageCulinaire: this.aromateForm.value.usageCulinaire?.trim() || null,
      proprietesIds: this.aromateForm.value.proprietesIds ?? []
    };

    this.aromateService.updateAromate(aromate.idAromate, aromateUpdate).subscribe({
      next: () => {
        this.router.navigate(['/aromates', aromate.idAromate]);
      },
      error: () => {
        this.message = this.aromateService.getMessageErreurModification();
      }
    });
  }

  get varieteId() {
    return this.aromateForm.get('varieteId');
  }
}