@echo off
echo ===============================================================================
echo SCRIPT DE CORRECCIÓN DE CADENAS DE CONEXIÓN
echo Unificando todas las cadenas de conexión para usar LocalDB
echo ===============================================================================
echo.

echo Verificando archivos de configuración...

if exist "App.config" (
    echo ✓ App.config encontrado
) else (
    echo ✗ App.config NO encontrado
)

if exist "Properties\Settings.settings" (
    echo ✓ Settings.settings encontrado
) else (
    echo ✗ Settings.settings NO encontrado
)

if exist "Properties\Settings.Designer.cs" (
    echo ✓ Settings.Designer.cs encontrado
) else (
    echo ✗ Settings.Designer.cs NO encontrado
)

echo.
echo ===============================================================================
echo INSTRUCCIONES IMPORTANTES:
echo ===============================================================================
echo.
echo 1. Los archivos de configuración han sido actualizados para usar LocalDB
echo 2. La nueva cadena de conexión es:
echo    Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Bike_Store;Integrated Security=True;TrustServerCertificate=True
echo.
echo 3. Pasos siguientes:
echo    a) Ejecuta el script Diagnose_Database_Connection.sql para verificar la BD
echo    b) Si la BD no existe, ejecuta Complete_Database_Script.sql
echo    c) Recompila la aplicación en Visual Studio
echo    d) Prueba la funcionalidad de reportes
echo.
echo 4. Si prefieres usar SQL Server Express, cambia la cadena de conexión a:
echo    Data Source=.\SQLEXPRESS;Initial Catalog=Bike_Store;Integrated Security=True;TrustServerCertificate=True
echo.
echo 5. Archivos actualizados:
echo    - App.config
echo    - Properties\Settings.settings
echo    - Properties\Settings.Designer.cs
echo    - bin\Debug\PedidosApp.exe.config
echo    - obj\Debug\PedidosApp.exe.config
echo.
echo ===============================================================================
echo CORRECCIÓN COMPLETADA
echo ===============================================================================

pause