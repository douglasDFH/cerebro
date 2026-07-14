"""
Módulo para generar gráficos del reporte de cierre de mes
"""
import matplotlib.pyplot as plt
import matplotlib.patches as mpatches
from matplotlib.patches import Wedge
import os

# Configuración de colores corporativos
COLOR_PRINCIPAL = '#2C5F5D'
COLOR_ACENTO = '#E31E24'
COLORES_CATEGORIAS = ['#FFA07A', '#FF7F50', '#FFD700', '#87CEEB', '#90EE90']


def crear_grafico_dona_ventas(contado_pct, minicuotas_pct, ruta_salida):
    """Crea gráfico de dona para ventas por tipo"""
    fig, ax = plt.subplots(figsize=(8, 6))

    sizes = [contado_pct, minicuotas_pct]
    labels = [f'Contado: {contado_pct}%', f'Minicuotas: {minicuotas_pct}%']
    colors = ['#87CEEB', '#505050']

    wedges, texts = ax.pie(sizes, colors=colors, startangle=90,
                           wedgeprops=dict(width=0.5, edgecolor='white'))

    # Añadir leyenda
    ax.legend(labels, loc='upper right', bbox_to_anchor=(1.2, 1))
    ax.set_title('VENTAS POR TIPO (Bs.)', fontsize=14, fontweight='bold', pad=20)

    plt.tight_layout()
    plt.savefig(ruta_salida, dpi=300, bbox_inches='tight', facecolor='white')
    plt.close()


def crear_grafico_categorias(categorias_data, ruta_salida, titulo="CATEGORIAS MAS VENDIDAS"):
    """
    Crea gráfico de barras comparativo para categorías
    categorias_data: lista de diccionarios con 'nombre', 'objetivo', 'ventas', 'alcance'
    """
    fig, ax = plt.subplots(figsize=(14, 8))

    nombres = [cat['nombre'] for cat in categorias_data]
    objetivos = [cat['objetivo'] for cat in categorias_data]
    ventas = [cat['ventas'] for cat in categorias_data]
    alcances = [cat['alcance'] for cat in categorias_data]

    x = range(len(nombres))
    width = 0.35

    # Crear barras
    barras_objetivo = ax.bar([i - width/2 for i in x], objetivos, width,
                             label='Objetivos Ventas Netas $',
                             color='#FFB6C1', edgecolor='white')
    barras_ventas = ax.bar([i + width/2 for i in x], ventas, width,
                           label='Ventas Netas $',
                           color='#FFA07A', edgecolor='white')

    # Añadir valores en las barras
    for i, (obj, vta) in enumerate(zip(objetivos, ventas)):
        ax.text(i - width/2, obj + 500, f'{obj:,}', ha='center', va='bottom', fontsize=10)
        ax.text(i + width/2, vta + 500, f'{vta:,}', ha='center', va='bottom', fontsize=10)

    # Añadir porcentajes de alcance
    for i, alcance in enumerate(alcances):
        ax.text(i, max(objetivos[i], ventas[i]) * 0.3, f'{alcance}%',
                ha='center', va='center', fontsize=16, fontweight='bold', color='#2C5F5D')

    ax.set_xlabel('')
    ax.set_ylabel('')
    ax.set_title(titulo, fontsize=16, fontweight='bold', pad=20, color='white',
                 bbox=dict(boxstyle='round', facecolor=COLOR_ACENTO, alpha=1))
    ax.set_xticks(x)
    ax.set_xticklabels(nombres, rotation=0, ha='center', fontsize=11)
    ax.legend(loc='upper left')
    ax.set_facecolor('#E8F4F3')
    fig.patch.set_facecolor('white')

    # Añadir tabla en la parte inferior
    tabla_datos = [
        ['Objetivos Ventas Netas $'] + [f'{obj:,}' for obj in objetivos],
        ['Ventas Netas $'] + [f'{vta:,}' for vta in ventas],
        ['Alcance Venta Neta'] + [f'{alc}%' for alc in alcances]
    ]

    tabla = ax.table(cellText=tabla_datos,
                     colLabels=[''] + nombres,
                     cellLoc='center',
                     loc='bottom',
                     bbox=[0, -0.35, 1, 0.25])

    tabla.auto_set_font_size(False)
    tabla.set_fontsize(9)
    tabla.scale(1, 1.5)

    # Estilo de la tabla
    for i in range(len(tabla_datos)):
        for j in range(len(nombres) + 1):
            cell = tabla[(i, j)]
            if i % 2 == 0:
                cell.set_facecolor('#D3E8E7')
            else:
                cell.set_facecolor('#E8F4F3')

    plt.tight_layout()
    plt.savefig(ruta_salida, dpi=300, bbox_inches='tight', facecolor='white')
    plt.close()


def crear_grafico_top_skus(skus_data, ruta_salida):
    """
    Crea gráfico de barras para Top 10 SKUs
    skus_data: lista de diccionarios con 'nombre' y 'cantidad'
    """
    fig, ax = plt.subplots(figsize=(14, 7))

    nombres = [sku['nombre'] for sku in skus_data]
    cantidades = [sku['cantidad'] for sku in skus_data]

    # Colores variados para las barras
    colores = ['#87CEEB', '#505050', '#90EE90', '#FFA07A', '#DDA0DD',
               '#FF69B4', '#FFD700', '#20B2AA', '#FF6B6B', '#87CEFA']

    barras = ax.bar(range(len(nombres)), cantidades, color=colores[:len(nombres)],
                    edgecolor='white', linewidth=2)

    # Añadir valores encima de las barras
    for i, (barra, cant) in enumerate(zip(barras, cantidades)):
        height = barra.get_height()
        ax.text(barra.get_x() + barra.get_width()/2., height,
                f'{cant}',
                ha='center', va='bottom', fontsize=11, fontweight='bold')

    ax.set_ylabel('SKU', fontsize=12, fontweight='bold')
    ax.set_xlabel('')
    ax.set_title('TOP 10 SKUS MES', fontsize=16, fontweight='bold', pad=20)
    ax.set_xticks(range(len(nombres)))
    ax.set_xticklabels(nombres, rotation=45, ha='right', fontsize=9)
    ax.set_ylim(0, max(cantidades) * 1.15)
    ax.set_facecolor('white')
    fig.patch.set_facecolor('white')

    plt.tight_layout()
    plt.savefig(ruta_salida, dpi=300, bbox_inches='tight', facecolor='white')
    plt.close()


def limpiar_graficos_temporales(directorio='assets'):
    """Limpia archivos temporales de gráficos"""
    if os.path.exists(directorio):
        for archivo in os.listdir(directorio):
            if archivo.endswith('.png'):
                try:
                    os.remove(os.path.join(directorio, archivo))
                except:
                    pass
