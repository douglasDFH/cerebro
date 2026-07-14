<?php
$servidor = "localhost"; //127.0.0.1
$basededatos = "appbike_store";
$usuario = "root";
$contrasenia = "";

// Habilitar reporte de errores
error_reporting(E_ALL);
ini_set('display_errors', 1);

try {
    $conexion = new PDO("mysql:host=$servidor;dbname=$basededatos", $usuario, $contrasenia);
    // Configurar PDO para que muestre excepciones cuando ocurran errores
    $conexion->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    // Configurar el juego de caracteres a UTF-8
    $conexion->exec("SET NAMES 'utf8'");
} catch (PDOException $ex) {
    die("Error de conexión: " . $ex->getMessage());
}
?>