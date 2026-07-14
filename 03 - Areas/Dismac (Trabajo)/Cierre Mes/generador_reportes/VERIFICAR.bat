@echo off
echo ============================================================
echo VERIFICADOR - Generador de Reportes DISMAC
echo ============================================================
echo.
echo Verificando instalacion de Python...
python --version
if errorlevel 1 (
    echo.
    echo ERROR: Python no esta instalado
    echo Por favor instale Python desde https://www.python.org/downloads/
    pause
    exit /b 1
)
echo.
echo Verificando dependencias...
echo.
python -c "import sys; import importlib.util; deps = ['pptx', 'matplotlib', 'PIL']; missing = [d for d in deps if importlib.util.find_spec(d.replace('pptx', 'python_pptx').replace('PIL', 'PIL')) is None]; sys.exit(0 if not missing else 1)"

if errorlevel 1 (
    echo.
    echo ADVERTENCIA: Faltan dependencias
    echo Por favor ejecute INSTALAR.bat primero
    echo.
) else (
    echo.
    echo ============================================================
    echo TODO LISTO! Puede ejecutar EJECUTAR.bat
    echo ============================================================
    echo.
)

echo Verificando archivos del proyecto...
echo.
if exist "main.py" (echo [OK] main.py) else (echo [FALTA] main.py)
if exist "interfaz_gui.py" (echo [OK] interfaz_gui.py) else (echo [FALTA] interfaz_gui.py)
if exist "generador_principal.py" (echo [OK] generador_principal.py) else (echo [FALTA] generador_principal.py)
if exist "graficos.py" (echo [OK] graficos.py) else (echo [FALTA] graficos.py)
if exist "exportar_pptx.py" (echo [OK] exportar_pptx.py) else (echo [FALTA] exportar_pptx.py)
if exist "requirements.txt" (echo [OK] requirements.txt) else (echo [FALTA] requirements.txt)
echo.

echo Verificando sintaxis de archivos Python...
python -m py_compile main.py
if errorlevel 1 (
    echo [ERROR] main.py tiene errores de sintaxis
) else (
    echo [OK] main.py - sintaxis correcta
)

python -m py_compile interfaz_gui.py
if errorlevel 1 (
    echo [ERROR] interfaz_gui.py tiene errores de sintaxis
) else (
    echo [OK] interfaz_gui.py - sintaxis correcta
)

python -m py_compile graficos.py
if errorlevel 1 (
    echo [ERROR] graficos.py tiene errores de sintaxis
) else (
    echo [OK] graficos.py - sintaxis correcta
)

python -m py_compile exportar_pptx.py
if errorlevel 1 (
    echo [ERROR] exportar_pptx.py tiene errores de sintaxis
) else (
    echo [OK] exportar_pptx.py - sintaxis correcta
)

python -m py_compile generador_principal.py
if errorlevel 1 (
    echo [ERROR] generador_principal.py tiene errores de sintaxis
) else (
    echo [OK] generador_principal.py - sintaxis correcta
)

echo.
echo ============================================================
echo Verificacion completada
echo ============================================================
echo.
pause
