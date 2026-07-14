using System;
using System.Drawing;

namespace SistemaZoofiPets
{
    /// <summary>
    /// Sistema de colores pastel violeta con detalles negro/blanco para ZoofiPets
    /// Paleta elegante y profesional para clínica veterinaria
    /// </summary>
    public static class ColoresPastelGlobales
    {
        // ═══════════════════════════════════════════════════════════════════
        // PALETA PRINCIPAL - VIOLETAS PASTEL
        // ═══════════════════════════════════════════════════════════════════
        
        /// <summary>
        /// Violeta muy claro - Fondo principal de la aplicación
        /// </summary>
        public static readonly Color VioletaMuyClaro = Color.FromArgb(248, 245, 255);
        
        /// <summary>
        /// Violeta claro - Paneles secundarios y áreas de contenido
        /// </summary>
        public static readonly Color VioletaClaro = Color.FromArgb(235, 225, 255);
        
        /// <summary>
        /// Violeta medio - Botones principales y elementos destacados
        /// </summary>
        public static readonly Color VioletaMedio = Color.FromArgb(204, 187, 255);
        
        /// <summary>
        /// Violeta oscuro - Barras de título y navegación
        /// </summary>
        public static readonly Color VioletaOscuro = Color.FromArgb(147, 112, 219);
        
        /// <summary>
        /// Violeta profundo - Elementos de acento y hover
        /// </summary>
        public static readonly Color VioletaProfundo = Color.FromArgb(123, 104, 238);

        // ═══════════════════════════════════════════════════════════════════
        // COLORES DE CONTRASTE - NEGRO Y BLANCO
        // ═══════════════════════════════════════════════════════════════════
        
        /// <summary>
        /// Negro suave - Bordes y líneas de separación
        /// </summary>
        public static readonly Color NegroSuave = Color.FromArgb(45, 45, 45);
        
        /// <summary>
        /// Negro carbón - Textos principales y elementos importantes
        /// </summary>
        public static readonly Color NegroCarbon = Color.FromArgb(33, 33, 33);
        
        /// <summary>
        /// Blanco puro - Fondos de contraste y textos en fondos oscuros
        /// </summary>
        public static readonly Color BlancoPuro = Color.White;
        
        /// <summary>
        /// Blanco crema - Fondos suaves y alternativas al blanco puro
        /// </summary>
        public static readonly Color BlancoCrema = Color.FromArgb(254, 254, 250);

        // ═══════════════════════════════════════════════════════════════════
        // COLORES COMPLEMENTARIOS PARA ESTADOS
        // ═══════════════════════════════════════════════════════════════════
        
        /// <summary>
        /// Verde suave - Estados exitosos y confirmaciones
        /// </summary>
        public static readonly Color VerdeSuave = Color.FromArgb(200, 230, 201);
        
        /// <summary>
        /// Naranja suave - Advertencias y notificaciones
        /// </summary>
        public static readonly Color NaranjaSuave = Color.FromArgb(255, 224, 178);
        
        /// <summary>
        /// Rojo suave - Errores y elementos críticos
        /// </summary>
        public static readonly Color RojoSuave = Color.FromArgb(255, 205, 210);
        
        /// <summary>
        /// Azul suave - Información y elementos informativos
        /// </summary>
        public static readonly Color AzulSuave = Color.FromArgb(187, 222, 251);

        // ═══════════════════════════════════════════════════════════════════
        // SISTEMA DE GRISES CON TINTE VIOLETA
        // ═══════════════════════════════════════════════════════════════════
        
        /// <summary>
        /// Gris muy claro con tinte violeta - Fondos sutiles
        /// </summary>
        public static readonly Color GrisVioletaMuyClaro = Color.FromArgb(245, 242, 248);
        
        /// <summary>
        /// Gris claro con tinte violeta - Separadores y líneas
        /// </summary>
        public static readonly Color GrisVioletaClaro = Color.FromArgb(220, 215, 225);
        
        /// <summary>
        /// Gris medio con tinte violeta - Textos secundarios
        /// </summary>
        public static readonly Color GrisVioletaMedio = Color.FromArgb(150, 145, 160);
        
        /// <summary>
        /// Gris oscuro con tinte violeta - Textos de apoyo
        /// </summary>
        public static readonly Color GrisVioletaOscuro = Color.FromArgb(100, 95, 110);

        // ═══════════════════════════════════════════════════════════════════
        // MÉTODOS AUXILIARES PARA MANIPULACIÓN DE COLORES
        // ═══════════════════════════════════════════════════════════════════

