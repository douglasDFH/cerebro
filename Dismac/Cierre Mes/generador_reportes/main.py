"""
Archivo principal para ejecutar el generador de reportes DISMAC
"""
import sys
import os

# Agregar el directorio actual al path
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))

from interfaz_gui import iniciar_aplicacion

if __name__ == "__main__":
    print("=" * 60)
    print("GENERADOR DE REPORTES DE CIERRE DE MES - DISMAC")
    print("=" * 60)
    print("\nIniciando aplicación...")
    print("Por favor espere mientras se carga la interfaz gráfica...\n")

    try:
        iniciar_aplicacion()
    except Exception as e:
        print(f"\nError al iniciar la aplicación: {e}")
        print("\nAsegúrese de tener instaladas todas las dependencias:")
        print("pip install -r requirements.txt")
        input("\nPresione Enter para salir...")
