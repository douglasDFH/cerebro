@echo off
:: ================================================================
:: Script para reparar y resetear la configuracion de red en Windows 10
:: Ejecutar como Administrador
:: ================================================================

echo ========================================
echo REPARACION DE RED - WINDOWS 10
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

echo [1/7] Verificando estado actual de red...
echo ========================================
ipconfig /all
echo.
pause

echo.
echo [2/7] Liberando direccion IP actual...
echo ========================================
ipconfig /release
echo.

echo [3/7] Renovando direccion IP...
echo ========================================
ipconfig /renew
echo.

echo [4/7] Limpiando cache DNS...
echo ========================================
ipconfig /flushdns
echo.

echo [5/7] Reseteando Winsock...
echo ========================================
netsh winsock reset
echo.

echo [6/7] Reseteando TCP/IP...
echo ========================================
netsh int ip reset
echo.

echo [7/7] Reiniciando adaptadores de red...
echo ========================================
netsh interface set interface "Ethernet" admin=disable
timeout /t 2 /nobreak >nul
netsh interface set interface "Ethernet" admin=enable
echo.

echo ========================================
echo Esperando 5 segundos para que la red se estabilice...
timeout /t 5 /nobreak
echo.

echo Verificando nueva configuracion...
echo ========================================
ipconfig
echo.

echo Probando conectividad...
echo ========================================
ping 8.8.8.8 -n 4
echo.

echo ========================================
echo REPARACION COMPLETADA
echo ========================================
echo.
echo IMPORTANTE: Si los cambios no funcionan, reinicia Windows.
echo.
pause
