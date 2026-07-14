-- PASO 1: Crear base de datos
CREATE DATABASE IF NOT EXISTS bike_store;
USE bike_store;

-- PASO 2: Configuración
SET FOREIGN_KEY_CHECKS = 0;

-- PASO 3: Limpiar
DROP TABLE IF EXISTS order_items;
DROP TABLE IF EXISTS orders; 
DROP TABLE IF EXISTS products;
DROP TABLE IF EXISTS customers;
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS roles;