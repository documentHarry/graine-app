import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';

import { Role } from '../../../services/api/models/role.model';
import { RoleService } from '../../../services/api/role.service';

@Component({
  selector: 'app-role-supprimer',
  imports: [CommonModule, RouterLink],
  templateUrl: './role-supprimer.component.html',
  styleUrls: ['./role-supprimer.component.css']
})
export class RoleSupprimerComponent {
  role$: Observable<Role>;
  message: string | null = null;

  constructor(
    private roleService: RoleService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.role$ = this.roleService.getRole(id);
  }

  supprimerRole(role: Role): void {
    const confirmation = confirm('Voulez-vous vraiment supprimer ce rôle ?');

    if (!confirmation) {
      return;
    }

    this.roleService.deleteRole(role.idRole).subscribe({
      next: () => {
        this.router.navigate(['/roles']);
      },
      error: () => {
        this.message = this.roleService.getMessageErreurSuppressionRole();
      }
    });
  }

  annuler(): void {
    this.router.navigate(['/roles']);
  }
}