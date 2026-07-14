<?php
// Iniciar sesión
session_start();

// Incluir archivo de conexión a la base de datos
include("bd.php");

// Variables para errores y mensajes
$errores = [];
$mensaje = "";

// Verificar si ya está logueado, redirigir al dashboard
if (isset($_SESSION['usuario_id']) && $_SESSION['usuario_id'] != "") {
    header("Location: index.php");
    exit();
}

// Procesar el formulario de login
if ($_POST) {
    // Obtener y limpiar datos del formulario
    $usuario = isset($_POST['usuario']) ? trim($_POST['usuario']) : '';
    $password = isset($_POST['password']) ? $_POST['password'] : '';
    
    // Validaciones básicas
    if (empty($usuario)) {
        $errores[] = "El nombre de usuario es obligatorio";
    }
    
    if (empty($password)) {
        $errores[] = "La contraseña es obligatoria";
    }
    
    // Si no hay errores de validación, proceder con la autenticación
    if (empty($errores)) {
        try {
            // Buscar usuario en la base de datos con su rol
            $stmt = $conexion->prepare("SELECT u.user_id, u.usuario, u.password, u.email, r.nombre_rol, r.descripcion 
                                       FROM usuarios u 
                                       LEFT JOIN roles r ON u.rol_id = r.rol_id 
                                       WHERE u.usuario = :usuario");
            $stmt->bindParam(':usuario', $usuario);
            $stmt->execute();
            
            $usuario_db = $stmt->fetch(PDO::FETCH_ASSOC);
            
            // Verificar si el usuario existe y la contraseña es correcta
            if ($usuario_db && password_verify($password, $usuario_db['password'])) {
                // Login exitoso - crear variables de sesión
                $_SESSION['usuario_id'] = $usuario_db['user_id'];
                $_SESSION['usuario_nombre'] = $usuario_db['usuario'];
                $_SESSION['usuario_email'] = $usuario_db['email'];
                $_SESSION['usuario_rol'] = $usuario_db['nombre_rol'];
                $_SESSION['usuario_rol_desc'] = $usuario_db['descripcion'];
                $_SESSION['login_time'] = time();
                
                // Redirigir al dashboard
                header("Location: index.php");
                exit();
            } else {
                // Depuración temporal
                if (!$usuario_db) {
                    $errores[] = "Usuario no encontrado en la base de datos";
                } elseif (!password_verify($password, $usuario_db['password'])) {
                    $errores[] = "La contraseña no coincide con la almacenada";
                } else {
                    $errores[] = "Usuario o contraseña incorrectos";
                }
            }
        } catch (Exception $e) {
            $errores[] = "Error en el sistema: " . $e->getMessage();
        }
    }
}

// Verificar si hay mensaje de logout
if (isset($_GET['logout'])) {
    $mensaje = "Sesión cerrada correctamente";
}
?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Iniciar Sesión - Bike Store</title>
    
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" 
          rel="stylesheet" 
          integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" 
          crossorigin="anonymous">
    
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
    
    <style>
        body {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        
        .login-card {
            background: rgba(255, 255, 255, 0.95);
            backdrop-filter: blur(10px);
            border-radius: 20px;
            box-shadow: 0 15px 35px rgba(0, 0, 0, 0.1);
            border: 1px solid rgba(255, 255, 255, 0.2);
        }
        
        .login-header {
            background: linear-gradient(135deg, #007bff, #0056b3);
            border-radius: 20px 20px 0 0;
            padding: 2rem;
            text-align: center;
            color: white;
        }
        
        .logo-icon {
            background: rgba(255, 255, 255, 0.2);
            width: 80px;
            height: 80px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 1rem;
            font-size: 2rem;
        }
        
        .form-floating .form-control {
            border-radius: 15px;
            border: 2px solid #e9ecef;
            transition: all 0.3s ease;
        }
        
        .form-floating .form-control:focus {
            border-color: #007bff;
            box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
        }
        
        .btn-login {
            background: linear-gradient(135deg, #007bff, #0056b3);
            border: none;
            border-radius: 15px;
            padding: 12px 30px;
            font-weight: 600;
            transition: all 0.3s ease;
        }
        
        .btn-login:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0, 123, 255, 0.4);
        }
        
        .floating-shapes {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            pointer-events: none;
            z-index: -1;
        }
        
        .shape {
            position: absolute;
            background: rgba(255, 255, 255, 0.1);
            border-radius: 50%;
            animation: float 6s ease-in-out infinite;
        }
        
        .shape:nth-child(1) {
            width: 80px;
            height: 80px;
            top: 20%;
            left: 10%;
            animation-delay: 0s;
        }
        
        .shape:nth-child(2) {
            width: 60px;
            height: 60px;
            top: 60%;
            right: 10%;
            animation-delay: 2s;
        }
        
        .shape:nth-child(3) {
            width: 100px;
            height: 100px;
            bottom: 20%;
            left: 20%;
            animation-delay: 4s;
        }
        
        @keyframes float {
            0%, 100% { transform: translateY(0px); }
            50% { transform: translateY(-20px); }
        }
        
        .alert {
            border-radius: 15px;
            border: none;
        }
    </style>
</head>

<body>
    <!-- Formas flotantes decorativas -->
    <div class="floating-shapes">
        <div class="shape"></div>
        <div class="shape"></div>
        <div class="shape"></div>
    </div>

    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-5 col-xl-4">
                <div class="login-card">
                    <!-- Header del login -->
                    <div class="login-header">
                        <div class="logo-icon">
                            <i class="bi bi-bicycle"></i>
                        </div>
                        <h3 class="fw-bold mb-2">Bike Store</h3>
                        <p class="mb-0 opacity-75">Sistema de Gestión</p>
                    </div>
                    
                    <!-- Cuerpo del formulario -->
                    <div class="p-4">
                        <!-- Mostrar mensaje de logout -->
                        <?php if (!empty($mensaje)): ?>
                        <div class="alert alert-success alert-dismissible fade show" role="alert">
                            <i class="bi bi-check-circle-fill me-2"></i>
                            <?php echo htmlspecialchars($mensaje); ?>
                            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                        </div>
                        <?php endif; ?>

                        <!-- Mostrar errores -->
                        <?php if (!empty($errores)): ?>
                        <div class="alert alert-danger alert-dismissible fade show" role="alert">
                            <i class="bi bi-exclamation-triangle-fill me-2"></i>
                            <strong>Error:</strong>
                            <ul class="mb-0 mt-2">
                                <?php foreach ($errores as $error): ?>
                                    <li><?php echo htmlspecialchars($error); ?></li>
                                <?php endforeach; ?>
                            </ul>
                            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                        </div>
                        <?php endif; ?>

                        <!-- Formulario de login -->
                        <form method="POST" action="" novalidate>
                            <!-- Campo Usuario -->
                            <div class="form-floating mb-4">
                                <input type="text" 
                                       class="form-control" 
                                       id="usuario" 
                                       name="usuario" 
                                       placeholder="Usuario"
                                       value="<?php echo isset($_POST['usuario']) ? htmlspecialchars($_POST['usuario']) : ''; ?>"
                                       required>
                                <label for="usuario">
                                    <i class="bi bi-person me-2"></i>Usuario
                                </label>
                            </div>

                            <!-- Campo Contraseña -->
                            <div class="form-floating mb-4">
                                <input type="password" 
                                       class="form-control" 
                                       id="password" 
                                       name="password" 
                                       placeholder="Contraseña"
                                       required>
                                <label for="password">
                                    <i class="bi bi-lock me-2"></i>Contraseña
                                </label>
                            </div>

                            <!-- Botón de login -->
                            <div class="d-grid mb-4">
                                <button type="submit" class="btn btn-primary btn-login btn-lg">
                                    <i class="bi bi-box-arrow-in-right me-2"></i>
                                    Iniciar Sesión
                                </button>
                            </div>
                        </form>

                        <!-- Información adicional -->
                        <div class="text-center">
                            <small class="text-muted">
                                <i class="bi bi-shield-check me-1"></i>
                                Acceso seguro al sistema
                            </small>
                        </div>
                    </div>
                    
                    <!-- Footer -->
                    <div class="text-center p-3 border-top">
                        <small class="text-muted">
                            <i class="bi bi-building me-1"></i>
                            Universidad Privada Domingo Savio &copy; <?php echo date('Y'); ?>
                        </small>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap JavaScript -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" 
            integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" 
            crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" 
            integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" 
            crossorigin="anonymous"></script>

    <script>
        document.addEventListener('DOMContentLoaded', function() {
            // Enfocar el campo de usuario al cargar
            document.getElementById('usuario').focus();
            
            // Validación en tiempo real
            const form = document.querySelector('form');
            const inputs = form.querySelectorAll('input[required]');
            
            inputs.forEach(function(input) {
                input.addEventListener('blur', function() {
                    if (this.value.trim()) {
                        this.classList.remove('is-invalid');
                        this.classList.add('is-valid');
                    } else {
                        this.classList.remove('is-valid');
                        this.classList.add('is-invalid');
                    }
                });
            });
            
            // Auto-ocultar alertas después de 5 segundos
            setTimeout(function() {
                const alerts = document.querySelectorAll('.alert');
                alerts.forEach(function(alert) {
                    const bsAlert = new bootstrap.Alert(alert);
                    if (bsAlert) {
                        bsAlert.close();
                    }
                });
            }, 5000);
        });
    </script>
</body>
</html>