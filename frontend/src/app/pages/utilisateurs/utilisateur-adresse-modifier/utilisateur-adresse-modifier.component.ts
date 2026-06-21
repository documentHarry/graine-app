import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { AdresseLivraison } from '../../../services/api/models/adresse-livraison.model';
import { AdresseLivraisonService } from '../../../services/api/adresse-livraison.service';
import { AdresseLivraisonUpdateRequest } from '../../../services/api/models/adresse-livraison-update-request.model';

import { Localite } from '../../../services/api/models/localite.model';
import { LocaliteService } from '../../../services/api/localite.service';

import { Utilisateur } from '../../../services/api/models/utilisateur.model';
import { UtilisateurService } from '../../../services/api/utilisateur.service';

@Component({
  selector: 'app-utilisateur-adresse-modifier',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './utilisateur-adresse-modifier.component.html',
  styleUrls: ['./utilisateur-adresse-modifier.component.css']
})
export class UtilisateurAdresseModifierComponent {
  adresseForm: FormGroup;

  utilisateur$: Observable<Utilisateur>;
  localites$: Observable<Localite[]>;

  adresse: AdresseLivraison | null = null;
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
    const idAdresse = Number(this.route.snapshot.paramMap.get('adresseId'));

    this.adresseForm = this.fb.group({
      rue: ['', Validators.required],
      numero: ['', Validators.required],
      parDefaut: [0, Validators.required],
      localiteId: [0, [Validators.required, Validators.min(1)]]
    });

    this.utilisateur$ = this.utilisateurService.getUtilisateur(idUtilisateur);
    this.localites$ = this.localiteService.getLocalites();

    this.utilisateur$.subscribe(utilisateur => {
      const adresse = utilisateur.adressesLivraison.find(
        adresse => adresse.idAdresse === idAdresse
      ) ?? null;

      if (adresse === null) {
        this.message = 'Adresse introuvable.';
        return;
      }

      this.adresse = adresse;

      this.adresseForm.patchValue({
        rue: adresse.rue,
        numero: adresse.numero,
        parDefaut: adresse.parDefaut ?? 0,
        localiteId: adresse.localiteId
      });
    });
  }

  enregistrer(utilisateur: Utilisateur): void {
    if (this.adresseForm.invalid || this.adresse === null) {
      this.adresseForm.markAllAsTouched();
      this.message = this.adresseLivraisonService.getMessageErreurChampsObligatoires();
      return;
    }

    const adresse: AdresseLivraisonUpdateRequest = {
      idAdresse: this.adresse.idAdresse,
      rue: this.adresseForm.value.rue.trim(),
      numero: this.adresseForm.value.numero.trim(),
      parDefaut: Number(this.adresseForm.value.parDefaut),
      localiteId: Number(this.adresseForm.value.localiteId)
    };

    this.adresseLivraisonService.updateAdresseLivraison(
      this.adresse.idAdresse,
      adresse
    ).subscribe({
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