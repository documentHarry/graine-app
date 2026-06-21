import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { ProprieteMedicinaleService } from '../../../services/api/propriete-medicinale.service';
import { ProprieteMedicinaleCreateRequest } from '../../../services/api/models/propriete-medicinale-create-request.model';

@Component({
  selector: 'app-propriete-medicinale-ajouter',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './propriete-medicinale-ajouter.component.html',
  styleUrls: ['./propriete-medicinale-ajouter.component.css']
})

export class ProprieteMedicinaleAjouterComponent implements OnInit {
  proprieteForm!: FormGroup;

  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private proprieteMedicinaleService: ProprieteMedicinaleService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.proprieteForm = this.fb.group({
      nomPropriete: ['', Validators.required]
    });
  }

  enregistrer(): void {
    if (this.proprieteForm.invalid) {
      this.proprieteForm.markAllAsTouched();
      this.message = 'Veuillez remplir le champ obligatoire.';
      return;
    }

    const propriete: ProprieteMedicinaleCreateRequest = {
      nomPropriete: this.proprieteForm.value.nomPropriete.trim()
    };

    this.proprieteMedicinaleService
      .addProprieteMedicinale(propriete)
      .subscribe({
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