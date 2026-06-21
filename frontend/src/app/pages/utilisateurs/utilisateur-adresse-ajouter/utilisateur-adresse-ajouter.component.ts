import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { AdresseLivraisonService } from '../../../services/api/adresse-livraison.service';
import { AdresseLivraisonCreateRequest } from '../../../services/api/models/adresse-livraison-create-request.model';

import { Localite } from '../../../services/api/models/localite.model';
import { LocaliteService } from '../../../services/api/localite.service';

import { Utilisateur } from '../../../services/api/models/utilisateur.model';
import { UtilisateurService } from '../../../services/api/utilisateur.service';

@Component({
  selector: 'app-utilisateur-adresse-ajouter',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './utilisateur-adresse-ajouter.component.html',
  styleUrls: ['./utilisateur-adresse-ajouter.component.css']
})

export class UtilisateurAdresseAjouterComponent {
  adresseForm: FormGroup;

  utilisateur$: Observable<Utilisateur>;
  localites$: Observable<Localite[]>;

  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private adresseLivraisonService: AdresseLivraisonService,
    private localiteService: LocaliteService,
    private utilisateurService: UtilisateurService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const idUtilisateur = Number(this.route.snapshot.paramMap.get('id'));

    this.adresseForm = this.fb.group({
      rue: ['', Validators.required],
      numero: ['', Validators.required],
      parDefaut: [0, Validators.required],
      localiteId: [0, [Validators.required, Validators.min(1)]]
    });

    this.utilisateur$ = this.utilisateurService.getUtilisateur(idUtilisateur);
    this.localites$ = this.localiteService.getLocalites();
  }

  enregistrer(utilisateur: Utilisateur): void {
    if (this.adresseForm.invalid) {
      this.adresseForm.markAllAsTouched();
      this.message = this.adresseLivraisonService.getMessageErreurChampsObligatoires();
      return;
    }

    const adresse: AdresseLivraisonCreateRequest = {
      rue: this.adresseForm.value.rue.trim(),
      numero: this.adresseForm.value.numero.trim(),
      parDefaut: Number(this.adresseForm.value.parDefaut),
      utilisateurId: utilisateur.idUtilisateur,
      localiteId: Number(this.adresseForm.value.localiteId)
    };

    this.adresseLivraisonService.addAdresseLivraison(adresse).subscribe({
      next: () => {
        this.router.navigate(['/utilisateurs', utilisateur.idUtilisateur]);
      },
      error: () => {
        this.message = this.adresseLivraisonService.getMessageErreurEnregistrement();
      }
    });
  }

  get rue() {
    return this.adresseForm.get('rue');
  }

  get numero() {
    return this.adresseForm.get('numero');
  }

  get localiteId() {
    return this.adresseForm.get('localiteId');
  }
}