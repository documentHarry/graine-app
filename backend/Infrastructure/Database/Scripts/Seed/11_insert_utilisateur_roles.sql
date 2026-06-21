USE graines;

-- Tous les utilisateurs existants reçoivent le rôle CLIENT.
INSERT INTO UtilisateurRole (UtilisateurId, RoleId)
SELECT
    Utilisateur.IdUtilisateur,
    Role.IdRole
FROM Utilisateur
JOIN Role ON Role.NomRole = 'CLIENT';

-- Compte de test : ADMIN.
INSERT INTO UtilisateurRole (UtilisateurId, RoleId)
SELECT
    Utilisateur.IdUtilisateur,
    Role.IdRole
FROM Utilisateur
JOIN Role ON Role.NomRole = 'ADMIN'
WHERE Utilisateur.Email = 'jthomas@example.org';

-- Compte de test : GESTIONNAIRE_CATALOGUE.
INSERT INTO UtilisateurRole (UtilisateurId, RoleId)
SELECT
    Utilisateur.IdUtilisateur,
    Role.IdRole
FROM Utilisateur
JOIN Role ON Role.NomRole = 'GESTIONNAIRE_CATALOGUE'
WHERE Utilisateur.Email = 'daan27@example.org';
