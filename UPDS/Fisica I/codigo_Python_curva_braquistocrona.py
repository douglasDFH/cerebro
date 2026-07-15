import numpy as np
from scipy.optimize import newton, minimize
from scipy.integrate import quad
import matplotlib.pyplot as plt
from matplotlib.animation import FuncAnimation
from matplotlib.widgets import Button

# Aceleración debida a la gravedad (m/s^2); posición final de la esfera (m).
g = 9.81
x2, y2 = 1, 0.65

def cycloid(x2, y2, N=100):
    """Devuelve la trayectoria de la curva de braquistócrona desde (0,0) hasta (x2, y2)."""
    def f(theta):
        return y2/x2 - (1-np.cos(theta))/(theta-np.sin(theta))
    theta2 = newton(f, np.pi/2)
    R = y2 / (1 - np.cos(theta2))
    def x_cycloid(theta):
        return R * (theta - np.sin(theta))
    def y_cycloid(theta):
        return R * (1 - np.cos(theta))
    theta = np.linspace(0, theta2, N)
    x = x_cycloid(theta)
    y = y_cycloid(theta)
    T = theta2 * np.sqrt(R / g)
    print('T(cycloid) = {:.3f}'.format(T))
    return x, y, T

def linear(x2, y2, N=100):
    """Devuelve la trayectoria de una línea recta desde (0,0) hasta (x2, y2)."""
    m = y2 / x2
    x = np.linspace(0, x2, N)
    y = m*x
    T = np.sqrt(2*(1+m**2)/g/m * x2)
    print('T(linear) = {:.3f}'.format(T))
    return x, y, T

def func(x, f, fp):
    """El integrando de la integral de tiempo a minimizar para una trayectoria f(x)."""
    return np.sqrt((1+fp(x)**2) / (2 * g * f(x)))

def circle(x2, y2, N=100):
    """Devuelve la trayectoria de un arco circular entre (0,0) y (x2, y2)."""
    r = (x2*2 + y2*2)/2/x2
    def f(x):
        return np.sqrt(2*r*x - x**2)
    def fp(x):
        return (r-x)/f(x)
    x = np.linspace(0, x2, N)
    y = f(x)
    T = quad(func, 0, x2, args=(f, fp))[0]
    print('T(circle) = {:.3f}'.format(T))
    return x, y, T

def parabola(x2, y2, N=100):
    """Devuelve la trayectoria de un arco parabólico entre (0,0) y (x2, y2)."""
    c = (y2/x2)**2
    def f(x):
        return np.sqrt(c*x)
    def fp(x):
        return c/2/f(x)
    x = np.linspace(0, x2, N)
    y = f(x)
    T = quad(func, 0, x2, args=(f, fp))[0]
    print('T(parabola) = {:.3f}'.format(T))
    return x, y, T

def polynomial_below_cycloid(x2, y2, N=100):
    """Ajusta un polinomio de 6º grado para estar por debajo de la cicloide y devuelve su trayectoria."""
    x_cycloid, y_cycloid, _ = cycloid(x2, y2, N)

    # Ajustar un polinomio de 6º grado a los puntos de la cicloide
    coefficients = np.polyfit(x_cycloid, y_cycloid, 6)
    
    def poly_func(x, coeffs):
        return np.polyval(coeffs, x)
    
    def distance(coeffs):
        y_poly = poly_func(x_cycloid, coeffs)
        return np.sum(np.maximum(0, y_poly - y_cycloid)**2)
    
    # Modificar las restricciones para asegurar que el polinomio pase por (x2, y2)
    constraints = [
        {'type': 'eq', 'fun': lambda coeffs: poly_func(x2, coeffs) - y2},
        {'type': 'ineq', 'fun': lambda coeffs: y_cycloid - poly_func(x_cycloid, coeffs)}
    ]
    
    result = minimize(distance, coefficients, constraints=constraints)
    
    coefficients = result.x
    
    # Asegurarse de que el polinomio esté por debajo de la cicloide
    def f(x):
        return np.minimum(np.polyval(coefficients, x), np.interp(x, x_cycloid, y_cycloid))
    
    def fp(x):
        return np.polyval(np.polyder(coefficients), x)
    
    x = np.linspace(0, x2, N)
    y = f(x)
    
    T = quad(func, 0, x2, args=(f, fp))[0]
    print('T(polynomial fit below cycloid) = {:.3f}'.format(T))
    return x, y, T

# Graficar una figura comparando las cinco trayectorias.
fig, ax = plt.subplots()

# Almacenar los tiempos de cada trayectoria
times = {}

for curve in ('cycloid', 'circle', 'parabola', 'linear', 'polynomial_below_cycloid'):
    x, y, T = globals()[curve](x2, y2)
    ax.plot(x, y, lw=4, alpha=0.5, label='{}: {:.3f} s'.format(curve, T))
    times[curve] = T

