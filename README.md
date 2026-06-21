# Graines

## Présentation

Graines est une application web de gestion d'un catalogue de graines, variétés végétales et produits associés.

L'application permet de centraliser et d'administrer :

- les catégories
- les espèces végétales
- les variétés
- les aromates
- les propriétés médicinales
- les produits
- les utilisateurs
- les rôles et autorisations

L'objectif principal est de fournir un outil interne permettant la gestion et la consultation d'un catalogue structuré de semences et produits associés.

---

# Important

## Application à usage interne

Cette application n'est pas un site e-commerce.

Aucune fonctionnalité liée à la vente n'est implémentée.

L'application ne permet pas :

- la prise de commande
- la gestion d'un panier
- le paiement en ligne
- la facturation
- le suivi d'expédition
- la gestion avancée des stocks
- la gestion des livraisons clients

Les informations présentes dans l'application sont utilisées exclusivement à des fins de gestion et de consultation internes.

---

# Technologies utilisées

## Frontend

- Angular 20
- TypeScript
- HTML5
- CSS3

## Backend

- ASP.NET Core 10
- C#
- Dapper
- Minimal APIs

## Base de données

- SQL Server
- MySQL

## Outils

- Visual Studio Code
- Git
- GitHub

---

# Architecture du projet

## Vue d'ensemble

L'application repose sur une architecture en couches inspirée de la Clean Architecture.

```text
┌─────────────────────┐
│  Frontend Angular   │
│      Angular 20     │
└──────────┬──────────┘
           │ HTTP / JSON
           ▼
┌─────────────────────┐
│         API         │
│   ASP.NET Core 10   │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│        Core         │
│    Logique métier   │
│      Use Cases      │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   Infrastructure    │
│ Repositories/Dapper │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│ SQL Server / MySQL  │
└─────────────────────┘
```

Cette architecture permet :

- une séparation claire des responsabilités
- une meilleure maintenabilité
- une meilleure testabilité
- une meilleure évolutivité
- une indépendance vis-à-vis du moteur de base de données

---

# Fonctionnalités principales

## Gestion du catalogue

### Catégories

Permet d'organiser les produits selon différentes catégories.

Exemples :

- Légumes
- Aromatiques
- Fleurs
- Plantes médicinales

### Espèces

Permet de gérer les espèces végétales.

Exemples :

- Solanum lycopersicum
- Daucus carota
- Ocimum basilicum

### Variétés

Permet de décrire précisément chaque variété.

Informations disponibles :

- nom
- descriptif
- cycle de vie
- période de semis
- durée avant récolte
- rusticité
- type de sol
- type d'ensoleillement
- conseils de plantation
- informations de germination
- dimensions et espacements

### Aromates

Certaines variétés peuvent être décrites comme aromatiques.

Informations disponibles :

- partie utilisée
- usages culinaires
- propriétés particulières

### Propriétés médicinales

Les aromates peuvent être associés à une ou plusieurs propriétés médicinales.

Exemples :

- digestive
- antiseptique
- calmante
- expectorante

### Produits

Les produits représentent les éléments réellement disponibles dans le catalogue.

Informations disponibles :

- intitulé
- prix unitaire
- quantité
- image
- catégorie
- variété associée

## Gestion des utilisateurs

L'application intègre une gestion des utilisateurs permettant :

- l'authentification
- la gestion des rôles
- l'administration des comptes
- la gestion des adresses de livraison

Chaque utilisateur possède :

- un compte sécurisé
- un ou plusieurs rôles
- une ou plusieurs adresses de livraison

---

# Modèle de données

```text
Utilisateur
├── AdresseLivraison
├── UtilisateurRole
└── Role

Categorie
└── Produit

Espece
└── Variete
    └── Aromate
        └── ProprieteMedicinale

Produit
├── Categorie
└── Variete

Localite
└── AdresseLivraison
```

---

# Gestion des rôles et autorisations

| Rôle | Description |
|--------|-------------|
| CLIENT | Consultation |
| GESTIONNAIRE_CATALOGUE | Gestion du catalogue |
| ADMIN | Administration complète |

## Sécurité

### Frontend

- AuthGuard
- RoleGuard
- Masquage des fonctionnalités selon les rôles

### Backend

- Contrôle des rôles sur les endpoints
- Protection via `RequireAuthorization(...)`

La sécurité est appliquée à deux niveaux : interface utilisateur et API.

