import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { Aromate } from '../../../services/api/models/aromate.model';
import { AromateService } from '../../../services/api/aromate.service';
import { AuthService } from '../../../services/api/auth.service';

@Component({
  selector: 'app-aromate-detail',
  imports: [CommonModule, RouterLink],
  templateUrl: './aromate-detail.component.html',
  styleUrls: ['./aromate-detail.component.css']
})

export class AromateDetailComponent {
  aromate$: Observable<Aromate>;

  constructor(
    private aromateService: AromateService,
    private route: ActivatedRoute,
    public authService: AuthService
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.aromate$ = this.aromateService.getAromate(id);
  }

  getProprietesMedicinales(aromate: Aromate): string[] {
    return this.aromateService.getProprietesMedicinales(aromate);
  }
}