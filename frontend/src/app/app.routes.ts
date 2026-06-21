import { Routes } from '@angular/router';
import { authGuard } from './pages/guards/auth.guard';
import { roleGuard } from './pages/guards/role.guard';
import { ConnexionComponent } from './pages/connexion/connexion.component';
import { CategoriesComponent } from './pages/categories/categories.component';
import { CategorieAjouterComponent } from './pages/categories/categorie-ajouter/categorie-ajouter.component';
import { CategorieModifierComponent } from './pages/categories/categorie-modifier/categorie-modifier.component';
import { CategorieSupprimerComponent } from './pages/categories/categorie-supprimer/categorie-supprimer.component';
import { EspecesComponent } from './pages/especes/especes.component';
import { EspeceAjouterComponent } from './pages/especes/espece-ajouter/espece-ajouter.component';
import { EspeceModifierComponent } from './pages/especes/espece-modifier/espece-modifier.component';
import { EspeceSupprimerComponent } from './pages/especes/espece-supprimer/espece-supprimer.component';
import { VarietesComponent } from './pages/varietes/varietes.component';
import { VarieteDetailComponent } from './pages/varietes/variete-detail/variete-detail.component';
import { VarieteAjouterComponent } from './pages/varietes/variete-ajouter/variete-ajouter.component';
import { VarieteModifierComponent } from './pages/varietes/variete-modifier/variete-modifier.component';
import { VarieteSupprimerComponent } from './pages/varietes/variete-supprimer/variete-supprimer.component';
import { ProprietesMedicinalesComponent } from './pages/proprietes-medicinales/proprietes-medicinales.component';
import { ProprieteMedicinaleAjouterComponent } from './pages/proprietes-medicinales/propriete-medicinale-ajouter/propriete-medicinale-ajouter.component';
import { ProprieteMedicinaleModifierComponent } from './pages/proprietes-medicinales/propriete-medicinale-modifier/propriete-medicinale-modifier.component';
import { ProprieteMedicinaleSupprimerComponent } from './pages/proprietes-medicinales/propriete-medicinale-supprimer/propriete-medicinale-supprimer.component';
import { AromatesComponent } from './pages/aromates/aromates.component';
import { AromateDetailComponent } from './pages/aromates/aromate-detail/aromate-detail.component';
import { AromateAjouterComponent } from './pages/aromates/aromate-ajouter/aromate-ajouter.component';
import { AromateModifierComponent } from './pages/aromates/aromate-modifier/aromate-modifier.component';
import { AromateSupprimerComponent } from './pages/aromates/aromate-supprimer/aromate-supprimer.component';
import { ProduitsComponent } from './pages/produits/produits.component';
import { ProduitFiltresComponent } from './pages/produits/produit-filtres/produit-filtres.component';
import { ProduitDetailComponent } from './pages/produits/produit-detail/produit-detail.component';
import { ProduitAjouterComponent } from './pages/produits/produit-ajouter/produit-ajouter.component';
import { ProduitModifierComponent } from './pages/produits/produit-modifier/produit-modifier.component';
import { ProduitSupprimerComponent } from './pages/produits/produit-supprimer/produit-supprimer.component';
import { RolesComponent } from './pages/roles/roles.component';
import { RoleAjouterComponent } from './pages/roles/role-ajouter/role-ajouter.component';
import { RoleModifierComponent } from './pages/roles/role-modifier/role-modifier.component';
import { RoleSupprimerComponent } from './pages/roles/role-supprimer/role-supprimer.component';
import { UtilisateurAdresseAjouterComponent } from './pages/utilisateurs/utilisateur-adresse-ajouter/utilisateur-adresse-ajouter.component';
import { UtilisateurAdresseModifierComponent } from './pages/utilisateurs/utilisateur-adresse-modifier/utilisateur-adresse-modifier.component';
import { UtilisateurAdresseSupprimerComponent } from './pages/utilisateurs/utilisateur-adresse-supprimer/utilisateur-adresse-supprimer.component';
import { UtilisateursComponent } from './pages/utilisateurs/utilisateurs.component';
import { UtilisateurDetailComponent } from './pages/utilisateurs/utilisateur-detail/utilisateur-detail.component';
import { UtilisateurAjouterComponent } from './pages/utilisateurs/utilisateur-ajouter/utilisateur-ajouter.component';
import { UtilisateurModifierComponent } from './pages/utilisateurs/utilisateur-modifier/utilisateur-modifier.component';
import { UtilisateurSupprimerComponent } from './pages/utilisateurs/utilisateur-supprimer/utilisateur-supprimer.component';
import { UtilisateurRolesComponent } from './pages/utilisateurs/utilisateur-roles/utilisateur-roles.component';