---

# Installation

## Prérequis

### Backend

- .NET 10 SDK
- SQL Server LocalDB ou MySQL 8

### Frontend

- Node.js v22.12.0
- Angular CLI

```bash
npm install -g @angular/cli
```

---

# Installation de la base de données

Depuis :

```text
backend/Infrastructure/Database/Scripts
```

## SQL Server

```cmd
setup-db-sqlserver.bat
```

## MySQL

```cmd
setup-db-mysql.bat
```

Les scripts :

- créent la base
- créent les tables
- insèrent les données de démonstration
- configurent automatiquement `appsettings.json`.

---

# Lancement du backend

Depuis le dossier :

```text
backend
```

Exécuter :

```bash
dotnet restore
dotnet run --project Api/Api.csproj
```

API disponible sur :

```text
http://localhost:5274
```

---

# Lancement du frontend

Depuis le dossier :

```text
frontend
```

Installer les dépendances :

```bash
npm install
```

Démarrer l'application :

```bash
ng serve --open
```

Application disponible sur :

```text
http://localhost:4200
```

# Comptes de test

| Rôle | Email | Mot de passe |
|------|-------|--------------|
| ADMIN | jthomas@example.org | `&o)FpKqbK0` |
| GESTIONNAIRE_CATALOGUE | daan27@example.org | `iFVsP9Ma(6` |
| CLIENT | mariannesimon@example.org | `r%K4Ncv1x(` |

Le compte administrateur permet de tester l'ensemble des fonctionnalités de l'application.

---

# Génération de la version de production

## Frontend

Depuis le dossier :

```text
frontend
```

Exécuter :

```bash
ng build
```

Les fichiers générés se trouvent dans :

```text
frontend/dist
```

## Backend

Depuis le dossier :

```text
backend
```

Exécuter :

```bash
dotnet publish Api/Api.csproj -c Release -o publish
```

Les fichiers générés se trouvent dans :

```text
backend/publish
```

## Vérification

```bash
cd publish
dotnet Api.dll
```

L'API publiée est alors disponible sur :

```text
http://localhost:5274
```

---

# Architecture détaillée

## Organisation des couches

L'application est structurée selon une architecture en couches permettant de séparer clairement les responsabilités.

Chaque couche possède un rôle précis et communique uniquement avec les couches qui lui sont destinées.

```text
Frontend Angular
        │
        ▼
API ASP.NET Core
        │
        ▼
Core (logique métier)
        │
        ▼
Infrastructure (persistance)
        │
        ▼
Base de données
```

Cette organisation permet de conserver une application plus robuste, maintenable et évolutive.

---

## Frontend Angular

Le frontend est développé avec Angular 20.

Il est responsable :

- de l'affichage des données
- de la navigation
- de la gestion des formulaires
- de l'appel des endpoints API
- de la consommation des endpoints REST
- de la gestion de l'authentification côté client
- du masquage des fonctionnalités selon les rôles

## Organisation du frontend

```text
src/
│
├── pages/
│   ├── aromates/
│   ├── categories/
│   ├── connexion/
│   ├── especes/
│   ├── guards/
│   ├── produits/
│   ├── proprietes-medicinales/
│   ├── roles/
│   ├── utilisateurs/
│   └── varietes/
│
├── services/
│   └── api/
│
└── app.routes.ts
```

### Pages

Les pages contiennent :

- les composants d'affichage
- les formulaires
- les listes
- les écrans de détail

Exemple :

```text
pages/categories/
```

contient notamment :

```text
categories.component
categorie-ajouter.component
categorie-modifier.component
categorie-supprimer.component
```

### Services

Les services centralisent les appels HTTP vers l'API.

Exemple :

```typescript
categorieService.getCategories();
```

effectue un appel vers :

```http
GET /api/categories
```

### Guards

Les guards contrôlent l'accès aux routes.

#### AuthGuard

Vérifie qu'un utilisateur est authentifié avant d'accéder à une route protégée.

#### RoleGuard

Vérifie que l'utilisateur possède au moins un rôle autorisé.

---

## Backend

Le backend est développé en ASP.NET Core.

Il est découpé en trois projets :

```text
Api
Core
Infrastructure
```

---

## Projet API

Le projet API constitue la couche d'exposition.

Il reçoit les requêtes HTTP provenant du frontend.

### Responsabilités

