import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { RoleService } from '../../../services/api/role.service';
import { RoleCreateRequest } from '../../../services/api/models/role-create-request.model';

@Component({
  selector: 'app-role-ajouter',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './role-ajouter.component.html',
  styleUrls: ['./role-ajouter.component.css']
})

export class RoleAjouterComponent implements OnInit {
  roleForm!: FormGroup;

  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private roleService: RoleService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.roleForm = this.fb.group({
      nomRole: ['', Validators.required]
    });
  }

  enregistrer(): void {
    if (this.roleForm.invalid) {
      this.roleForm.markAllAsTouched();
      this.message = 'Veuillez saisir un nom de rôle.';
      return;
    }

    const role: RoleCreateRequest = {
      nomRole: this.roleForm.value.nomRole.trim()
    };

    this.roleService.addRole(role).subscribe({
      next: () => {
        this.router.navigate(['/roles']);
      },
      error: () => {
        this.message = this.roleService.getMessageErreurCreationRole();
      }
    });
  }

  get nomRole() {
    return this.roleForm.get('nomRole');
  }
}