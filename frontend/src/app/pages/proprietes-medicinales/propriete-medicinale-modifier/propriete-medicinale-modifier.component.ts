import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { ProprieteMedicinale } from '../../../services/api/models/propriete-medicinale.model';
import { ProprieteMedicinaleUpdateRequest } from '../../../services/api/models/propriete-medicinale-update-request.model';
import { ProprieteMedicinaleService } from '../../../services/api/propriete-medicinale.service';

@Component({
  selector: 'app-propriete-medicinale-modifier',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './propriete-medicinale-modifier.component.html',
  styleUrls: ['./propriete-medicinale-modifier.component.css']
})

export class ProprieteMedicinaleModifierComponent {
  propriete$: Observable<ProprieteMedicinale>;
  proprieteForm: FormGroup;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private proprieteMedicinaleService: ProprieteMedicinaleService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.proprieteForm = this.fb.group({
      nomPropriete: ['', Validators.required]
    });

    this.propriete$ = this.proprieteMedicinaleService.getProprieteMedicinale(id);

    this.propriete$.subscribe(propriete => {
      this.proprieteForm.patchValue({
        nomPropriete: propriete.nomPropriete
      });
    });
  }

  enregistrer(propriete: ProprieteMedicinale): void {
    if (this.proprieteForm.invalid) {
      this.proprieteForm.markAllAsTouched();
      this.message = 'Veuillez remplir le champ obligatoire.';
      return;
    }

    const proprieteUpdate: ProprieteMedicinaleUpdateRequest = {
      idPropriete: propriete.idPropriete,
      nomPropriete: this.proprieteForm.value.nomPropriete.trim()
    };

    this.proprieteMedicinaleService.updateProprieteMedicinale(
      propriete.idPropriete,
      proprieteUpdate
    ).subscribe({
      next: () => {
        this.router.navigate(['/proprietes-medicinales']);
      },
      error: () => {
        this.message = this.proprieteMedicinaleService.getMessageErreurEnregistrement();
      }
    });
  }

  get nomPropriete() {
    return this.proprieteForm.get('nomPropriete');
  }
}