- définition des routes
- validation des paramètres
- validation des autorisations
- gestion des réponses HTTP
- injection des dépendances
- configuration de l'application
- appel des cas d'utilisation métier
- retour des réponses JSON

### Organisation

```text
Api/
│
├── EndPoints/
├── appsettings.json
└── Program.cs
```

### EndPoints

Chaque ressource possède son propre fichier.

Exemples :

```text
CategorieRoutes.cs
ProduitRoutes.cs
VarieteRoutes.cs
```

Exemple de route :

```csharp
group.MapGet("", (...) =>
{
    ...
});
```

L'API ne contient pas la logique métier.

Elle délègue les traitements au projet Core.

---

## Projet Core

Le projet Core contient toute la logique métier de l'application.

C'est le cœur du système.

Le Core ne dépend ni de l'API ni de l'Infrastructure.

### Responsabilités

- règles métier
- cas d'utilisation
- modèles métier
- interfaces des gateways
- interfaces des cas d'utilisation

### Modèles

Les entités métier sont définies dans :

```text
Core/Models
```

Exemples :

```text
Categorie
Produit
Variete
Utilisateur
Role
```

### Use Cases

Les cas d'utilisation représentent les actions possibles dans l'application.

Exemples :

```text
ICategorieUseCases
IProduitUseCases
IUtilisateurUseCases
```

Leur rôle est de :

- appliquer les règles métier
- coordonner les opérations
- appeler les gateways

Exemple :

```csharp
categorieUseCases.AddCategorie(...)
```

### Interfaces

Le Core définit les contrats nécessaires à l'application.

Le Core sait ce qu'il veut faire.

Il ne sait pas comment les données sont stockées.

Cette séparation permet de conserver une logique métier indépendante de toute technologie.

---

## Projet Infrastructure

Le projet Infrastructure contient les implémentations techniques.

C'est la couche qui communique réellement avec la base de données.

### Responsabilités

- accès aux données
- implémentation des repositories
- implémentation des gateways
- gestion des connexions
- requêtes SQL
- persistance
- mapping entre modèles métier et modèles de persistance

### Repositories

Exemples :

```text
CategorieRepository
ProduitRepository
VarieteRepository
```

Les repositories exécutent les requêtes SQL via Dapper.

### Gateways

Les gateways assurent la liaison entre les modèles du Core et les modèles de l'Infrastructure.

Ils permettent d'isoler la logique métier des détails techniques.

### Factories de connexion

Le projet utilise des factories de connexion afin de pouvoir changer de moteur de base de données.

Exemples :

```text
SqlServerConnectionFactory
MySqlConnectionFactory
```

Le choix du moteur est effectué via la configuration de l'application.

---

# Avantages de cette architecture

## Séparation des responsabilités

Chaque couche possède un rôle précis.

### Frontend

Interface utilisateur.

### API

Communication HTTP.

### Core

Logique métier.

### Infrastructure

Persistance des données.

## Maintenabilité

Une modification dans une couche impacte peu les autres couches.

## Testabilité

Le Core peut être testé indépendamment :

```text
sans Angular
sans API
sans base de données
```

## Évolutivité

L'infrastructure peut être remplacée sans modifier la logique métier.

Exemples :

```text
SQL Server
→ MySQL
```

```text
Dapper
→ Entity Framework
```

Le Core reste inchangé.

---

# Gestion des rôles et des autorisations

## Présentation

L'application met en œuvre une gestion des autorisations basée sur les rôles.

La sécurité est assurée à deux niveaux :

1. **Frontend Angular**
   - Protection des routes via des guards.
   - Masquage des boutons et liens non autorisés.

2. **Backend .NET**
   - Protection des endpoints API via les rôles.
   - Empêche tout contournement par appel direct à l'API.

L'objectif est d'offrir une interface adaptée aux droits de l'utilisateur tout en garantissant que les opérations sensibles restent sécurisées côté serveur.

---

## Rôles disponibles

| Rôle | Description |
|--------|-------------|
| CLIENT | Utilisateur standard |
| GESTIONNAIRE_CATALOGUE | Gestion du catalogue |
| ADMIN | Administration complète |

---

## Matrice des permissions

