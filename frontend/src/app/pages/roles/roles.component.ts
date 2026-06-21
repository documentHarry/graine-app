import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';

import { Role } from '../../services/api/models/role.model';
import { RoleService } from '../../services/api/role.service';

@Component({
  selector: 'app-roles',
  imports: [CommonModule, RouterLink],
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})

export class RolesComponent {
  roles$: Observable<Role[]>;

  recherche = '';

  constructor(private roleService: RoleService) {
    this.roles$ = this.roleService.getRoles();
  }

  rolesFiltres(roles: Role[]): Role[] {
    return this.roleService.filtrerRoles(roles, this.recherche);
  }

  changerRecherche(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.recherche = input.value;
  }
}