const ROLES = {
  CLIENT: 'CLIENT',
  GESTIONNAIRE_CATALOGUE: 'GESTIONNAIRE_CATALOGUE',
  ADMIN: 'ADMIN'
} as const;

const CATALOGUE_ROLES = [ROLES.GESTIONNAIRE_CATALOGUE, ROLES.ADMIN];
const ADMIN_ROLES = [ROLES.ADMIN];

export const routes: Routes = [
  { path: 'connexion', component: ConnexionComponent },

  { path: '', redirectTo: 'categories', pathMatch: 'full' },

  { path: 'categories', component: CategoriesComponent },
  { path: 'categories/ajouter', component: CategorieAjouterComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'categories/modifier/:id', component: CategorieModifierComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'categories/supprimer/:id', component: CategorieSupprimerComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },

  { path: 'especes', component: EspecesComponent },
  { path: 'especes/ajouter', component: EspeceAjouterComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'especes/modifier/:id', component: EspeceModifierComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'especes/supprimer/:id', component: EspeceSupprimerComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },

  { path: 'varietes', component: VarietesComponent },
  { path: 'varietes/ajouter', component: VarieteAjouterComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'varietes/modifier/:id', component: VarieteModifierComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'varietes/supprimer/:id', component: VarieteSupprimerComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'varietes/:id', component: VarieteDetailComponent },

  { path: 'proprietes-medicinales', component: ProprietesMedicinalesComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'proprietes-medicinales/ajouter', component: ProprieteMedicinaleAjouterComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'proprietes-medicinales/modifier/:id', component: ProprieteMedicinaleModifierComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'proprietes-medicinales/supprimer/:id', component: ProprieteMedicinaleSupprimerComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },

  { path: 'aromates', component: AromatesComponent },
  { path: 'aromates/ajouter', component: AromateAjouterComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'aromates/modifier/:id', component: AromateModifierComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'aromates/supprimer/:id', component: AromateSupprimerComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'aromates/:id', component: AromateDetailComponent },

  { path: 'produits', component: ProduitsComponent },
  { path: 'produits/categorie/:id', component: ProduitsComponent },
  { path: 'produits/ajouter', component: ProduitAjouterComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'produits/modifier/:id', component: ProduitModifierComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'produits/supprimer/:id', component: ProduitSupprimerComponent,
    canActivate: [authGuard, roleGuard], data: { roles: CATALOGUE_ROLES } },
  { path: 'produits/:id', component: ProduitDetailComponent },

  { path: 'roles', component: RolesComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },
  { path: 'roles/ajouter', component: RoleAjouterComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },
  { path: 'roles/modifier/:id', component: RoleModifierComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },
  { path: 'roles/supprimer/:id', component: RoleSupprimerComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },

  { path: 'utilisateurs', component: UtilisateursComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },
  { path: 'utilisateurs/ajouter', component: UtilisateurAjouterComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },
  { path: 'utilisateurs/modifier/:id', component: UtilisateurModifierComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },
  { path: 'utilisateurs/supprimer/:id', component: UtilisateurSupprimerComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },
  { path: 'utilisateurs/roles/:id', component: UtilisateurRolesComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },

  { path: 'utilisateurs/:id/adresses/ajouter', component: UtilisateurAdresseAjouterComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },
  { path: 'utilisateurs/:id/adresses/modifier/:adresseId', component: UtilisateurAdresseModifierComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },
  { path: 'utilisateurs/:id/adresses/supprimer/:adresseId', component: UtilisateurAdresseSupprimerComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },

  { path: 'utilisateurs/:id', component: UtilisateurDetailComponent,
    canActivate: [authGuard, roleGuard], data: { roles: ADMIN_ROLES } },

  { path: '**', redirectTo: 'categories' }
];