@echo off
:: ================================================================
:: Script para verificar conectividad de Internet
:: ================================================================

echo ========================================
echo VERIFICACION DE CONECTIVIDAD A INTERNET
echo ========================================
echo.

echo [1] Verificando configuracion de red...
echo ========================================
ipconfig | findstr /i "IPv4 Gateway DNS"
echo.

echo [2] Verificando conectividad a DNS de Google (8.8.8.8)...
echo ========================================
ping 8.8.8.8 -n 4
echo.

echo [3] Verificando conectividad a Google.com...
echo ========================================
ping google.com -n 4
echo.

echo [4] Verificando DNS...
echo ========================================
nslookup google.com
echo.

echo [5] Probando conexion HTTP...
echo ========================================
powershell -Command "try { $response = Invoke-WebRequest -Uri 'http://google.com' -UseBasicParsing -TimeoutSec 5; Write-Host 'Conexion HTTP exitosa - Status:' $response.StatusCode } catch { Write-Host 'Error en conexion HTTP:' $_.Exception.Message }"
echo.

echo ========================================
echo VERIFICACION COMPLETADA
echo ========================================
echo.
echo Si todos los tests fueron exitosos, tu VM tiene internet funcionando!
echo.
pause
