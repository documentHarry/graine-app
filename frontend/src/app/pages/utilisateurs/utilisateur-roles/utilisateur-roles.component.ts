import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { Role } from '../../../services/api/models/role.model';
import { RoleService } from '../../../services/api/role.service';
import { Utilisateur } from '../../../services/api/models/utilisateur.model';
import { UtilisateurService } from '../../../services/api/utilisateur.service';
import { UtilisateurRoleService } from '../../../services/api/utilisateur-role.service';

@Component({
  selector: 'app-utilisateur-roles',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './utilisateur-roles.component.html',
  styleUrls: ['./utilisateur-roles.component.css']
})
export class UtilisateurRolesComponent {
  rolesForm: FormGroup;

  utilisateur$: Observable<Utilisateur>;
  roles$: Observable<Role[]>;

  utilisateur: Utilisateur | null = null;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private utilisateurService: UtilisateurService,
    private roleService: RoleService,
    private utilisateurRoleService: UtilisateurRoleService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const idUtilisateur = Number(this.route.snapshot.paramMap.get('id'));

    this.rolesForm = this.fb.group({
      rolesIds: [[]]
    });

    this.utilisateur$ = this.utilisateurService.getUtilisateur(idUtilisateur);
    this.roles$ = this.roleService.getRoles();

    this.utilisateur$.subscribe(utilisateur => {
      this.utilisateur = utilisateur;
    });

    this.utilisateurRoleService.getUtilisateurRoles(idUtilisateur).subscribe({
      next: (utilisateurRoles) => {
        this.rolesForm.patchValue({
          rolesIds: this.utilisateurRoleService.getRoleIdsDepuisUtilisateurRoles(utilisateurRoles)
        });
      },
      error: () => {
        this.message = 'Erreur pendant le chargement des rôles de l’utilisateur.';
      }
    });
  }

  getNomComplet(utilisateur: Utilisateur): string {
    return this.utilisateurRoleService.getNomComplet(utilisateur);
  }

  isRoleCoche(role: Role): boolean {
    const rolesIds = this.rolesForm.value.rolesIds ?? [];

    return this.utilisateurRoleService.isRoleCoche(role, rolesIds);
  }

  modifierRole(role: Role, event: Event): void {
    const input = event.target as HTMLInputElement;
    const rolesIds = this.rolesForm.value.rolesIds ?? [];

    if (input.checked) {
      this.rolesForm.patchValue({
        rolesIds: this.utilisateurRoleService.ajouterRoleId(rolesIds, role)
      });
      return;
    }

    this.rolesForm.patchValue({
      rolesIds: this.utilisateurRoleService.retirerRoleId(rolesIds, role)
    });
  }

  enregistrer(utilisateur: Utilisateur): void {
    const rolesIds = this.rolesForm.value.rolesIds ?? [];

    if (rolesIds.length === 0) {
      this.message = this.utilisateurRoleService.getMessageErreurAucunRole();
      return;
    }

    this.utilisateurRoleService.updateUtilisateurRoles(
      utilisateur.idUtilisateur,
      rolesIds
    ).subscribe({
      next: () => {
        this.router.navigate(['/utilisateurs', utilisateur.idUtilisateur]);
      },
      error: () => {
        this.message = this.utilisateurRoleService.getMessageErreurModification();
      }
    });
  }
}