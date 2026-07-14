# -*- coding: utf-8 -*-
# Regenera las notas "📇 Índice" que conectan todo el cerebro.
# Portable: detecta solo la raíz del vault (repo git). Lo dispara git (hooks).
import os, glob, subprocess

try:
    VAULT = subprocess.check_output(['git', 'rev-parse', '--show-toplevel'],
                                    text=True).strip()
except Exception:
    VAULT = os.getcwd()
VABS = os.path.abspath(VAULT)

NOISE = {'.git', '.obsidian', '.claude', '.githooks', 'node_modules', 'bin',
         'obj', '.vs', '.vscode', '.angular', 'dist', 'build', 'packages',
         'vendor', '.idea', '__pycache__', '.next', 'out', 'target'}
MARK_FILES = {'package.json', 'composer.json', 'angular.json', 'index.php'}
MARK_EXT = {'.sln', '.csproj', '.pyproj'}

def rel(p): return os.path.relpath(p, VAULT).replace(os.sep, '/')
def is_marker(f): return f in MARK_FILES or os.path.splitext(f)[1].lower() in MARK_EXT
def wl(path, disp):
    t = path[:-3] if path.lower().endswith('.md') else path
    return f"[[{t}|{disp}]]"
def write(path, text):
    try:
        with open(path, 'w', encoding='utf-8') as fh:
            fh.write(text)
    except OSError:
        pass  # rutas demasiado largas u otros: no romper el commit

for f in glob.glob(os.path.join(VAULT, '**', '\U0001F4C7 *.md'), recursive=True):
    try: os.remove(f)
    except OSError: pass

files_in, children, alldirs = {}, {}, []
for root, dirs, files in os.walk(VAULT):
    dirs[:] = sorted([d for d in dirs if d not in NOISE])
    if any(part in NOISE for part in rel(root).split('/')):
        continue
    a = os.path.abspath(root)
    alldirs.append(a)
    files_in[a] = sorted([f for f in files if not (f.startswith('\U0001F4C7 ') and f.endswith('.md'))])
    children[a] = [os.path.abspath(os.path.join(root, d)) for d in dirs]

project_roots = set(d for d in alldirs if any(is_marker(f) for f in files_in[d]))
def inside_project(d):
    return any(d != pr and (d + os.sep).startswith(pr + os.sep) for pr in project_roots)
top_projects = set(pr for pr in project_roots if not inside_project(pr))
def under(d, base): return d == base or (d + os.sep).startswith(base + os.sep)

for pr in top_projects:
    L = ["---", "tags:", "  - indice", "  - proyecto-codigo", "---", "",
         f"# \U0001F4BB {os.path.basename(pr)} — Proyecto de código", "",
         "> [!info] Proyecto de código", "> Nivel principal (interior no listado).", ""]
    subs = [os.path.basename(c) for c in children[pr]]
    if subs:
        L += ["## \U0001F4C1 Carpetas del proyecto"] + [f"- {s}" for s in subs] + [""]
    if files_in[pr]:
        L.append("## \U0001F4C4 Archivos principales")
        L += ["- " + wl(rel(os.path.join(pr, f)), os.path.splitext(f)[0]) for f in files_in[pr]]
        L.append("")
    L += ["---", "Volver a [[\U0001F3E0 Inicio]]", ""]
    write(os.path.join(pr, f"\U0001F4C7 {os.path.basename(pr)} (proyecto).md"), "\n".join(L))

sections = []
for area in ["03 - Areas/UPDS", "03 - Areas/UDABOL", "03 - Areas/Dismac (Trabajo)"]:
    ap = os.path.abspath(os.path.join(VAULT, area))
    if os.path.isdir(ap):
        for c in sorted(children.get(ap, [])):
            sections.append((c, area))
for name in ["00 - Inbox", "01 - Diario", "02 - Proyectos", "04 - Recursos", "05 - Archivo", "99 - Plantillas"]:
    p = os.path.abspath(os.path.join(VAULT, name))
    if os.path.isdir(p):
        sections.append((p, None))

MOC_UP = {"03 - Areas/UPDS": "[[\U0001F393 UPDS - MOC]]",
          "03 - Areas/UDABOL": "[[\U0001F393 UDABOL - MOC]]",
          "03 - Areas/Dismac (Trabajo)": "[[\U0001F4BC Dismac - MOC]]"}

for sec, area in sections:
    up = MOC_UP.get(area, "[[\U0001F3E0 Inicio]]")
    if sec in top_projects:
        continue
    groups, projs = {}, []
    for d in alldirs:
        if not under(d, sec): continue
        if d in top_projects: projs.append(d); continue
        if inside_project(d): continue
        if files_in[d]:
            key = rel(d)[len(rel(sec)):].strip('/') or "(principal)"
            groups[key] = files_in[d]
    name = os.path.basename(sec)
    L = ["---", "tags:", "  - indice", "---", "",
         f"# \U0001F4C7 Índice — {name}", "", f"> \U0001F9ED {up} · [[\U0001F3E0 Inicio]]", ""]
    for key in sorted(groups):
        if key != "(principal)": L.append(f"### \U0001F4C1 {key}")
        base = sec if key == "(principal)" else os.path.join(sec, key)
        L += ["- " + wl(rel(os.path.join(base, f)), os.path.splitext(f)[0]) for f in groups[key]]
        L.append("")
    if projs:
        L.append("### \U0001F4BB Proyectos de código")
        for pr in sorted(projs):
            L.append("- " + wl(rel(os.path.join(pr, f"\U0001F4C7 {os.path.basename(pr)} (proyecto).md")), os.path.basename(pr)))
        L.append("")
    L += ["---", "Volver a " + up + " · [[\U0001F3E0 Inicio]]", ""]
    write(os.path.join(sec, f"\U0001F4C7 {name}.md"), "\n".join(L))
