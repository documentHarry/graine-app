@echo off

set MYSQL=C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe
set SERVER=localhost
set USER=root
set SCRIPT=all_mysql_scripts.sql

for /f "usebackq delims=" %%P in (`powershell -NoProfile -Command "$p = Read-Host 'Mot de passe MySQL root' -AsSecureString; $b = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($p); [Runtime.InteropServices.Marshal]::PtrToStringAuto($b)"`) do set "MYSQL_PASSWORD=%%P"

echo Creation de la base et des tables...

copy /b "..\Scripts\MySql\00_create_database_and_all_tables.sql" "%SCRIPT%" > nul

"%MYSQL%" --default-character-set=utf8mb4 -h %SERVER% -u %USER% -p%MYSQL_PASSWORD% < "%SCRIPT%"

del "%SCRIPT%"

echo Verification des donnees existantes...

set ROW_COUNT=

for /f "usebackq delims=" %%C in (`powershell -NoProfile -Command "& '%MYSQL%' -h %SERVER% -u %USER% -p%MYSQL_PASSWORD% -N -B -e 'SELECT COUNT(*) FROM graines.Categorie;'"`) do set "ROW_COUNT=%%C"

if "%ROW_COUNT%"=="0" (
    echo Base vide, insertion des seeds...

    type nul > "%SCRIPT%"

    type "..\Scripts\Seed\01_insert_categories.sql" >> "%SCRIPT%"
    type "..\Scripts\Seed\02_insert_especes.sql" >> "%SCRIPT%"
    type "..\Scripts\Seed\03_insert_varietes.sql" >> "%SCRIPT%"
    type "..\Scripts\Seed\04_insert_aromates.sql" >> "%SCRIPT%"
    type "..\Scripts\Seed\05_insert_proprietes_medicinales.sql" >> "%SCRIPT%"
    type "..\Scripts\Seed\06_insert_aromates_proprietes.sql" >> "%SCRIPT%"
    type "..\Scripts\Seed\07_insert_utilisateurs.sql" >> "%SCRIPT%"
    type "..\Scripts\Seed\08_insert_localites.sql" >> "%SCRIPT%"
    type "..\Scripts\Seed\09_insert_adresses_livraisons.sql" >> "%SCRIPT%"
    type "..\Scripts\Seed\10_insert_roles.sql" >> "%SCRIPT%"
    type "..\Scripts\Seed\11_insert_utilisateur_roles.sql" >> "%SCRIPT%"
    type "..\Scripts\Seed\12_insert_produits.sql" >> "%SCRIPT%"

    "%MYSQL%" --default-character-set=utf8mb4 -h %SERVER% -u %USER% -p%MYSQL_PASSWORD% graines < "%SCRIPT%"

    del "%SCRIPT%"
) else (
    echo Base deja remplie, seeds ignores.
)

echo Mise a jour de appsettings.json...

(
echo {
echo   "DatabaseProvider": "MySql",
echo   "ConnectionStrings": {
echo     "SqlServerConnection": "Server=(localdb)\\MSSQLLocalDB;Database=graines;Trusted_Connection=True;TrustServerCertificate=True;",
echo     "MySqlConnection": "Server=localhost;Database=graines;User=root;Password=%MYSQL_PASSWORD%;"
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

echo Base MySQL initialisee.