import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Espece } from '../../../services/api/models/espece.model';
import { EspeceService } from '../../../services/api/espece.service';

@Component({
  selector: 'app-espece-supprimer',
  imports: [CommonModule, RouterLink],
  templateUrl: './espece-supprimer.component.html',
  styleUrls: ['./espece-supprimer.component.css']
})

export class EspeceSupprimerComponent {
  espece$: Observable<Espece>;
  message: string | null = null;

  constructor(
    private especeService: EspeceService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.espece$ = this.especeService.getEspece(id);
  }

  supprimerEspece(espece: Espece): void {
    const confirmation = confirm('Voulez-vous vraiment supprimer cette espèce ?');

    if (!confirmation) {
      return;
    }

    this.especeService.deleteEspece(espece.idEspece).subscribe({
      next: () => {
        this.router.navigate(['/especes']);
      },
      error: () => {
        this.message = 'Cette espèce possède des variétés associées. Elle ne peut pas être supprimée.';
      }
    });
  }

  annuler(): void {
    this.router.navigate(['/especes']);
  }
}