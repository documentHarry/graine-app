import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Utilisateur } from '../../../services/api/models/utilisateur.model';
import { UtilisateurService } from '../../../services/api/utilisateur.service';
import { UtilisateurUpdateRequest } from '../../../services/api/models/utilisateur-update-request.model';

@Component({
  selector: 'app-utilisateur-modifier',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './utilisateur-modifier.component.html',
  styleUrls: ['./utilisateur-modifier.component.css']
})

export class UtilisateurModifierComponent {
  utilisateurForm: FormGroup;

  utilisateur$: Observable<Utilisateur>;

  utilisateur: Utilisateur | null = null;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private utilisateurService: UtilisateurService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.utilisateurForm = this.fb.group({
      nom: ['', Validators.required],
      prenom: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });

    this.utilisateur$ = this.utilisateurService.getUtilisateur(id);

    this.utilisateur$.subscribe(utilisateur => {
      this.utilisateur = utilisateur;

      this.utilisateurForm.patchValue({
        nom: utilisateur.nom,
        prenom: utilisateur.prenom,
        email: utilisateur.email
      });
    });
  }

  enregistrer(): void {
    if (this.utilisateurForm.invalid || this.utilisateur === null) {
      this.utilisateurForm.markAllAsTouched();
      this.message = 'Veuillez remplir les champs obligatoires.';
      return;
    }

    const utilisateur: UtilisateurUpdateRequest = {
      idUtilisateur: this.utilisateur.idUtilisateur,
      nom: this.utilisateurForm.value.nom.trim(),
      prenom: this.utilisateurForm.value.prenom.trim(),
      email: this.utilisateurForm.value.email.trim()
    };

    this.utilisateurService.updateUtilisateur(
      this.utilisateur.idUtilisateur,
      utilisateur
    ).subscribe({
      next: () => {
        this.router.navigate(['/utilisateurs', this.utilisateur?.idUtilisateur]);
      },
      error: () => {
        this.message = this.utilisateurService.getMessageErreurModification();
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
}