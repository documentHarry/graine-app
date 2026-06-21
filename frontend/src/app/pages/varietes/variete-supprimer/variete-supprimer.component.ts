import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Variete } from '../../../services/api/models/variete.model';
import { VarieteService } from '../../../services/api/variete.service';

@Component({
  selector: 'app-variete-supprimer',
  imports: [CommonModule, RouterLink],
  templateUrl: './variete-supprimer.component.html',
  styleUrls: ['./variete-supprimer.component.css']
})
export class VarieteSupprimerComponent {
  variete$: Observable<Variete>;
  message: string | null = null;

  constructor(
    private varieteService: VarieteService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.variete$ = this.varieteService.getVariete(id);
  }

  getNombreProduits(variete: Variete): number {
    return this.varieteService.getNombreProduits(variete);
  }

  supprimerVariete(variete: Variete): void {
    const confirmation = confirm('Voulez-vous vraiment supprimer cette variété ?');

    if (!confirmation) {
      return;
    }

    this.varieteService.deleteVariete(variete.idVariete).subscribe({
      next: () => {
        this.router.navigate(['/varietes']);
      },
      error: () => {
        this.message = this.varieteService.getMessageErreurSuppression();
      }
    });
  }

  annuler(variete: Variete): void {
    this.router.navigate(['/varietes', variete.idVariete]);
  }
}