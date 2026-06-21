import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { Role } from '../../../services/api/models/role.model';
import { RoleService } from '../../../services/api/role.service';
import { RoleUpdateRequest } from '../../../services/api/models/role-update-request.model';

@Component({
  selector: 'app-role-modifier',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './role-modifier.component.html',
  styleUrls: ['./role-modifier.component.css']
})

export class RoleModifierComponent {
  role$: Observable<Role>;
  roleForm: FormGroup;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private roleService: RoleService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.roleForm = this.fb.group({
      nomRole: ['', Validators.required]
    });

    this.role$ = this.roleService.getRole(id);

    this.role$.subscribe(role => {
      this.roleForm.patchValue({
        nomRole: role.nomRole
      });
    });
  }

  enregistrer(role: Role): void {
    if (this.roleForm.invalid) {
      this.roleForm.markAllAsTouched();
      this.message = 'Veuillez saisir un nom de rôle.';
      return;
    }

    const roleUpdate: RoleUpdateRequest = {
      idRole: role.idRole,
      nomRole: this.roleForm.value.nomRole.trim()
    };

    this.roleService.updateRole(role.idRole, roleUpdate).subscribe({
      next: () => {
        this.router.navigate(['/roles']);
      },
      error: () => {
        this.message = this.roleService.getMessageErreurModificationRole();
      }
    });
  }

  get nomRole() {
    return this.roleForm.get('nomRole');
  }
}