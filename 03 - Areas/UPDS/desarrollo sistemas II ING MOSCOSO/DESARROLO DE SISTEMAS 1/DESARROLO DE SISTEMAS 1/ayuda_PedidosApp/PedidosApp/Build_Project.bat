@echo off
echo ================================================
echo COMPILANDO PROYECTO PEDIDOSAPP
echo ================================================

REM Primero intentar con Visual Studio MSBuild
echo Intentando compilacion con Visual Studio MSBuild...
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" PedidosApp.sln /p:Configuration=Debug /p:Platform="Any CPU"

REM Si falla, intentar con Framework MSBuild
if errorlevel 1 (
    echo.
    echo Intentando con .NET Framework MSBuild...
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe" PedidosApp.sln /p:Configuration=Debug /p:Platform="Any CPU"
)

REM Si falla, intentar con MSBuild del Framework
if errorlevel 1 (
    echo.
    echo Intentando con MSBuild 4.0...
    "%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" PedidosApp.sln /p:Configuration=Debug /p:Platform="Any CPU"
)

if errorlevel 1 (
    echo.
    echo ERROR: No se pudo compilar el proyecto
    echo Puede que necesite abrir el proyecto en Visual Studio
    echo y compilar manualmente
) else (
    echo.
    echo COMPILACION EXITOSA!
    echo El ejecutable esta en: bin\Debug\PedidosApp.exe
)

echo.
pause