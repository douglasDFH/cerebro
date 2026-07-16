---
tags:
  - documentacion
  - guia
creado: 2026-07-16
---

# 📘 Cómo se construyó el Cerebro — Paso a Paso

> Documentación completa del proceso, desde cero hasta el cerebro actual: organizado, conectado, sincronizado en 2 computadoras, con chat y grafo 2D/3D.

## 🧭 Índice
1. [[#1 Punto de partida]]
2. [[#2 Conectar Obsidian con Claude Code]]
3. [[#3 Estructura inicial]]
4. [[#4 Llenar el cerebro con archivos de la PC]]
5. [[#5 Git como respaldo]]
6. [[#6 Sincronización con GitHub y la 2da compu]]
7. [[#7 Automatización auto-guardado]]
8. [[#8 Conexión tipo red neuronal índices y MOCs]]
9. [[#9 Automatizar la conexión git hooks]]
10. [[#10 Chatear con el cerebro MCP]]
11. [[#11 El grafo bonito Extended Graph]]
12. [[#12 Nueva arquitectura a mi medida]]
13. [[#13 Limpiar el código que inflaba]]
14. [[#14 Grafo 3D y modo oscuro]]
15. [[#Estado final]]

---

## 1. Punto de partida
- Tenía **Obsidian instalado** y quería usarlo como un **cerebro digital** (un solo lugar donde todo se conecta).
- Vault (carpeta del cerebro): `C:\Users\SAMSUNG\CEREBRO`

## 2. Conectar Obsidian con Claude Code
- **Idea clave:** un vault de Obsidian es solo una carpeta con archivos `.md`. Claude Code trabaja sobre esa carpeta → ya están "conectados", sin plugins.
- Se abrió el vault como carpeta de proyecto en Claude Code. Desde ahí Claude puede leer, crear, mover, buscar y enlazar notas.

## 3. Estructura inicial
- Se empezó con el método **PARA** (Inbox, Diario, Proyectos, Áreas, Recursos, Archivo, Plantillas).
- *(Más adelante se reemplazó por una estructura a mi medida — ver paso 12.)*

## 4. Llenar el cerebro con archivos de la PC
- Claude revisó Descargas/Escritorio y **clasificó** el material leyendo dentro de los archivos.
- **Dismac** (trabajo): reportes encubierto, despachos, compras, inventarios, presentaciones.
- **UPDS** (universidad): materias (Emprendedurismo/NutriCiclo, Investigación Operativa, IA, Móviles…).
- Los archivos se **movieron** (no copiaron) al vault. Se detectó que la música de tienda (~380 MB) inflaba todo → se excluyó del sync.

## 5. Git como respaldo
- Se activó control de versiones (**git**) en el vault: una "máquina del tiempo" que registra cada cambio.
```bash
git init
git add -A
git commit -m "Estructura inicial del cerebro"
```
- Se creó `.gitignore` para excluir estado local de Obsidian y archivos pesados.
- **Decisión:** los commits van **sin la firma de Anthropic**.

## 6. Sincronización con GitHub y la 2da compu
- Se creó un repositorio **privado** en GitHub: `https://github.com/douglasDFH/cerebro`
- Se subió el cerebro:
```bash
git remote add origin https://github.com/douglasDFH/cerebro.git
git push -u origin master
```
- En la **2da compu (Dell)** se clonó: `git clone ...`
- **Regla de oro con 2 compus:** `git pull` al empezar, `git push` al terminar.
- **Windows:** se activó `git config core.longpaths true` (rutas largas).

## 7. Automatización (auto-guardado)
- **Hook de Claude Code** (`.claude/settings.json`, evento `Stop`): cada vez que Claude termina, hace `git add + commit + push` automático si hay cambios.
- **Plugin Git de Obsidian** (escritorio): auto-guardado cada 10 min + auto-pull al abrir.
- *(En el celular el plugin Git NO funcionó → se deja para Obsidian Sync más adelante.)*

## 8. Conexión tipo red neuronal (índices y MOCs)
- **Problema:** los archivos adjuntos (PDF, Excel…) salían "sueltos" en el grafo, sin conexión.
- **Solución:** notas **"📇 Índice"** (una por materia) que enlazan todos sus archivos, y notas **MOC** (mapas) por cada área. Así:
  > archivo → índice de materia → MOC de área → 🏠 Inicio
- Los proyectos de código se tratan como una unidad (no se expanden).

## 9. Automatizar la conexión (git hooks)
- El "conectar archivos" es un script fijo (no necesita IA) → se metió en **git hooks**:
  - `.githooks/actualizar_indices.py` (generador portable de índices)
  - `.githooks/pre-commit` (regenera índices + `git add` en cada commit)
  - `.githooks/post-merge` (reconecta tras cada `git pull`)
```bash
git config core.hooksPath .githooks
```
- **Resultado:** en cada commit/pull, en cualquier compu, todo se reconecta solo.
- **Parte inteligente (bajo pedido):** archivos nuevos sin ubicar → carpeta **Diario** → digo *"organiza"* y Claude los lee, clasifica y conecta.

## 10. Chatear con el cerebro (MCP)
- Plugin de Obsidian **"Local REST API with MCP"** (Adam Coddington) → expone un servidor MCP en `http://127.0.0.1:27123/mcp`.
- Conectado a **Claude Code**:
```bash
claude mcp add obsidian --transport http --scope user "http://127.0.0.1:27123/mcp" --header "Authorization: Bearer <API_KEY>"
```
- Conectado a la **app de escritorio de Claude** (`claude_desktop_config.json`) con el puente `npx mcp-remote`.
- Da funciones nativas: buscar, nota abierta, abrir en la UI, tags, ejecutar comandos de Obsidian.
- **Requiere Obsidian abierto.**

## 11. El grafo bonito (Extended Graph)
- Plugin **Extended Graph** (ElsaTam), instalado colocando sus archivos en `.obsidian/plugins/extended-graph/`.
- Configurado en su `data.json`:
  - 🎨 **Colores por área** (colorGroups): UPDS azul, Dismac rojo, UDABOL verde, Proyectos teal, MOC dorado.
  - 🔷 **Formas por tipo** (shapeQueries): diamante=`#moc`, hexágono=`#indice`, cuadrado=`#proyecto-codigo`.
  - 📏 **Tamaño por nº de enlaces** (`nodesSizeFunction: degree`).
  - ➡️ **Flechas del color del área** (`linksSameColorAsNode` + `alwaysOpaqueArrows`).
  - 🚫 **Ocultar Inicio** del grafo (`search: -file:"Inicio"`), para ver los 5 espacios separados.
- **Truco clave:** editar la config **con Obsidian cerrado** (si está abierto, la sobrescribe).
- **Bloqueo resuelto:** `maxNodes` estaba en **20** → con 800+ nodos Extended Graph se apagaba. Se subió a **10000**.

## 12. Nueva arquitectura (a mi medida)
- El método PARA no encajaba. Se rediseñó a mi forma de trabajar. Nivel principal:
```
CEREBRO/
├── 🏠 Inicio
├── 📅 Diario       (lo del día a día)
├── 🎓 UPDS         (materias, solo apuntes)
├── 🎓 UDABOL       (materias)
├── 💼 Dismac       (trabajo)
└── 💻 Proyectos    (fichas de mis programas)
```
- Se eliminaron: Inbox, Recursos, Archivo, Plantillas y el wrapper "Áreas".

## 13. Limpiar el código que inflaba
- **Problema:** el cerebro tenía ~12,800 archivos, la mayoría **proyectos de código** metidos en las materias (PROGRAMA1 solo tenía 9,281 — era una librería descargada).
- **Solución:** se sacaron **~38 programas** del vault a `C:\Users\SAMSUNG\Proyectos-Codigo` (con `robocopy /MOVE` por las rutas largas). En **Proyectos/** quedó una **ficha por programa** (qué es, tecnología, dónde está el código).
- **Resultado:** de **~12,800 → ~830 archivos**. Materias con puros apuntes.

## 14. Grafo 3D y modo oscuro
- Plugin **New 3D Graph** (Apoo711) instalado igual (archivos en `.obsidian/plugins/new-3d-graph/`).
- Configurado: nodos por área (5 colores), enlaces celeste `#A5D8FF`, Inicio oculto.
  - *(Limitación: el 3D solo soporta un color de enlace, no por-área como el 2D.)*
- **Tema oscuro** activado (`appearance.json` → `"theme": "obsidian"`).

---

## Estado final
| Componente | Estado |
|---|---|
| 🧠 Estructura a medida (UPDS/UDABOL/Dismac/Diario/Proyectos) | ✅ |
| 🕸️ Todo conectado (índices 📇 + auto-conexión en cada commit/pull) | ✅ |
| ☁️ GitHub privado + 2 computadoras sincronizadas | ✅ |
| 🤖 Auto-guardado (Stop hook + Obsidian Git) | ✅ |
| 💬 Chat con el cerebro (Claude Code + app de escritorio, vía MCP) | ✅ |
| 🎨 Grafo 2D (colores/formas/flechas por área) | ✅ |
| 🌐 Grafo 3D + 🌙 modo oscuro | ✅ |
| 📱 Celular | ⏳ pendiente (Obsidian Sync) |

**De una carpeta vacía a un cerebro completo, conectado, automático y sincronizado.** 🚀

---
Volver a [[🏠 Inicio]]
