import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Utilisateur } from '../../../services/api/models/utilisateur.model';
import { UtilisateurService } from '../../../services/api/utilisateur.service';

@Component({
  selector: 'app-utilisateur-supprimer',
  imports: [CommonModule, RouterLink],
  templateUrl: './utilisateur-supprimer.component.html',
  styleUrls: ['./utilisateur-supprimer.component.css']
})

export class UtilisateurSupprimerComponent {
  utilisateur$: Observable<Utilisateur>;
  message: string | null = null;

  constructor(
    private utilisateurService: UtilisateurService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.utilisateur$ = this.utilisateurService.getUtilisateur(id);
  }

  getNomComplet(utilisateur: Utilisateur): string {
    return this.utilisateurService.getNomComplet(utilisateur);
  }

  getNombreAdresses(utilisateur: Utilisateur): number {
    return this.utilisateurService.getNombreAdresses(utilisateur);
  }

  supprimerUtilisateur(utilisateur: Utilisateur): void {
    const confirmation = confirm('Voulez-vous vraiment désactiver cet utilisateur ?');

    if (!confirmation) {
      return;
    }

    this.utilisateurService.deleteUtilisateur(utilisateur.idUtilisateur).subscribe({
      next: () => {
        this.router.navigate(['/utilisateurs']);
      },
      error: () => {
        this.message = this.utilisateurService.getMessageErreurSuppression();
      }
    });
  }

  annuler(utilisateur: Utilisateur): void {
    this.router.navigate(['/utilisateurs', utilisateur.idUtilisateur]);
  }
}