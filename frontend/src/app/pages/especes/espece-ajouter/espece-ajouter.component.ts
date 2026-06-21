import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { EspeceService } from '../../../services/api/espece.service';
import { EspeceCreateRequest } from '../../../services/api/models/espece-create-request.model';

@Component({
  selector: 'app-espece-ajouter',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './espece-ajouter.component.html',
  styleUrls: ['./espece-ajouter.component.css']
})

export class EspeceAjouterComponent implements OnInit {
  especeForm!: FormGroup;

  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private especeService: EspeceService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.especeForm = this.fb.group({
      nomCommun: ['', Validators.required],
      nomScientifique: ['', Validators.required]
    });
  }

  enregistrer(): void {
    if (this.especeForm.invalid) {
      this.especeForm.markAllAsTouched();
      this.message = 'Veuillez remplir les champs obligatoires.';
      return;
    }

    const espece: EspeceCreateRequest = this.especeForm.value;

    this.especeService.addEspece(espece).subscribe({
      next: () => {
        this.router.navigate(['/especes']);
      },
      error: () => {
        this.message = 'Erreur pendant la création de l’espèce.';
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