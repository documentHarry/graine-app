import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { UtilisateurService } from '../../../services/api/utilisateur.service';
import { UtilisateurCreateRequest } from '../../../services/api/models/utilisateur-create-request.model';

@Component({
  selector: 'app-utilisateur-ajouter',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './utilisateur-ajouter.component.html',
  styleUrls: ['./utilisateur-ajouter.component.css']
})

export class UtilisateurAjouterComponent implements OnInit {
  utilisateurForm!: FormGroup;

  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private utilisateurService: UtilisateurService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.utilisateurForm = this.fb.group({
      nom: ['', Validators.required],
      prenom: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      motDePasse: ['', Validators.required]
    });
  }

  enregistrer(): void {
    if (this.utilisateurForm.invalid) {
      this.utilisateurForm.markAllAsTouched();
      this.message = 'Veuillez remplir les champs obligatoires.';
      return;
    }

    const utilisateur: UtilisateurCreateRequest = {
      nom: this.utilisateurForm.value.nom.trim(),
      prenom: this.utilisateurForm.value.prenom.trim(),
      email: this.utilisateurForm.value.email.trim(),
      motDePasse: this.utilisateurForm.value.motDePasse
    };

    this.utilisateurService.addUtilisateur(utilisateur).subscribe({
      next: () => {
        this.router.navigate(['/utilisateurs']);
      },
      error: () => {
        this.message = this.utilisateurService.getMessageErreurCreation();
      }
    });
  }

  get nom() {
    return this.utilisateurForm.get('nom');
  }

  get prenom() {
    return this.utilisateurForm.get('prenom');
  }

  get email() {
    return this.utilisateurForm.get('email');
  }

  get motDePasse() {
    return this.utilisateurForm.get('motDePasse');
  }
}