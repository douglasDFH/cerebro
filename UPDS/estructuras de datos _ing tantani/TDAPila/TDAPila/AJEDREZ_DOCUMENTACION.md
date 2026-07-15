# JUEGO DE AJEDREZ COMPLETO - DOCUMENTACIÓN

## Estructura TDA (Tipo de Dato Abstracto)

El juego de ajedrez ha sido implementado siguiendo principios educativos de TDA, con una arquitectura modular y bien organizada.

---

## CLASES CREADAS

### 1. **clsPieza** (Clase Abstracta Base)
- Clase padre de todas las piezas
- Atributos: color, tipo, fila, columna, seHaMovido
- Método abstracto: `esMovimientoValido()`
- Método protegido: `caminoLibre()` para piezas de movimiento lineal

### 2. **Clases de Piezas Específicas**

#### **clsRey**
- Movimiento: 1 casilla en cualquier dirección
- Reglas especiales: Enroque (corto y largo)
- Verificación: No puede moverse a casilla atacada

#### **clsReina**
- Movimiento: Combinación de Torre + Alfil
- Líneas rectas (horizontal, vertical, diagonal)
- Alcance ilimitado si el camino está libre

#### **clsTorre**
- Movimiento: Horizontal y vertical
- Alcance ilimitado si el camino está libre
- Participación en enroque

#### **clsAlfil**
- Movimiento: Solo diagonal
- Alcance ilimitado si el camino está libre
- Cada alfil permanece en su color inicial

#### **clsCaballo**
- Movimiento: Forma de "L" (2+1 casillas)
- Única pieza que puede saltar sobre otras
- No requiere camino libre

#### **clsPeon**
- Movimiento: 1 casilla hacia adelante (2 desde posición inicial)
- Captura: En diagonal
- Reglas especiales:
  - Promoción al llegar a la última fila
  - Captura al paso (en passant) - base implementada

### 3. **clsCasilla**
- Representa cada casilla del tablero 8x8
- Atributos: fila, columna, color (patrón ajedrez), pieza
- Método: `getNotacionAlgebraica()` (ej: "e4", "d5")

### 4. **clsTablero**
- Maneja la matriz 8x8 de piezas
- Inicialización automática en posición estándar
- Métodos principales:
  - `moverPieza()`: Ejecuta movimientos
  - `estEnJaque()`: Detecta jaque
  - `esJaqueMate()`: Detecta jaque mate
  - `esAhogado()`: Detecta empate por ahogado
  - `promoverPeon()`: Promoción de peones
  - `buscarRey()`: Localiza el rey
  - `estaAtacada()`: Verifica si una casilla está amenazada

### 5. **clsJugador**
- Representa cada jugador (Blanco/Negro)
- Atributos: nombre, color, piezasCapturadas, estados (jaque, jaqueMate, ahogado)
- Métodos:
  - `capturarPieza()`: Registra capturas
  - `getPuntuacion()`: Calcula valor de piezas capturadas

### 6. **clsMovimiento**
- Registra cada movimiento de la partida
- Atributos: origen, destino, pieza, piezaCapturada, flags especiales
- Método: `getNotacionAlgebraica()`: Genera notación estándar

### 7. **clsPartidaAjedrez** (Clase Principal)
- Orquesta toda la lógica del juego
- Maneja turnos, validaciones, estados
- Métodos principales:
  - `realizarMovimiento()`: Procesa y valida movimientos
  - `getMovimientosPosibles()`: Calcula movimientos legales
  - `verificarEstadoJuego()`: Actualiza estado (jaque, mate, ahogado)
  - `reiniciarPartida()`: Reset completo
  - `getNotacionAlgebraica()`: Exporta partida
  - `deshacerMovimiento()`: Retrocede jugadas

---

## REGLAS IMPLEMENTADAS COMPLETAS

### ✅ Reglas Básicas
- [x] Movimiento correcto de todas las piezas
- [x] Captura de piezas enemigas
- [x] Turnos alternados (Blancas empiezan)
- [x] No mover piezas del oponente
- [x] No capturar piezas propias

### ✅ Reglas Avanzadas
- [x] **Jaque**: Detección cuando el rey está amenazado
- [x] **Jaque Mate**: Partida termina cuando no hay escape del jaque
- [x] **Ahogado (Stalemate)**: Empate cuando no hay movimientos legales sin estar en jaque
- [x] **Enroque**:
  - Corto (O-O): Rey + Torre derecha
  - Largo (O-O-O): Rey + Torre izquierda
  - Condiciones: Ni rey ni torre se han movido, camino libre, rey no en jaque
- [x] **Promoción de Peón**: Al llegar a última fila (por defecto: Reina)
- [x] **Validación de movimientos**: No se puede dejar al propio rey en jaque

### ✅ Sistema de Puntuación
- Peón: 1 punto
- Caballo/Alfil: 3 puntos
- Torre: 5 puntos
- Reina: 9 puntos
- Rey: Invaluable

### ✅ Historial y Notación
- Registro completo de movimientos
- Notación algebraica estándar
- Posibilidad de deshacer movimientos

---

## OPTIMIZACIONES IMPLEMENTADAS

### 1. **Validación en Cascada**
```
Usuario selecciona pieza → Validar que sea su turno
                         → Validar movimiento según tipo de pieza
                         → Simular movimiento
                         → Validar que no deja rey en jaque
                         → Ejecutar movimiento
```

### 2. **Simulación de Movimientos**
- Los movimientos se simulan antes de ejecutarse
- Permite detectar jaque antes de confirmar
- Evita estados ilegales del tablero

