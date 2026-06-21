@echo off

set SQLCMD=sqlcmd
set SERVER=(localdb)\MSSQLLocalDB
set SCRIPT=all_sqlserver_scripts.sql

echo Creation de la base et des tables...

copy /b "..\SqlServer\00_create_database_and_all_tables.sql" "%SCRIPT%" > nul

"%SQLCMD%" -S "%SERVER%" -E -f 65001 -i "%SCRIPT%"

del "%SCRIPT%"

echo Verification des donnees existantes...

set ROW_COUNT=

for /f "usebackq delims=" %%C in (`powershell -NoProfile -Command "sqlcmd -S '%SERVER%' -E -d graines -h -1 -W -Q 'SET NOCOUNT ON; SELECT COUNT(*) FROM dbo.Categorie;'"`) do set "ROW_COUNT=%%C"

if "%ROW_COUNT%"=="0" (
    echo Base vide, insertion des seeds...

    type nul > "%SCRIPT%"

    type "..\Seed\01_insert_categories.sql" >> "%SCRIPT%"
    type "..\Seed\02_insert_especes.sql" >> "%SCRIPT%"
    type "..\Seed\03_insert_varietes.sql" >> "%SCRIPT%"
    type "..\Seed\04_insert_aromates.sql" >> "%SCRIPT%"
    type "..\Seed\05_insert_proprietes_medicinales.sql" >> "%SCRIPT%"
    type "..\Seed\06_insert_aromates_proprietes.sql" >> "%SCRIPT%"
    type "..\Seed\07_insert_utilisateurs.sql" >> "%SCRIPT%"
    type "..\Seed\08_insert_localites.sql" >> "%SCRIPT%"
    type "..\Seed\09_insert_adresses_livraisons.sql" >> "%SCRIPT%"
    type "..\Seed\10_insert_roles.sql" >> "%SCRIPT%"
    type "..\Seed\11_insert_utilisateur_roles.sql" >> "%SCRIPT%"
    type "..\Seed\12_insert_produits.sql" >> "%SCRIPT%"

    "%SQLCMD%" -S "%SERVER%" -E -d graines -f 65001 -i "%SCRIPT%"

    del "%SCRIPT%"
) else (
    echo Base deja remplie, seeds ignores.
)

echo Mise a jour de appsettings.json...

(
echo {
echo   "DatabaseProvider": "SqlServer",
echo   "ConnectionStrings": {
echo     "SqlServerConnection": "Server=(localdb)\\MSSQLLocalDB;Database=graines;Trusted_Connection=True;TrustServerCertificate=True;",
echo     "MySqlConnection": "Server=localhost;Database=graines;User=root;"
echo   },
echo   "Logging": {
echo     "LogLevel": {
echo       "Default": "Information",
echo       "Microsoft.AspNetCore": "Warning"
echo     }
echo   },
echo   "AllowedHosts": "*"
echo }
) > "..\..\..\Api\appsettings.json"

echo Base SQL Server initialisee.