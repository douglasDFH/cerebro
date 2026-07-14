"""
Interfaz gráfica para el generador de reportes de cierre de mes
"""
import tkinter as tk
from tkinter import ttk, messagebox, filedialog
import os
from datetime import datetime


class ReporteGUI:
    def __init__(self, root):
        self.root = root
        self.root.title("Generador de Reportes - DISMAC")
        self.root.geometry("900x700")
        self.root.configure(bg='#2C5F5D')

        # Variables para almacenar datos
        self.datos_generales = {}
        self.categorias = []
        self.skus = []
        self.mystery_shopper = {}

        # Crear notebook (pestañas)
        self.notebook = ttk.Notebook(self.root)
        self.notebook.pack(fill='both', expand=True, padx=10, pady=10)

        # Crear pestañas
        self.crear_pestaña_general()
        self.crear_pestaña_categorias()
        self.crear_pestaña_skus()
        self.crear_pestaña_mystery()

        # Botón de generar reporte
        btn_generar = tk.Button(self.root, text="GENERAR REPORTE",
                               command=self.generar_reporte,
                               bg='#E31E24', fg='white',
                               font=('Arial', 14, 'bold'),
                               padx=20, pady=10)
        btn_generar.pack(pady=10)

    def crear_pestaña_general(self):
        """Crea la pestaña de datos generales"""
        frame = ttk.Frame(self.notebook)
        self.notebook.add(frame, text='Datos Generales')

        # Canvas y scrollbar
        canvas = tk.Canvas(frame, bg='white')
        scrollbar = ttk.Scrollbar(frame, orient="vertical", command=canvas.yview)
        scrollable_frame = ttk.Frame(canvas)

        scrollable_frame.bind(
            "<Configure>",
            lambda e: canvas.configure(scrollregion=canvas.bbox("all"))
        )

        canvas.create_window((0, 0), window=scrollable_frame, anchor="nw")
        canvas.configure(yscrollcommand=scrollbar.set)

        # Título
        tk.Label(scrollable_frame, text="INFORMACIÓN GENERAL DEL MES",
                font=('Arial', 16, 'bold'), bg='white', fg='#2C5F5D').grid(
                row=0, column=0, columnspan=2, pady=20)

        row = 1

        # Mes y Año
        tk.Label(scrollable_frame, text="Mes:", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.mes_var = tk.StringVar(value="DICIEMBRE")
        meses = ["ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO",
                "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"]
        ttk.Combobox(scrollable_frame, textvariable=self.mes_var, values=meses,
                    width=30).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        tk.Label(scrollable_frame, text="Año:", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.anio_var = tk.StringVar(value=str(datetime.now().year))
        tk.Entry(scrollable_frame, textvariable=self.anio_var, width=32,
                font=('Arial', 11)).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        # Tienda
        tk.Label(scrollable_frame, text="Tienda:", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.tienda_var = tk.StringVar(value="TIENDA CRISTOBAL DE MENDOZA")
        tk.Entry(scrollable_frame, textvariable=self.tienda_var, width=32,
                font=('Arial', 11)).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        # Separador
        ttk.Separator(scrollable_frame, orient='horizontal').grid(
            row=row, column=0, columnspan=2, sticky='ew', pady=15)
        row += 1

        # Objetivos
        tk.Label(scrollable_frame, text="OBJETIVOS Y VENTAS",
                font=('Arial', 14, 'bold'), bg='white', fg='#2C5F5D').grid(
                row=row, column=0, columnspan=2, pady=10)
        row += 1

        tk.Label(scrollable_frame, text="Objetivo ($us):", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.objetivo_usd_var = tk.StringVar(value="402918")
        tk.Entry(scrollable_frame, textvariable=self.objetivo_usd_var, width=32,
                font=('Arial', 11)).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        tk.Label(scrollable_frame, text="Objetivo (Bs):", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.objetivo_bs_var = tk.StringVar(value="2804309")
        tk.Entry(scrollable_frame, textvariable=self.objetivo_bs_var, width=32,
                font=('Arial', 11)).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        tk.Label(scrollable_frame, text="Ventas ($us):", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.ventas_usd_var = tk.StringVar(value="404134")
        tk.Entry(scrollable_frame, textvariable=self.ventas_usd_var, width=32,
                font=('Arial', 11)).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        tk.Label(scrollable_frame, text="Ventas (Bs):", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.ventas_bs_var = tk.StringVar(value="2812772.54")
        tk.Entry(scrollable_frame, textvariable=self.ventas_bs_var, width=32,
                font=('Arial', 11)).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        # Separador
        ttk.Separator(scrollable_frame, orient='horizontal').grid(
            row=row, column=0, columnspan=2, sticky='ew', pady=15)
        row += 1

        # Ticket promedio
        tk.Label(scrollable_frame, text="TICKET PROMEDIO",
                font=('Arial', 14, 'bold'), bg='white', fg='#2C5F5D').grid(
                row=row, column=0, columnspan=2, pady=10)
        row += 1

        tk.Label(scrollable_frame, text="Ticket Promedio ($us):", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.ticket_usd_var = tk.StringVar(value="394")
        tk.Entry(scrollable_frame, textvariable=self.ticket_usd_var, width=32,
                font=('Arial', 11)).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        tk.Label(scrollable_frame, text="Ticket Promedio (Bs):", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.ticket_bs_var = tk.StringVar(value="2744.17")
        tk.Entry(scrollable_frame, textvariable=self.ticket_bs_var, width=32,
                font=('Arial', 11)).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        # Separador
        ttk.Separator(scrollable_frame, orient='horizontal').grid(
            row=row, column=0, columnspan=2, sticky='ew', pady=15)
        row += 1

        # Ventas por tipo
        tk.Label(scrollable_frame, text="VENTAS POR TIPO",
                font=('Arial', 14, 'bold'), bg='white', fg='#2C5F5D').grid(
                row=row, column=0, columnspan=2, pady=10)
        row += 1

        tk.Label(scrollable_frame, text="Contado (%):", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.contado_pct_var = tk.StringVar(value="36.5")
        tk.Entry(scrollable_frame, textvariable=self.contado_pct_var, width=32,
                font=('Arial', 11)).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        tk.Label(scrollable_frame, text="Minicuotas (%):", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.minicuotas_pct_var = tk.StringVar(value="63.5")
        tk.Entry(scrollable_frame, textvariable=self.minicuotas_pct_var, width=32,
                font=('Arial', 11)).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        canvas.pack(side="left", fill="both", expand=True)
        scrollbar.pack(side="right", fill="y")

    def crear_pestaña_categorias(self):
        """Crea la pestaña de categorías"""
        frame = ttk.Frame(self.notebook)
        self.notebook.add(frame, text='Categorías')

        tk.Label(frame, text="CATEGORÍAS (Objetivos vs Ventas)",
                font=('Arial', 14, 'bold'), bg='white').pack(pady=10)

        # Frame para botones
        btn_frame = tk.Frame(frame, bg='white')
        btn_frame.pack(pady=5)

        tk.Button(btn_frame, text="Agregar Categoría",
                 command=self.agregar_categoria,
                 bg='#2C5F5D', fg='white', font=('Arial', 10)).pack(side='left', padx=5)

        tk.Button(btn_frame, text="Eliminar Seleccionada",
                 command=self.eliminar_categoria,
                 bg='#E31E24', fg='white', font=('Arial', 10)).pack(side='left', padx=5)

        # Treeview para categorías
        columns = ('Nombre', 'Objetivo', 'Ventas', 'Alcance %')
        self.tree_categorias = ttk.Treeview(frame, columns=columns, show='headings', height=15)

        for col in columns:
            self.tree_categorias.heading(col, text=col)
            self.tree_categorias.column(col, width=150)

        self.tree_categorias.pack(fill='both', expand=True, padx=10, pady=10)

        # Scrollbar
        scrollbar = ttk.Scrollbar(frame, orient='vertical', command=self.tree_categorias.yview)
        self.tree_categorias.configure(yscrollcommand=scrollbar.set)
        scrollbar.pack(side='right', fill='y')

        # Datos por defecto
        categorias_default = [
            ("CUIDADO PERSONAL", "10399", "6753", "65"),
            ("ELECTRO MENOR", "28263", "20111", "71"),
            ("FREEZERS", "10748", "8544", "79"),
            ("AIRE ACONDICIONADO", "33085", "27246", "82"),
            ("CLIMATIZACION", "4257", "3565", "84")
        ]

        for cat in categorias_default:
            self.tree_categorias.insert('', 'end', values=cat)

    def crear_pestaña_skus(self):
        """Crea la pestaña de Top SKUs"""
        frame = ttk.Frame(self.notebook)
        self.notebook.add(frame, text='Top 10 SKUs')

        tk.Label(frame, text="TOP 10 SKUS DEL MES",
                font=('Arial', 14, 'bold'), bg='white').pack(pady=10)

        # Frame para botones
        btn_frame = tk.Frame(frame, bg='white')
        btn_frame.pack(pady=5)

        tk.Button(btn_frame, text="Agregar SKU",
                 command=self.agregar_sku,
                 bg='#2C5F5D', fg='white', font=('Arial', 10)).pack(side='left', padx=5)

        tk.Button(btn_frame, text="Eliminar Seleccionado",
                 command=self.eliminar_sku,
                 bg='#E31E24', fg='white', font=('Arial', 10)).pack(side='left', padx=5)

        # Treeview para SKUs
        columns = ('SKU', 'Cantidad')
        self.tree_skus = ttk.Treeview(frame, columns=columns, show='headings', height=15)

        for col in columns:
            self.tree_skus.heading(col, text=col)
            self.tree_skus.column(col, width=200)

        self.tree_skus.pack(fill='both', expand=True, padx=10, pady=10)

        # Scrollbar
        scrollbar = ttk.Scrollbar(frame, orient='vertical', command=self.tree_skus.yview)
        self.tree_skus.configure(yscrollcommand=scrollbar.set)
        scrollbar.pack(side='right', fill='y')

        # Datos por defecto
        skus_default = [
            ("EXCE610003", "10"),
            ("EV22403", "8"),
            ("FLE-DISCO-37161", "8"),
            ("BE548", "8"),
            ("PP8857", "7"),
            ("VEF773862", "7"),
            ("MUGE02108", "7"),
            ("K09L4152", "7"),
            ("SAP01040", "7"),
            ("SAM-SSD-MZ77Q500", "7")
        ]

        for sku in skus_default:
            self.tree_skus.insert('', 'end', values=sku)

    def crear_pestaña_mystery(self):
        """Crea la pestaña de Mystery Shopper"""
        frame = ttk.Frame(self.notebook)
        self.notebook.add(frame, text='Mystery Shopper')

        # Canvas y scrollbar
        canvas = tk.Canvas(frame, bg='white')
        scrollbar = ttk.Scrollbar(frame, orient="vertical", command=canvas.yview)
        scrollable_frame = ttk.Frame(canvas)

        scrollable_frame.bind(
            "<Configure>",
            lambda e: canvas.configure(scrollregion=canvas.bbox("all"))
        )

        canvas.create_window((0, 0), window=scrollable_frame, anchor="nw")
        canvas.configure(yscrollcommand=scrollbar.set)

        tk.Label(scrollable_frame, text="MYSTERY SHOPPER",
                font=('Arial', 14, 'bold'), bg='white', fg='#2C5F5D').grid(
                row=0, column=0, columnspan=2, pady=20)

        row = 1

        tk.Label(scrollable_frame, text="Índice General (%):", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.mystery_indice_var = tk.StringVar(value="93")
        tk.Entry(scrollable_frame, textvariable=self.mystery_indice_var, width=32,
                font=('Arial', 11)).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        tk.Label(scrollable_frame, text="Atención del Vendedor (%):", font=('Arial', 12), bg='white').grid(
            row=row, column=0, sticky='e', padx=10, pady=5)
        self.mystery_atencion_var = tk.StringVar(value="83")
        tk.Entry(scrollable_frame, textvariable=self.mystery_atencion_var, width=32,
                font=('Arial', 11)).grid(row=row, column=1, padx=10, pady=5)
        row += 1

        ttk.Separator(scrollable_frame, orient='horizontal').grid(
            row=row, column=0, columnspan=2, sticky='ew', pady=15)
        row += 1

        tk.Label(scrollable_frame, text="PUNTOS A MEJORAR",
                font=('Arial', 12, 'bold'), bg='white', fg='#2C5F5D').grid(
                row=row, column=0, columnspan=2, pady=10)
        row += 1

        # Puntos a mejorar
        self.mystery_puntos = []
        puntos_default = [
            ("Comunicar precios bajos y garantía", "50"),
            ("Explicar garantía extendida", "50"),
            ("Ofrecer servicio de entrega gratuita", "0"),
            ("Ofrecer servicio de instalación", "0"),
            ("Explicar política de cambios", "50")
        ]

        for i, (desc, pct) in enumerate(puntos_default):
            tk.Label(scrollable_frame, text=f"Punto {i+1}:", font=('Arial', 11), bg='white').grid(
                row=row, column=0, sticky='e', padx=10, pady=5)
            desc_var = tk.StringVar(value=desc)
            pct_var = tk.StringVar(value=pct)

            entry_desc = tk.Entry(scrollable_frame, textvariable=desc_var, width=25,
                                 font=('Arial', 10))
            entry_desc.grid(row=row, column=1, padx=(10, 5), pady=5, sticky='w')

            tk.Label(scrollable_frame, text="%:", font=('Arial', 11), bg='white').grid(
                row=row, column=1, padx=(250, 5), pady=5, sticky='w')
            entry_pct = tk.Entry(scrollable_frame, textvariable=pct_var, width=8,
                                font=('Arial', 10))
            entry_pct.grid(row=row, column=1, padx=(280, 10), pady=5, sticky='w')

            self.mystery_puntos.append({'descripcion': desc_var, 'porcentaje': pct_var})
            row += 1

        canvas.pack(side="left", fill="both", expand=True)
        scrollbar.pack(side="right", fill="y")

    def agregar_categoria(self):
        """Abre ventana para agregar nueva categoría"""
        ventana = tk.Toplevel(self.root)
        ventana.title("Agregar Categoría")
        ventana.geometry("400x250")
        ventana.configure(bg='white')

        tk.Label(ventana, text="Nombre:", font=('Arial', 11), bg='white').grid(
            row=0, column=0, sticky='e', padx=10, pady=10)
        nombre_var = tk.StringVar()
        tk.Entry(ventana, textvariable=nombre_var, width=25, font=('Arial', 11)).grid(
            row=0, column=1, padx=10, pady=10)

        tk.Label(ventana, text="Objetivo:", font=('Arial', 11), bg='white').grid(
            row=1, column=0, sticky='e', padx=10, pady=10)
        objetivo_var = tk.StringVar()
        tk.Entry(ventana, textvariable=objetivo_var, width=25, font=('Arial', 11)).grid(
            row=1, column=1, padx=10, pady=10)

        tk.Label(ventana, text="Ventas:", font=('Arial', 11), bg='white').grid(
            row=2, column=0, sticky='e', padx=10, pady=10)
        ventas_var = tk.StringVar()
        tk.Entry(ventana, textvariable=ventas_var, width=25, font=('Arial', 11)).grid(
            row=2, column=1, padx=10, pady=10)

        tk.Label(ventana, text="Alcance (%):", font=('Arial', 11), bg='white').grid(
            row=3, column=0, sticky='e', padx=10, pady=10)
        alcance_var = tk.StringVar()
        tk.Entry(ventana, textvariable=alcance_var, width=25, font=('Arial', 11)).grid(
            row=3, column=1, padx=10, pady=10)

        def guardar():
            if nombre_var.get() and objetivo_var.get() and ventas_var.get() and alcance_var.get():
                self.tree_categorias.insert('', 'end', values=(
                    nombre_var.get(),
                    objetivo_var.get(),
                    ventas_var.get(),
                    alcance_var.get()
                ))
                ventana.destroy()
            else:
                messagebox.showwarning("Campos incompletos", "Por favor complete todos los campos")

        tk.Button(ventana, text="Guardar", command=guardar,
                 bg='#2C5F5D', fg='white', font=('Arial', 11)).grid(
                 row=4, column=0, columnspan=2, pady=15)

    def eliminar_categoria(self):
        """Elimina la categoría seleccionada"""
        selected = self.tree_categorias.selection()
        if selected:
            self.tree_categorias.delete(selected)
        else:
            messagebox.showwarning("Sin selección", "Por favor seleccione una categoría para eliminar")

    def agregar_sku(self):
        """Abre ventana para agregar nuevo SKU"""
        ventana = tk.Toplevel(self.root)
        ventana.title("Agregar SKU")
        ventana.geometry("350x150")
        ventana.configure(bg='white')

        tk.Label(ventana, text="SKU:", font=('Arial', 11), bg='white').grid(
            row=0, column=0, sticky='e', padx=10, pady=10)
        sku_var = tk.StringVar()
        tk.Entry(ventana, textvariable=sku_var, width=25, font=('Arial', 11)).grid(
            row=0, column=1, padx=10, pady=10)

        tk.Label(ventana, text="Cantidad:", font=('Arial', 11), bg='white').grid(
            row=1, column=0, sticky='e', padx=10, pady=10)
        cantidad_var = tk.StringVar()
        tk.Entry(ventana, textvariable=cantidad_var, width=25, font=('Arial', 11)).grid(
            row=1, column=1, padx=10, pady=10)

        def guardar():
            if sku_var.get() and cantidad_var.get():
                self.tree_skus.insert('', 'end', values=(sku_var.get(), cantidad_var.get()))
                ventana.destroy()
            else:
                messagebox.showwarning("Campos incompletos", "Por favor complete todos los campos")

        tk.Button(ventana, text="Guardar", command=guardar,
                 bg='#2C5F5D', fg='white', font=('Arial', 11)).grid(
                 row=2, column=0, columnspan=2, pady=15)

    def eliminar_sku(self):
        """Elimina el SKU seleccionado"""
        selected = self.tree_skus.selection()
        if selected:
            self.tree_skus.delete(selected)
        else:
            messagebox.showwarning("Sin selección", "Por favor seleccione un SKU para eliminar")

    def _convertir_a_numero(self, valor, tipo='int'):
        """Convierte un valor a número de forma segura, manejando strings y números"""
        try:
            # Si ya es un número, devolverlo
            if isinstance(valor, (int, float)):
                return int(valor) if tipo == 'int' else float(valor)

            # Si es string, limpiar y convertir
            valor_limpio = str(valor).replace(',', '').strip()

            if tipo == 'int':
                return int(float(valor_limpio))
            else:
                return float(valor_limpio)
        except:
            return 0

    def obtener_datos(self):
        """Recopila todos los datos del formulario"""
        # Datos generales
        try:
            objetivo_usd = self._convertir_a_numero(self.objetivo_usd_var.get(), 'int')
            objetivo_bs = self._convertir_a_numero(self.objetivo_bs_var.get(), 'int')
            ventas_usd = self._convertir_a_numero(self.ventas_usd_var.get(), 'int')
            ventas_bs = self._convertir_a_numero(self.ventas_bs_var.get(), 'float')
            alcance = int((ventas_usd / objetivo_usd) * 100) if objetivo_usd > 0 else 0

            self.datos_generales = {
                'mes': self.mes_var.get(),
                'anio': self.anio_var.get(),
                'tienda': self.tienda_var.get(),
                'objetivo_usd': objetivo_usd,
                'objetivo_bs': objetivo_bs,
                'ventas_usd': ventas_usd,
                'ventas_bs': ventas_bs,
                'alcance': alcance,
                'ticket_usd': self._convertir_a_numero(self.ticket_usd_var.get(), 'int'),
                'ticket_bs': self._convertir_a_numero(self.ticket_bs_var.get(), 'float'),
                'contado_pct': self._convertir_a_numero(self.contado_pct_var.get(), 'float'),
                'minicuotas_pct': self._convertir_a_numero(self.minicuotas_pct_var.get(), 'float')
            }

            # Categorías
            self.categorias = []
            for item in self.tree_categorias.get_children():
                valores = self.tree_categorias.item(item)['values']
                self.categorias.append({
                    'nombre': str(valores[0]),
                    'objetivo': self._convertir_a_numero(valores[1], 'int'),
                    'ventas': self._convertir_a_numero(valores[2], 'int'),
                    'alcance': self._convertir_a_numero(valores[3], 'int')
                })

            # SKUs
            self.skus = []
            for item in self.tree_skus.get_children():
                valores = self.tree_skus.item(item)['values']
                self.skus.append({
                    'nombre': str(valores[0]),
                    'cantidad': self._convertir_a_numero(valores[1], 'int')
                })

            # Mystery Shopper
            self.mystery_shopper = {
                'indice_general': self._convertir_a_numero(self.mystery_indice_var.get(), 'int'),
                'atencion_vendedor': self._convertir_a_numero(self.mystery_atencion_var.get(), 'int'),
                'puntos_mejorar': [
                    {
                        'descripcion': punto['descripcion'].get(),
                        'porcentaje': self._convertir_a_numero(punto['porcentaje'].get(), 'int')
                    }
                    for punto in self.mystery_puntos if punto['descripcion'].get()
                ]
            }

            return True

        except Exception as e:
            messagebox.showerror("Error en datos", f"Error al procesar los datos: {str(e)}")
            return False

    def generar_reporte(self):
        """Genera el reporte en PowerPoint"""
        if not self.obtener_datos():
            return

        # Importar módulos de generación
        try:
            from generador_principal import generar_reporte_completo

            # Elegir ubicación de guardado
            archivo_salida = filedialog.asksaveasfilename(
                defaultextension=".pptx",
                filetypes=[("PowerPoint", "*.pptx")],
                initialfile=f"Cierre_Mes_{self.datos_generales['mes']}_{self.datos_generales['anio']}.pptx"
            )

            if archivo_salida:
                # Generar reporte
                generar_reporte_completo(
                    self.datos_generales,
                    self.categorias,
                    self.skus,
                    self.mystery_shopper,
                    archivo_salida
                )

                messagebox.showinfo("Éxito", f"Reporte generado exitosamente en:\n{archivo_salida}")

        except Exception as e:
            messagebox.showerror("Error", f"Error al generar el reporte:\n{str(e)}")


def iniciar_aplicacion():
    root = tk.Tk()
    app = ReporteGUI(root)
    root.mainloop()


if __name__ == "__main__":
    iniciar_aplicacion()