### 3. **Búsqueda Optimizada**
- Método `buscarRey()` para localización rápida
- Cache de posiciones críticas
- Validación early-exit en bucles

### 4. **Arquitectura Modular**
- Separación de responsabilidades (SRP)
- Cada clase tiene una función específica
- Fácil mantenimiento y extensión

### 5. **Método `caminoLibre()`**
- Algoritmo genérico para piezas lineales
- Usa `Integer.signum()` para calcular dirección
- Evita código duplicado

---

## ESTRUCTURA DE ARCHIVOS

```
negocio/
├── clsPieza.java           (Clase abstracta base)
├── clsRey.java            (Pieza: Rey)
├── clsReina.java          (Pieza: Reina)
├── clsTorre.java          (Pieza: Torre)
├── clsAlfil.java          (Pieza: Alfil)
├── clsCaballo.java        (Pieza: Caballo)
├── clsPeon.java           (Pieza: Peón)
├── clsCasilla.java        (Representa casilla del tablero)
├── clsTablero.java        (Tablero 8x8 y lógica de juego)
├── clsJugador.java        (Datos y estado del jugador)
├── clsMovimiento.java     (Registro de movimientos)
└── clsPartidaAjedrez.java (Lógica principal del juego)
```

---

## CÓMO USAR LA LÓGICA (Para la Interfaz)

### 1. Iniciar Partida
```java
clsPartidaAjedrez partida = new clsPartidaAjedrez("Jugador 1", "Jugador 2");
```

### 2. Obtener Tablero
```java
clsTablero tablero = partida.getTablero();
clsPieza[][] matriz = tablero.getTablero();
```

### 3. Realizar Movimiento
```java
// Mover de e2 a e4 (notación: fila 6, col 4 → fila 4, col 4)
boolean exito = partida.realizarMovimiento(6, 4, 4, 4);
```

### 4. Obtener Movimientos Posibles (Para Resaltar)
```java
ArrayList<int[]> movimientos = partida.getMovimientosPosibles(fila, columna);
// Cada int[] tiene {fila, columna} de destino válido
```

### 5. Verificar Estado
```java
String estado = partida.getEstadoPartida();
// "Turno de Jugador 1 (BLANCO)"
// "Turno de Jugador 2 (EN JAQUE)"
// "Partida terminada: Ganan las BLANCO"
```

### 6. Obtener Jugador Actual
```java
clsJugador actual = partida.getJugadorActual();
String color = actual.getColor();
boolean enJaque = actual.isEnJaque();
```

### 7. Promover Peón (Si es necesario)
```java
// Después de mover un peón a la última fila
tablero.promoverPeon(fila, columna, "REINA");
// Opciones: "REINA", "TORRE", "ALFIL", "CABALLO"
```

### 8. Reiniciar Partida
```java
partida.reiniciarPartida();
```

### 9. Obtener Símbolo de Pieza (Para UI)
```java
clsPieza pieza = tablero.getPieza(fila, columna);
if (pieza != null) {
    String simbolo = pieza.getSimbolo();
    // Retorna: ♔♕♖♗♘♙ (blancas) o ♚♛♜♝♞♟ (negras)
}
```

---

## CONVERSIÓN DE COORDENADAS

### Notación Algebraica → Índices de Matriz
```
a8 b8 c8 d8 e8 f8 g8 h8    [0][0] [0][1] [0][2] ... [0][7]
a7 b7 c7 d7 e7 f7 g7 h7    [1][0] [1][1] [1][2] ... [1][7]
...                        ...
a1 b1 c1 d1 e1 f1 g1 h1    [7][0] [7][1] [7][2] ... [7][7]

Fórmula:
- fila = 8 - número
- columna = letra - 'a'

Ejemplo: "e4"
- fila = 8 - 4 = 4
- columna = 'e' - 'a' = 4
```

---

## PRÓXIMAS MEJORAS OPCIONALES

1. **Captura al paso (En Passant)** - Implementación completa
2. **Reloj de ajedrez** - Tiempo por jugador
3. **Regla de 50 movimientos** - Empate automático
4. **Repetición triple** - Empate por repetición
5. **Guardar/Cargar partida** - Persistencia en archivo
6. **IA básica** - Oponente automático
7. **Análisis de posición** - Evaluación de ventaja
8. **Modo multijugador en red** - Cliente-servidor

---

## PRINCIPIOS TDA APLICADOS

1. **Encapsulación**: Atributos privados, acceso por getters/setters
2. **Abstracción**: Clase base abstracta `clsPieza`
3. **Herencia**: Todas las piezas heredan de `clsPieza`
4. **Polimorfismo**: Método `esMovimientoValido()` sobrescrito por cada pieza
5. **Modularidad**: Cada clase tiene responsabilidad única
6. **Reutilización**: Método `caminoLibre()` compartido

---

## TESTING RECOMENDADO

Para probar la lógica sin interfaz:
```java
public static void main(String[] args) {
    clsPartidaAjedrez partida = new clsPartidaAjedrez("Blancas", "Negras");

    // Imprimir tablero inicial
    partida.getTablero().imprimirTablero();

    // Movimiento: e4
    partida.realizarMovimiento(6, 4, 4, 4);

    // Movimiento: e5
    partida.realizarMovimiento(1, 4, 3, 4);

    // Imprimir tablero actualizado
    partida.getTablero().imprimirTablero();

    // Ver estado
    System.out.println(partida.getEstadoPartida());
}
```

---

**¡La lógica del juego está completa y lista para conectar con la interfaz gráfica!**
