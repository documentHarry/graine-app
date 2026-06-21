import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { AromateService } from '../../../services/api/aromate.service';
import { VarieteService } from '../../../services/api/variete.service';
import { ProprieteMedicinaleService } from '../../../services/api/propriete-medicinale.service';

import { AromateCreateRequest } from '../../../services/api/models/aromate-create-request.model';
import { Variete } from '../../../services/api/models/variete.model';
import { ProprieteMedicinale } from '../../../services/api/models/propriete-medicinale.model';

@Component({
  selector: 'app-aromate-ajouter',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './aromate-ajouter.component.html',
  styleUrls: ['./aromate-ajouter.component.css']
})

export class AromateAjouterComponent {
  aromateForm: FormGroup;

  varietes$: Observable<Variete[]>;
  proprietesMedicinales$: Observable<ProprieteMedicinale[]>;

  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private aromateService: AromateService,
    private varieteService: VarieteService,
    private proprieteMedicinaleService: ProprieteMedicinaleService,
    private router: Router
  ) {
    this.aromateForm = this.fb.group({
      varieteId: [0, [Validators.required, Validators.min(1)]],
      partieUtilisee: [''],
      propriete: [''],
      usageCulinaire: [''],
      proprietesIds: [[]]
    });

    this.varietes$ = this.varieteService.getVarietes();
    this.proprietesMedicinales$ = this.proprieteMedicinaleService.getProprietesMedicinales();
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

  enregistrer(): void {
    if (this.aromateForm.invalid) {
      this.aromateForm.markAllAsTouched();
      this.message = 'Veuillez remplir les champs obligatoires.';
      return;
    }

    const aromate: AromateCreateRequest = {
      varieteId: Number(this.aromateForm.value.varieteId),
      partieUtilisee: this.aromateForm.value.partieUtilisee?.trim() || null,
      propriete: this.aromateForm.value.propriete?.trim() || null,
      usageCulinaire: this.aromateForm.value.usageCulinaire?.trim() || null,
      proprietesIds: this.aromateForm.value.proprietesIds ?? []
    };

    this.aromateService.addAromate(aromate).subscribe({
      next: () => {
        this.router.navigate(['/aromates']);
      },
      error: () => {
        this.message = this.aromateService.getMessageErreurCreation();
      }
    });
  }

  get varieteId() {
    return this.aromateForm.get('varieteId');
  }
}