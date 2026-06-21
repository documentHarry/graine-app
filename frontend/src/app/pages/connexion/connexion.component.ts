import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthService } from '../../services/api/auth.service';
import { AuthenticationRequest } from '../../services/api/models/authentication-request.model';

@Component({
  selector: 'app-connexion',
  imports: [ReactiveFormsModule],
  templateUrl: './connexion.component.html',
  styleUrls: ['./connexion.component.css']
})

export class ConnexionComponent implements OnInit {
  connexionForm!: FormGroup;

  messageErreur: string | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.connexionForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      motDePasse: ['', Validators.required]
    });
  }

  seConnecter(): void {
    this.messageErreur = null;

    if (this.connexionForm.invalid) {
      this.connexionForm.markAllAsTouched();
      this.messageErreur = this.authService.getMessageErreurFormulaireConnexion();
      return;
    }

    const identifiants: AuthenticationRequest =
      this.authService.construireIdentifiantsConnexion(this.connexionForm.value);

    this.authService.login(identifiants).subscribe({
      next: () => {
        const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') ?? '/';
        this.router.navigateByUrl(returnUrl);
      },
      error: () => {
        this.messageErreur = this.authService.getMessageErreurIdentifiants();
      }
    });
  }

  get email() {
    return this.connexionForm.get('email');
  }

  get motDePasse() {
    return this.connexionForm.get('motDePasse');
  }
}