# Encontrar el tiempo mínimo
min_curve = min(times, key=times.get)
min_time = times[min_curve]

ax.legend()

# Agregar ecuaciones al gráfico
ax.text(0.5, 1.10, r'Recta: $y = 2(1 - \frac{x}{\pi})$', transform=ax.transAxes, fontsize=12, verticalalignment='top', horizontalalignment='center')
ax.text(0.5, 1.05, r'Parábola: $y = 2(1 - \frac{x}{\pi})^2$', transform=ax.transAxes, fontsize=12, verticalalignment='top', horizontalalignment='center')
ax.text(0.5, 1.00, r'Cicloide: $x = \arccos(y -1) - \sqrt{2y - y^2}$', transform=ax.transAxes, fontsize=12, verticalalignment='top', horizontalalignment='center')
ax.text(0.5, 0.95, r'Círculo: $y = \sqrt{r^2 - x^2}$', transform=ax.transAxes, fontsize=12, verticalalignment='top', horizontalalignment='center')
ax.text(0.5, 0.90, r'Polinomio: $6º \text{ grado ajustado a la cicloide}$', transform=ax.transAxes, fontsize=12, verticalalignment='top', horizontalalignment='center')

# Anotar el tiempo más rápido
ax.text(0.5, 0.85, f'Tiempo más rápido: {min_time:.3f} s ({min_curve})', transform=ax.transAxes, fontsize=12, verticalalignment='top', horizontalalignment='center', color='red')

ax.set_xlabel('$x$')
ax.set_ylabel('$y$')
ax.set_xlim(0, 1)
ax.set_ylim(0.8, 0)
ax.set_aspect('equal')

# Animación de la esfera moviéndose a lo largo de la trayectoria lineal
x_linear, y_linear, T_linear = linear(x2, y2)
sphere_linear, = ax.plot([], [], 'ro', markersize=8)

# Animación de la esfera moviéndose a lo largo de la trayectoria parabólica
x_parabola, y_parabola, T_parabola = parabola(x2, y2)
sphere_parabola, = ax.plot([], [], 'bo', markersize=8)

# Animación de la esfera moviéndose a lo largo de la trayectoria cicloide
x_cycloid, y_cycloid, T_cycloid = cycloid(x2, y2)
sphere_cycloid, = ax.plot([], [], 'go', markersize=8)

# Animación de la esfera moviéndose a lo largo de la trayectoria circular
x_circle, y_circle, T_circle = circle(x2, y2)
sphere_circle, = ax.plot([], [], 'yo', markersize=8)

# Animación de la esfera moviéndose a lo largo de la trayectoria polinómica ajustada
x_poly, y_poly, T_poly = polynomial_below_cycloid(x2, y2)
sphere_poly, = ax.plot([], [], 'mo', markersize=8)

# Función de inicialización para la animación
def init():
    sphere_linear.set_data([], [])
    sphere_parabola.set_data([], [])
    sphere_cycloid.set_data([], [])
    sphere_circle.set_data([], [])
    sphere_poly.set_data([], [])
    return sphere_linear, sphere_parabola, sphere_cycloid, sphere_circle, sphere_poly

# Función de actualización para la animación
def update(frame):
    if frame < len(x_linear):
        sphere_linear.set_data(x_linear[frame], y_linear[frame])
    if frame < len(x_parabola):
        sphere_parabola.set_data(x_parabola[frame], y_parabola[frame])
    if frame < len(x_cycloid):
        sphere_cycloid.set_data(x_cycloid[frame], y_cycloid[frame])
    if frame < len(x_circle):
        sphere_circle.set_data(x_circle[frame], y_circle[frame])
    if frame < len(x_poly):
        sphere_poly.set_data(x_poly[frame], y_poly[frame])
    return sphere_linear, sphere_parabola, sphere_cycloid, sphere_circle, sphere_poly

# Crear la animación
ani = FuncAnimation(fig, update, frames=max(len(x_linear), len(x_parabola), len(x_cycloid), len(x_circle), len(x_poly)), init_func=init, blit=True, repeat=False)

# Agregar botones de inicio, pausa y reinicio
button_ax_start = plt.axes([0.85, 0.05, 0.1, 0.05])
button_start = Button(button_ax_start, 'Start', color='green')

def start_animation(event):
    ani.event_source.start()

button_start.on_clicked(start_animation)

button_ax_stop = plt.axes([0.85, 0.15, 0.1, 0.05])
button_stop = Button(button_ax_stop, 'Stop', color='red')

def stop_animation(event):
    ani.event_source.stop()

button_stop.on_clicked(stop_animation)

button_ax_restart = plt.axes([0.85, 0.25, 0.1, 0.05])
button_restart = Button(button_ax_restart, 'Restart', color='blue')

def restart_animation(event):
    ani.frame_seq = ani.new_frame_seq()
    ani.event_source.start()

button_restart.on_clicked(restart_animation)

plt.show()