<?php
// Iniciar sesión siempre al principio
session_start();

// Verificar si hay una solicitud de cambio de idioma
if (isset($_GET['lang']) && in_array($_GET['lang'], ['es', 'en', 'pt'])) {
    $_SESSION['lang'] = $_GET['lang'];
    // Redirigir a la misma página sin el parámetro
    header("Location: " . $_SERVER['PHP_SELF']);
    exit;
}

// Establecer idioma predeterminado si no está definido
if (!isset($_SESSION['lang'])) {
    $_SESSION['lang'] = 'es';
}

// Definir traducciones
$translations = [
    'es' => [
        'bank_name' => 'Banco Mercantil',
        'bank_logo_alt' => 'Logo de Banco Mercantil',
        'login' => 'Iniciar Sesión',
        'username_placeholder' => 'Nombre de usuario',
        'password_placeholder' => 'Contraseña',
        'login_button_text' => 'Iniciar Sesión',
        'forgot_password' => '¿Olvidó su contraseña?',
        'fill_all_fields' => 'Por favor complete todos los campos',
        'spanish' => 'Español',
        'english' => 'English',
        'portuguese' => 'Português',
        'version' => 'Versión'
    ],
    'en' => [
        'bank_name' => 'Mercantil Bank',
        'bank_logo_alt' => 'Mercantil Bank Logo',
        'login' => 'Login',
        'username_placeholder' => 'Username',
        'password_placeholder' => 'Password',
        'login_button_text' => 'Sign In',
        'forgot_password' => 'Forgot password?',
        'fill_all_fields' => 'Please fill in all fields',
        'spanish' => 'Spanish',
        'english' => 'English',
        'portuguese' => 'Portuguese',
        'version' => 'Version'
    ],
    'pt' => [
        'bank_name' => 'Banco Mercantil',
        'bank_logo_alt' => 'Logo do Banco Mercantil',
        'login' => 'Iniciar Sessão',
        'username_placeholder' => 'Nome de usuário',
        'password_placeholder' => 'Senha',
        'login_button_text' => 'Entrar',
        'forgot_password' => 'Esqueceu sua senha?',
        'fill_all_fields' => 'Por favor, preencha todos os campos',
        'spanish' => 'Espanhol',
        'english' => 'Inglês',
        'portuguese' => 'Português',
        'version' => 'Versão'
    ]
];

// Obtener las traducciones para el idioma actual
$lang = $translations[$_SESSION['lang']];
?>
<!DOCTYPE html>
<html lang="<?php echo $_SESSION['lang']; ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $lang['bank_name']; ?> - <?php echo $lang['login']; ?></title>
    <link rel="stylesheet" href="assets/css/StyleLogin.css">
    <link rel="manifest" href="manifest.json">
    <meta name="theme-color" content="#056f1f">
    <style>
        /* Estilos adicionales para los botones de idioma */
        .language-selector {
            margin-top: 20px;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        
        .language-button {
            display: inline-block;
            margin: 0 5px;
            padding: 5px 10px;
            border-radius: 4px;
            text-decoration: none;
            color: #333;
            transition: background-color 0.3s;
        }
        
        .language-button:hover {
            background-color: #f0f0f0;
        }
        
        .language-button.active {
            font-weight: bold;
            background-color: #e0e0e0;
        }
        
        .icon {
            margin-right: 8px;
        }
    </style>
</head>
<body>
    <!-- Contenedor principal del formulario de login -->
    <div class="login-container">
        <!-- LOGO -->
        <div class="logo">
            <img src="assets/img/LOGO-FINAL.png" alt="<?php echo $lang['bank_logo_alt']; ?>">
            <div style="height: 25px;"></div>
            <div class="animated-bar"></div>
        </div>
        
        <!-- TÍTULO -->
        <div style="height: 15px;"></div>
        <h2><?php echo $lang['login']; ?></h2>
        <div style="height: 5px;"></div>

        <?php
        // Mostrar mensaje flash si existe
        if (isset($this->session) && method_exists($this->session, 'getFlashMessage')) {
            $flashMessage = $this->session->getFlashMessage();
            if ($flashMessage) {
                echo '<div class="alert alert-' . $flashMessage['type'] . '">' . $flashMessage['message'] . '</div>';
            }
        }
        ?>

        <!-- FORMULARIO -->
        <form id="login-form" method="POST" action="index.php?controller=usuario&action=autenticar" onsubmit="return validateForm()">
            <input type="hidden" name="csrf_token" value="<?php echo isset($csrfToken) ? $csrfToken : ''; ?>">
            
            <!-- Campo de Usuario -->
            <div class="form-group">
                <div class="input-with-icon">
                    <span class="icon">👤</span>
                    <input type="text" id="username" name="username" placeholder="<?php echo $lang['username_placeholder']; ?>" required autocomplete="username">
                </div>
            </div>
            
            <!-- Campo de Contraseña -->
            <div class="form-group">
                <div class="input-with-icon">
                    <span class="icon">🔒</span>
                    <input type="password" id="password" name="password" placeholder="<?php echo $lang['password_placeholder']; ?>" required autocomplete="current-password">
                </div>
            </div>
            
            <div style="height: 2px;"></div>
            <button type="submit" id="login-button">
                <span id="login-text"><?php echo $lang['login_button_text']; ?></span>
                <span id="loading-spinner" class="spinner-border spinner-border-sm" role="status" aria-hidden="true" style="display: none;"></span>
            </button>
            
            <a href="#" class="recover-password"><?php echo $lang['forgot_password']; ?></a>
        </form>

        <!-- Selector de idioma con enlaces directos pero con estilo similar al selector -->
        <div class="language-selector">
            <span class="icon">🌐</span>
            <a href="?lang=es" class="language-button <?php echo ($_SESSION['lang'] == 'es') ? 'active' : ''; ?>">
                🇪🇸 <?php echo $translations['es']['spanish']; ?>
            </a>
            <a href="?lang=en" class="language-button <?php echo ($_SESSION['lang'] == 'en') ? 'active' : ''; ?>">
                🇺🇸 <?php echo $translations['en']['english']; ?>
            </a>
        </div>

        <!-- Versión -->
        <p class="version"><?php echo $lang['version']; ?> 4.1.0.6</p>
    </div>

    <script>
        // Validación del formulario
        function validateForm() {
            const username = document.getElementById('username').value.trim();
            const password = document.getElementById('password').value.trim();

            if (!username || !password) {
                alert('<?php echo $lang['fill_all_fields']; ?>');
                return false;
            }

            // Mostrar spinner de carga
            document.getElementById('login-text').style.display = 'none';
            document.getElementById('loading-spinner').style.display = 'inline-block';

            return true;
        }
    </script>
</body>
</html>