        /// <summary>
        /// Obtiene un color más claro basado en el color base
        /// </summary>
        /// <param name="baseColor">Color base</param>
        /// <param name="factor">Factor de claridad (0.0 a 1.0)</param>
        /// <returns>Color más claro</returns>
        public static Color ObtenerColorMasClaro(Color baseColor, float factor = 0.3f)
        {
            int r = (int)(baseColor.R + (255 - baseColor.R) * factor);
            int g = (int)(baseColor.G + (255 - baseColor.G) * factor);
            int b = (int)(baseColor.B + (255 - baseColor.B) * factor);
            
            return Color.FromArgb(
                Math.Min(255, r),
                Math.Min(255, g),
                Math.Min(255, b)
            );
        }

        /// <summary>
        /// Obtiene un color más oscuro basado en el color base
        /// </summary>
        /// <param name="baseColor">Color base</param>
        /// <param name="factor">Factor de oscuridad (0.0 a 1.0)</param>
        /// <returns>Color más oscuro</returns>
        public static Color ObtenerColorMasOscuro(Color baseColor, float factor = 0.3f)
        {
            int r = (int)(baseColor.R * (1 - factor));
            int g = (int)(baseColor.G * (1 - factor));
            int b = (int)(baseColor.B * (1 - factor));
            
            return Color.FromArgb(
                Math.Max(0, r),
                Math.Max(0, g),
                Math.Max(0, b)
            );
        }

        /// <summary>
        /// Agrega borde negro suave a un color
        /// </summary>
        public static Color ConBordeNegro(Color baseColor) => NegroSuave;

        /// <summary>
        /// Agrega borde blanco a un color
        /// </summary>
        public static Color ConBordeBlanco(Color baseColor) => BlancoPuro;

        // ═══════════════════════════════════════════════════════════════════
        // PALETAS ESPECÍFICAS POR CONTEXTO
        // ═══════════════════════════════════════════════════════════════════

        /// <summary>
        /// Paleta específica para el Dashboard principal
        /// </summary>
        public static class Dashboard
        {
            public static readonly Color FondoPrincipal = VioletaMuyClaro;
            public static readonly Color PanelAnimales = VioletaClaro;
            public static readonly Color PanelCitas = VioletaMedio;
            public static readonly Color PanelVentas = VerdeSuave;
            public static readonly Color PanelInventario = NaranjaSuave;
            public static readonly Color TextoPrincipal = NegroCarbon;
            public static readonly Color TextoSecundario = GrisVioletaOscuro;
            public static readonly Color Bordes = NegroSuave;
        }

        /// <summary>
        /// Paleta para formularios de gestión de datos
        /// </summary>
        public static class Formularios
        {
            public static readonly Color FondoPrincipal = VioletaMuyClaro;
            public static readonly Color PanelCabecera = VioletaOscuro;
            public static readonly Color PanelContenido = BlancoCrema;
            public static readonly Color TextoTitulo = BlancoPuro;
            public static readonly Color TextoNormal = NegroCarbon;
            public static readonly Color BotonPrimario = VioletaProfundo;
            public static readonly Color BotonSecundario = VioletaMedio;
            public static readonly Color BordesControles = NegroSuave;
            public static readonly Color FondoInputs = BlancoPuro;
        }

        /// <summary>
        /// Paleta para la navegación principal (VentanaPrincipalModerna)
        /// </summary>
        public static class Navegacion
        {
            public static readonly Color MenuLateral = VioletaOscuro;
            public static readonly Color BarraTitulo = VioletaProfundo;
            public static readonly Color AreaPrincipal = VioletaMuyClaro;
            public static readonly Color BotonActivo = VioletaMedio;
            public static readonly Color BotonInactivo = VioletaOscuro;
            public static readonly Color TextoBoton = BlancoPuro;
            public static readonly Color TextoTitulo = BlancoPuro;
            public static readonly Color BordeActivo = BlancoPuro;
            public static readonly Color IconosActivos = BlancoPuro;
        }

        /// <summary>
        /// Paleta para formularios de login y autenticación
        /// </summary>
        public static class Login
        {
            public static readonly Color FondoPrincipal = VioletaMuyClaro;
            public static readonly Color PanelLogin = BlancoCrema;
            public static readonly Color TextoTitulo = VioletaProfundo;
            public static readonly Color TextoSubtitulo = VioletaOscuro;
            public static readonly Color CamposInput = BlancoPuro;
            public static readonly Color BordesCampos = GrisVioletaClaro;
            public static readonly Color BotonIngresar = VioletaProfundo;
            public static readonly Color BotonSalir = GrisVioletaMedio;
            public static readonly Color TextoBotones = BlancoPuro;
            public static readonly Color EtiquetasCampos = VioletaOscuro;
        }
    }
}