| Fonctionnalité | CLIENT | GESTIONNAIRE_CATALOGUE | ADMIN |
|---------------|---------|---------|---------|
| Consulter catégories | ✅ | ✅ | ✅ |
| Consulter espèces | ✅ | ✅ | ✅ |
| Consulter variétés | ✅ | ✅ | ✅ |
| Consulter aromates | ✅ | ✅ | ✅ |
| Consulter produits | ✅ | ✅ | ✅ |
| Ajouter catégorie | ❌ | ✅ | ✅ |
| Modifier catégorie | ❌ | ✅ | ✅ |
| Supprimer catégorie | ❌ | ✅ | ✅ |
| Ajouter espèce | ❌ | ✅ | ✅ |
| Modifier espèce | ❌ | ✅ | ✅ |
| Supprimer espèce | ❌ | ✅ | ✅ |
| Ajouter variété | ❌ | ✅ | ✅ |
| Modifier variété | ❌ | ✅ | ✅ |
| Supprimer variété | ❌ | ✅ | ✅ |
| Ajouter aromate | ❌ | ✅ | ✅ |
| Modifier aromate | ❌ | ✅ | ✅ |
| Supprimer aromate | ❌ | ✅ | ✅ |
| Ajouter produit | ❌ | ✅ | ✅ |
| Modifier produit | ❌ | ✅ | ✅ |
| Supprimer produit | ❌ | ✅ | ✅ |
| Gérer propriétés médicinales | ❌ | ✅ | ✅ |
| Gérer utilisateurs | ❌ | ❌ | ✅ |
| Gérer rôles | ❌ | ❌ | ✅ |
| Gérer adresses utilisateurs | ❌ | ❌ | ✅ |
| Attribuer des rôles aux utilisateurs | ❌ | ❌ | ✅ |

---

## Justification des rôles

Le rôle **CLIENT** dispose d'un accès en consultation uniquement. Il peut parcourir le catalogue sans risque de modification des données.

Le rôle **GESTIONNAIRE_CATALOGUE** est chargé de l'administration du catalogue. Il peut gérer les catégories, espèces, variétés, aromates, propriétés médicinales et produits, mais ne possède aucun droit d'administration des utilisateurs.

Le rôle **ADMIN** dispose d'un accès complet à l'ensemble de l'application. Il assure la gestion des utilisateurs, des rôles et des autorisations.

---

## Protection Frontend

### Guards Angular

#### AuthGuard

Le guard d'authentification vérifie qu'un utilisateur est connecté avant d'accéder à une route protégée.

```typescript
canActivate: [authGuard]
```

Si l'utilisateur n'est pas authentifié, il est redirigé vers :

```text
/connexion
```

#### RoleGuard

Le guard de rôle vérifie que l'utilisateur possède au moins un des rôles autorisés.

```typescript
canActivate: [authGuard, roleGuard],
data: {
  roles: ['GESTIONNAIRE_CATALOGUE', 'ADMIN']
}
```

### Masquage des boutons

Les fonctionnalités non autorisées sont masquées dans l'interface.

Exemple :

```html
@if (authService.hasAnyRole(['GESTIONNAIRE_CATALOGUE', 'ADMIN'])) {
  <a routerLink="/categories/ajouter">Ajouter une catégorie</a>
}
```

### Navigation conditionnelle

Les liens de navigation sont affichés selon le rôle connecté.

#### CLIENT

Visible :

- Catégories
- Espèces
- Variétés
- Aromates
- Produits

Masqué :

- Propriétés médicinales
- Utilisateurs
- Rôles

#### GESTIONNAIRE_CATALOGUE

Visible :

- Catégories
- Espèces
- Variétés
- Aromates
- Produits
- Propriétés médicinales

Masqué :

- Utilisateurs
- Rôles

#### ADMIN

Visible :

- Toutes les fonctionnalités

---

## Protection Backend

Les endpoints sensibles sont protégés grâce à :

```csharp
.RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
```

ou :

```csharp
.RequireAuthorization(policy => policy.RequireRole("ADMIN"))
```

La protection côté API constitue la véritable sécurité de l'application.

Même si un utilisateur tente un appel manuel avec :

- Postman
- Curl
- Swagger
- DevTools du navigateur

l'accès sera refusé si son rôle n'est pas autorisé.

---

# Routes protégées

## Catégories

### Public

```http
GET /api/categories
GET /api/categories/{id}
```

### Gestionnaire catalogue + Admin

```http
POST /api/categories
PUT /api/categories/{id}
DELETE /api/categories/{id}
DELETE /api/categories/{id}/reaffectation/{id}
```

