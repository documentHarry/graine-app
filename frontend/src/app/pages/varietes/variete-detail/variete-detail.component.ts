import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { Variete } from '../../../services/api/models/variete.model';
import { VarieteService } from '../../../services/api/variete.service';
import { AuthService } from '../../../services/api/auth.service';

@Component({
  selector: 'app-variete-detail',
  imports: [CommonModule, RouterLink],
  templateUrl: './variete-detail.component.html',
  styleUrls: ['./variete-detail.component.css']
})

export class VarieteDetailComponent {
  variete$: Observable<Variete>;
  message: string | null = null;

  constructor(
    private varieteService: VarieteService,
    private route: ActivatedRoute,
    public authService: AuthService
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.variete$ = this.varieteService.getVariete(id);
  }

  getLabelBio(variete: Variete): string {
    return this.varieteService.getLabelBio(variete);
  }

  getNombreProduits(variete: Variete): number {
    return this.varieteService.getNombreProduits(variete);
  }

  getConseilsPlantation(variete: Variete): string[] {
    return this.varieteService.getConseilsPlantation(variete);
  }
}