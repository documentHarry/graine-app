import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { ProprieteMedicinale } from '../../../services/api/models/propriete-medicinale.model';
import { ProprieteMedicinaleService } from '../../../services/api/propriete-medicinale.service';

@Component({
  selector: 'app-propriete-medicinale-supprimer',
  imports: [CommonModule, RouterLink],
  templateUrl: './propriete-medicinale-supprimer.component.html',
  styleUrls: ['./propriete-medicinale-supprimer.component.css']
})
export class ProprieteMedicinaleSupprimerComponent {
  propriete$: Observable<ProprieteMedicinale>;
  message: string | null = null;

  constructor(
    private proprieteMedicinaleService: ProprieteMedicinaleService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.propriete$ = this.proprieteMedicinaleService.getProprieteMedicinale(id);
  }

  supprimerPropriete(propriete: ProprieteMedicinale): void {
    const confirmation = confirm(
      'Voulez-vous vraiment supprimer cette propriété médicinale ?'
    );

    if (!confirmation) {
      return;
    }

    this.proprieteMedicinaleService.deleteProprieteMedicinale(
      propriete.idPropriete
    ).subscribe({
      next: () => {
        this.router.navigate(['/proprietes-medicinales']);
      },
      error: () => {
        this.message = this.proprieteMedicinaleService.getMessageErreurSuppression();
      }
    });
  }

  annuler(): void {
    this.router.navigate(['/proprietes-medicinales']);
  }
}