---

## Espèces

### Public

```http
GET /api/especes
GET /api/especes/{id}
```

### Gestionnaire catalogue + Admin

```http
POST /api/especes
PUT /api/especes/{id}
DELETE /api/especes/{id}
```

---

## Variétés

### Public

```http
GET /api/varietes
GET /api/varietes/{id}
```

### Gestionnaire catalogue + Admin

```http
POST /api/varietes
PUT /api/varietes/{id}
DELETE /api/varietes/{id}
```

---

## Aromates

### Public

```http
GET /api/aromates
GET /api/aromates/{id}
```

### Gestionnaire catalogue + Admin

```http
POST /api/aromates
PUT /api/aromates/{id}
DELETE /api/aromates/{id}
```

---

## Produits

### Public

```http
GET /api/produits
GET /api/produits/{id}
GET /api/produits/categorie/{id}
```

### Gestionnaire catalogue + Admin

```http
POST /api/produits
PUT /api/produits/{id}
DELETE /api/produits/{id}
```

---

## Propriétés médicinales

### Gestionnaire catalogue + Admin

```http
GET /api/proprietes-medicinales
GET /api/proprietes-medicinales/{id}

POST /api/proprietes-medicinales
PUT /api/proprietes-medicinales/{id}
DELETE /api/proprietes-medicinales/{id}
```

---

## Utilisateurs

### Admin uniquement

```http
GET /api/utilisateurs
GET /api/utilisateurs/{id}

POST /api/utilisateurs
PUT /api/utilisateurs/{id}
DELETE /api/utilisateurs/{id}
```

---

## Rôles

### Admin uniquement

```http
GET /api/roles
GET /api/roles/{id}

POST /api/roles
PUT /api/roles/{id}
DELETE /api/roles/{id}
```

---

## Utilisateurs et rôles

### Admin uniquement

```http
GET /api/utilisateurs-roles/{utilisateurId}
PUT /api/utilisateurs-roles/{utilisateurId}
```

Cette route permet d'attribuer ou de retirer des rôles à un utilisateur.

---

## Adresses de livraison

### Admin uniquement

```http
GET /api/adresses-livraison/utilisateur/{utilisateurId}
GET /api/adresses-livraison/{id}

POST /api/adresses-livraison
PUT /api/adresses-livraison/{id}
DELETE /api/adresses-livraison/{id}
```

---

## Localités

### Public

```http
GET /api/localites
GET /api/localites/{id}
```

### Admin uniquement

```http
POST /api/localites
PUT /api/localites/{id}
DELETE /api/localites/{id}
```

---

## Authentification

### Public

```http
POST /api/auth/login
```

Cette route permet à un utilisateur de s'authentifier.

---

## Principe de sécurité

L'application applique une stratégie de défense à deux niveaux.

### Niveau 1 : Frontend

L'utilisateur ne voit que les fonctionnalités auxquelles il a accès.

Exemple :

```html
@if (authService.hasAnyRole(['GESTIONNAIRE_CATALOGUE', 'ADMIN'])) {
    ...
}
```

Cela améliore l'expérience utilisateur et limite les erreurs de manipulation.

### Niveau 2 : Backend

Les routes sensibles sont protégées par des contrôles d'autorisation.

Exemple :

```csharp
.RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
```

ou :

```csharp
.RequireAuthorization(policy => policy.RequireRole("ADMIN"))
```

Même si un utilisateur contourne l'interface graphique, l'API refusera toute opération non autorisée.

Le frontend améliore l'expérience utilisateur.

Le backend garantit la sécurité réelle de l'application.

---

# Base de données

Le projet peut fonctionner avec **SQL Server** ou **MySQL**.

## Structure des scripts

Les scripts SQL sont regroupés dans :

```text
backend/Infrastructure/Database
```

```text
Database/
├── MySql/
│   └── 00_create_database_and_all_tables.sql
│
├── SqlServer/
│   └── 00_create_database_and_all_tables.sql
│
├── Seed/
│   ├── 01_insert_categories.sql
│   ├── 02_insert_especes.sql
│   ├── 03_insert_varietes.sql
│   ├── 04_insert_aromates.sql
│   ├── 05_insert_proprietes_medicinales.sql
│   ├── 06_insert_aromates_proprietes.sql
│   ├── 07_insert_utilisateurs.sql
│   ├── 08_insert_localites.sql
│   ├── 09_insert_adresses_livraisons.sql
│   ├── 10_insert_roles.sql
│   ├── 11_insert_utilisateur_roles.sql
│   └── 12_insert_produits.sql
│
└── Scripts/
    ├── setup-db-mysql.bat
    └── setup-db-sqlserver.bat
```

