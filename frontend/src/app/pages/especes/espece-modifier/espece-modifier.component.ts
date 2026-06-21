import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Espece } from '../../../services/api/models/espece.model';
import { EspeceService } from '../../../services/api/espece.service';
import { EspeceUpdateRequest } from '../../../services/api/models/espece-update-request.model';

@Component({
  selector: 'app-espece-modifier',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './espece-modifier.component.html',
  styleUrls: ['./espece-modifier.component.css']
})

export class EspeceModifierComponent {
  espece$: Observable<Espece>;
  especeForm: FormGroup;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private especeService: EspeceService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.especeForm = this.fb.group({
      nomCommun: ['', Validators.required],
      nomScientifique: ['', Validators.required]
    });

    this.espece$ = this.especeService.getEspece(id);

    this.espece$.subscribe(espece => {
      this.especeForm.patchValue({
        nomCommun: espece.nomCommun,
        nomScientifique: espece.nomScientifique
      });
    });
  }

  enregistrer(espece: Espece): void {
    if (this.especeForm.invalid) {
      this.especeForm.markAllAsTouched();
      this.message = 'Veuillez remplir les champs obligatoires.';
      return;
    }

    const especeUpdate: EspeceUpdateRequest = {
      idEspece: espece.idEspece,
      nomCommun: this.especeForm.value.nomCommun.trim(),
      nomScientifique: this.especeForm.value.nomScientifique.trim()
    };

    this.especeService.updateEspece(espece.idEspece, especeUpdate).subscribe({
      next: () => {
        this.router.navigate(['/especes']);
      },
      error: () => {
        this.message = 'Erreur pendant la modification de l’espèce.';
      }
    });
  }

  get nomCommun() {
    return this.especeForm.get('nomCommun');
  }

  get nomScientifique() {
    return this.especeForm.get('nomScientifique');
  }
}