@echo off
:: ================================================================
:: Script para desinstalar SQL Server completamente
:: Ejecutar como Administrador
:: ================================================================

echo ========================================
echo DESINSTALADOR DE SQL SERVER
echo ========================================
echo.

:: Verificar privilegios de administrador
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo ERROR: Este script requiere privilegios de administrador
    echo Por favor, ejecuta como Administrador
    pause
    exit /b 1
)

echo [1/5] Verificando versiones de SQL Server instaladas...
echo ========================================
wmic product where "name like '%%SQL Server%%'" get name,version
echo.
echo ========================================

pause

echo.
echo [2/5] Deteniendo servicios de SQL Server...
echo ========================================

:: Detener servicios de SQL Server
net stop MSSQLSERVER /y 2>nul
net stop SQLServerAgent /y 2>nul
net stop MSSQLServerOLAPService /y 2>nul
net stop ReportServer /y 2>nul
net stop SQLSERVERAGENT /y 2>nul
net stop SQLBrowser /y 2>nul
net stop MSSQLFDLauncher /y 2>nul
net stop SSASTELEMETRY /y 2>nul
net stop SSISTELEMETRY /y 2>nul
net stop SQLWriter /y 2>nul
net stop MsDtsServer150 /y 2>nul
net stop MsDtsServer140 /y 2>nul
net stop MsDtsServer130 /y 2>nul
net stop MsDtsServer120 /y 2>nul
net stop MsDtsServer110 /y 2>nul
net stop MsDtsServer100 /y 2>nul

echo Servicios detenidos.
echo.

pause

echo.
echo [3/5] Desinstalando componentes de SQL Server...
echo ========================================
echo ADVERTENCIA: Esto puede tomar varios minutos
echo No cierres esta ventana
echo.

:: Desinstalar todos los componentes de SQL Server
wmic product where "name like '%%SQL Server%%'" call uninstall /nointeractive

echo.
echo Desinstalacion de componentes completada.
echo.

pause

echo.
echo [4/5] Eliminando carpetas residuales...
echo ========================================

:: Eliminar carpetas de SQL Server
if exist "C:\Program Files\Microsoft SQL Server" (
    echo Eliminando: C:\Program Files\Microsoft SQL Server
    rd /s /q "C:\Program Files\Microsoft SQL Server" 2>nul
)

if exist "C:\Program Files (x86)\Microsoft SQL Server" (
    echo Eliminando: C:\Program Files (x86)\Microsoft SQL Server
    rd /s /q "C:\Program Files (x86)\Microsoft SQL Server" 2>nul
)

if exist "C:\Program Files\Microsoft SQL Server Management Studio" (
    echo Eliminando: C:\Program Files\Microsoft SQL Server Management Studio
    rd /s /q "C:\Program Files\Microsoft SQL Server Management Studio" 2>nul
)

if exist "C:\Program Files (x86)\Microsoft SQL Server Management Studio" (
    echo Eliminando: C:\Program Files (x86)\Microsoft SQL Server Management Studio
    rd /s /q "C:\Program Files (x86)\Microsoft SQL Server Management Studio" 2>nul
)

echo.
echo Carpetas eliminadas.
echo.

pause

echo.
echo [5/5] Limpiando claves de registro (opcional)...
echo ========================================
echo ADVERTENCIA: Esto eliminara las claves de registro de SQL Server
echo Si no estas seguro, cancela esta operacion (Ctrl+C)
echo.

pause

:: Eliminar claves de registro de SQL Server
reg delete "HKLM\SOFTWARE\Microsoft\Microsoft SQL Server" /f 2>nul
reg delete "HKLM\SOFTWARE\Wow6432Node\Microsoft\Microsoft SQL Server" /f 2>nul
reg delete "HKLM\SYSTEM\CurrentControlSet\Services\MSSQLSERVER" /f 2>nul
reg delete "HKLM\SYSTEM\CurrentControlSet\Services\SQLServerAgent" /f 2>nul
reg delete "HKLM\SYSTEM\CurrentControlSet\Services\MSSQLServerOLAPService" /f 2>nul
reg delete "HKLM\SYSTEM\CurrentControlSet\Services\ReportServer" /f 2>nul
reg delete "HKLM\SYSTEM\CurrentControlSet\Services\SQLBrowser" /f 2>nul

echo.
echo ========================================
echo DESINSTALACION COMPLETADA
echo ========================================
echo.
echo SQL Server ha sido desinstalado completamente.
echo Se recomienda reiniciar el sistema.
echo.
echo Deseas reiniciar ahora? (S/N)
set /p respuesta=

if /i "%respuesta%"=="S" (
    echo Reiniciando sistema en 10 segundos...
    shutdown /r /t 10
) else (
    echo Por favor, reinicia manualmente cuando sea conveniente.
)

echo.
pause