## MySQL

Depuis le dossier :

```text
backend/Infrastructure/Database/Scripts
```

exécuter :

```powershell
.\setup-db-mysql.bat
```

Le script :

1. exécute `MySql/00_create_database_and_all_tables.sql`
2. crée la base et les tables si nécessaire
3. vérifie si la table `Categorie` est vide
4. exécute les scripts du dossier `Seed` uniquement si la base est vide
5. met à jour `Api/appsettings.json` avec :

```json
"DatabaseProvider": "MySql"
```

## SQL Server

Depuis le dossier :

```text
backend/Infrastructure/Database/Scripts
```

exécuter :

```powershell
.\setup-db-sqlserver.bat
```

Le script :

1. exécute `SqlServer/00_create_database_and_all_tables.sql`
2. crée la base et les tables si nécessaire
3. vérifie si la table `Categorie` est vide
4. exécute les scripts du dossier `Seed` uniquement si la base est vide
5. met à jour `Api/appsettings.json` avec :

```json
"DatabaseProvider": "SqlServer"
```

## Sélection du moteur

Le fichier `Api/appsettings.json` contient :

```json
"DatabaseProvider": "SqlServer"
```

ou :

```json
"DatabaseProvider": "MySql"
```

`Program.cs` lit cette valeur et charge automatiquement le fournisseur de base de données correspondant.

## Relance des scripts

Les scripts peuvent être exécutés plusieurs fois sans créer de doublons :

- si la base est vide → insertion des données de seed
- si la base contient déjà des données → seeds ignorés

---

# Structure du projet

```text
graines-app
│
├── frontend
│   ├── public
│   ├── src
│   │   ├── app
│   │   └── environments
│   ├── dist
│   ├── angular.json
│   ├── package.json
│   └── tsconfig.json
│
└── backend
    ├── Api
    │   ├── EndPoints
    │   ├── appsettings.json
    │   └── Program.cs
    │
    ├── Core
    │   ├── Models
    │   ├── IGateways
    │   ├── UseCases
    │   └── UseCases.Abstractions
    │
    ├── Infrastructure
    │   ├── Database
    │   │   ├── MySql
    │   │   ├── SqlServer
    │   │   ├── Seed
    │   │   └── Scripts
    │   ├── Gateways
    │   ├── Models
    │   ├── Repositories
    │   └── Repositories.Abstractions
    │
    └── publish
```

---

# Choix techniques

Plusieurs choix techniques ont été effectués lors du développement :

- utilisation d'une architecture en couches inspirée de la Clean Architecture
- séparation claire entre le frontend, l'API, le Core et l'Infrastructure
- utilisation de Dapper pour garder un contrôle direct sur les requêtes SQL
- séparation entre les modèles métier et les modèles de persistance
- utilisation de gateways pour isoler la logique métier des détails techniques
- abstraction de la connexion à la base de données via des factories
- compatibilité avec SQL Server et MySQL
- utilisation des Minimal APIs pour exposer les endpoints REST
- gestion des autorisations par rôles côté frontend et backend
- scripts d'installation pour faciliter la mise en place du projet

---

# Perspectives d'évolution

Plusieurs améliorations pourraient être ajoutées :

- ajout d'une gestion complète des commandes
- ajout d'un panier
- gestion avancée des stocks
- amélioration de la gestion des images
- ajout de tests automatisés
- déploiement dans un environnement cloud
- ajout de rapports statistiques
- ajout d'un historique des modifications
- amélioration de l'interface d'administration
- ajout d'une documentation Swagger

---

# Objectifs pédagogiques

Ce projet a permis de mettre en pratique :

- Angular moderne
- ASP.NET Core 10
- Dapper
- SQL Server
- MySQL
- Minimal APIs
- Architecture en couches
- Injection de dépendances
- Authentification
- Gestion des rôles
- API REST
- séparation des responsabilités
- inversion des dépendances
- scripts SQL d'installation
- configuration multi-base de données

---

# Auteur

Harry Francis