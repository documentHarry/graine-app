import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Aromate } from '../../../services/api/models/aromate.model';
import { AromateService } from '../../../services/api/aromate.service';

@Component({
  selector: 'app-aromate-supprimer',
  imports: [CommonModule, RouterLink],
  templateUrl: './aromate-supprimer.component.html',
  styleUrls: ['./aromate-supprimer.component.css']
})

export class AromateSupprimerComponent {
  aromate$: Observable<Aromate>;
  message: string | null = null;

  constructor(
    private aromateService: AromateService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.aromate$ = this.aromateService.getAromate(id);
  }

  supprimerAromate(aromate: Aromate): void {
    const confirmation = confirm('Voulez-vous vraiment supprimer cet aromate ?');

    if (!confirmation) {
      return;
    }

    this.aromateService.deleteAromate(aromate.idAromate).subscribe({
      next: () => {
        this.router.navigate(['/aromates']);
      },
      error: () => {
        this.message = this.aromateService.getMessageErreurSuppression();
      }
    });
  }

  annuler(aromate: Aromate): void {
    this.router.navigate(['/aromates', aromate.idAromate]);
  }
}