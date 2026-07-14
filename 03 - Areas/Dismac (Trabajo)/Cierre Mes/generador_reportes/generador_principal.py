"""
Módulo principal para generar el reporte completo de cierre de mes
"""
import os
from graficos import (
    crear_grafico_dona_ventas,
    crear_grafico_categorias,
    crear_grafico_top_skus,
    limpiar_graficos_temporales
)
from exportar_pptx import (
    crear_presentacion,
    agregar_portada,
    agregar_objetivos,
    agregar_ticket_promedio,
    agregar_grafico_categorias as agregar_slide_categorias,
    agregar_mystery_shopper,
    agregar_gracias,
    guardar_presentacion
)


def generar_reporte_completo(datos_generales, categorias, skus, mystery_shopper, ruta_salida):
    """
    Genera el reporte completo en PowerPoint

    Args:
        datos_generales: dict con información general del mes
        categorias: lista de dict con datos de categorías
        skus: lista de dict con datos de SKUs
        mystery_shopper: dict con datos de mystery shopper
        ruta_salida: ruta donde guardar el archivo PowerPoint
    """
    # Crear directorio de assets si no existe
    if not os.path.exists('assets'):
        os.makedirs('assets')

    # Limpiar gráficos anteriores
    limpiar_graficos_temporales('assets')

    # Generar gráficos
    print("Generando gráficos...")

    # 1. Gráfico de dona para ventas por tipo
    grafico_dona_path = 'assets/grafico_dona.png'
    crear_grafico_dona_ventas(
        datos_generales['contado_pct'],
        datos_generales['minicuotas_pct'],
        grafico_dona_path
    )

    # 2. Gráfico de categorías más vendidas
    grafico_categorias_mas_path = 'assets/grafico_categorias_mas.png'
    crear_grafico_categorias(
        categorias,
        grafico_categorias_mas_path,
        titulo="CATEGORIAS MAS VENDIDAS"
    )

    # 3. Gráfico de categorías menos vendidas (ordenadas por alcance)
    categorias_ordenadas = sorted(categorias, key=lambda x: x['alcance'])
    grafico_categorias_menos_path = 'assets/grafico_categorias_menos.png'
    crear_grafico_categorias(
        categorias_ordenadas,
        grafico_categorias_menos_path,
        titulo="CATEGORIAS MENOS VENDIDAS POR ALCANCE EN %"
    )

    # 4. Gráfico de Top 10 SKUs
    grafico_skus_path = 'assets/grafico_skus.png'
    if skus:
        crear_grafico_top_skus(skus, grafico_skus_path)

    print("Gráficos generados exitosamente")

    # Crear presentación PowerPoint
    print("Creando presentación PowerPoint...")
    prs = crear_presentacion()

    # Agregar diapositivas
    print("Agregando diapositiva de portada...")
    agregar_portada(
        prs,
        datos_generales['mes'],
        datos_generales['anio'],
        datos_generales['tienda']
    )

    print("Agregando diapositiva de objetivos...")
    agregar_objetivos(
        prs,
        datos_generales['objetivo_usd'],
        datos_generales['objetivo_bs'],
        datos_generales['alcance'],
        datos_generales['ventas_usd'],
        datos_generales['ventas_bs']
    )

    print("Agregando diapositiva de ticket promedio...")
    agregar_ticket_promedio(
        prs,
        datos_generales['ticket_usd'],
        datos_generales['ticket_bs'],
        datos_generales['contado_pct'],
        datos_generales['minicuotas_pct'],
        grafico_dona_path if os.path.exists(grafico_dona_path) else None
    )

    print("Agregando diapositiva de categorías más vendidas...")
    agregar_slide_categorias(
        prs,
        "CATEGORIAS MAS VENDIDAS",
        grafico_categorias_mas_path if os.path.exists(grafico_categorias_mas_path) else None
    )

    print("Agregando diapositiva de categorías menos vendidas...")
    agregar_slide_categorias(
        prs,
        "CATEGORIAS MENOS VENDIDAS POR ALCANCE EN %",
        grafico_categorias_menos_path if os.path.exists(grafico_categorias_menos_path) else None
    )

    if skus and os.path.exists(grafico_skus_path):
        print("Agregando diapositiva de Top 10 SKUs...")
        agregar_slide_categorias(
            prs,
            "TOP 10 SKUS MES",
            grafico_skus_path
        )

    print("Agregando diapositiva de Mystery Shopper...")
    agregar_mystery_shopper(
        prs,
        mystery_shopper['indice_general'],
        mystery_shopper['atencion_vendedor'],
        mystery_shopper['puntos_mejorar']
    )

    print("Agregando diapositiva de agradecimiento...")
    agregar_gracias(prs)

    # Guardar presentación
    print(f"Guardando presentación en {ruta_salida}...")
    guardar_presentacion(prs, ruta_salida)

    print(f"\n¡Reporte generado exitosamente!")
    print(f"Ubicación: {ruta_salida}")

    # Limpiar archivos temporales
    limpiar_graficos_temporales('assets')

    return ruta_salida


if __name__ == "__main__":
    # Ejemplo de uso
    datos_ejemplo = {
        'mes': 'DICIEMBRE',
        'anio': '2024',
        'tienda': 'TIENDA CRISTOBAL DE MENDOZA',
        'objetivo_usd': 402918,
        'objetivo_bs': 2804309,
        'ventas_usd': 404134,
        'ventas_bs': 2812772.54,
        'alcance': 100,
        'ticket_usd': 394,
        'ticket_bs': 2744.17,
        'contado_pct': 36.5,
        'minicuotas_pct': 63.5
    }

    categorias_ejemplo = [
        {'nombre': 'CUIDADO PERSONAL', 'objetivo': 10399, 'ventas': 6753, 'alcance': 65},
        {'nombre': 'ELECTRO MENOR', 'objetivo': 28263, 'ventas': 20111, 'alcance': 71},
        {'nombre': 'FREEZERS', 'objetivo': 10748, 'ventas': 8544, 'alcance': 79},
        {'nombre': 'AIRE ACONDICIONADO', 'objetivo': 33085, 'ventas': 27246, 'alcance': 82},
        {'nombre': 'CLIMATIZACION', 'objetivo': 4257, 'ventas': 3565, 'alcance': 84}
    ]

    skus_ejemplo = [
        {'nombre': 'EXCE610003', 'cantidad': 10},
        {'nombre': 'EV22403', 'cantidad': 8},
        {'nombre': 'FLE-DISCO-37161', 'cantidad': 8},
        {'nombre': 'BE548', 'cantidad': 8},
        {'nombre': 'PP8857', 'cantidad': 7}
    ]

    mystery_ejemplo = {
        'indice_general': 93,
        'atencion_vendedor': 83,
        'puntos_mejorar': [
            {'descripcion': 'Comunicar precios bajos y garantía', 'porcentaje': 50},
            {'descripcion': 'Explicar garantía extendida', 'porcentaje': 50},
            {'descripcion': 'Ofrecer servicio de entrega gratuita', 'porcentaje': 0}
        ]
    }

    generar_reporte_completo(
        datos_ejemplo,
        categorias_ejemplo,
        skus_ejemplo,
        mystery_ejemplo,
        'output/Cierre_Mes_Diciembre_2024.pptx'
    )
