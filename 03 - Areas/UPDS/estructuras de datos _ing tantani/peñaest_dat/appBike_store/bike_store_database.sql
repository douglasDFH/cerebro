-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 10-10-2025 a las 16:17:33
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `appbike_store`
--
CREATE DATABASE IF NOT EXISTS `appbike_store` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `appbike_store`;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `customers`
--

DROP TABLE IF EXISTS `customers`;
CREATE TABLE IF NOT EXISTS `customers` (
  `customer_id` int(11) NOT NULL AUTO_INCREMENT,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `email` varchar(100) NOT NULL,
  `street` varchar(255) DEFAULT NULL,
  `city` varchar(100) DEFAULT NULL,
  `state` varchar(100) DEFAULT NULL,
  `activo` tinyint(1) DEFAULT 1,
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp(),
  `fecha_actualizacion` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`customer_id`),
  UNIQUE KEY `unique_email_customer` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `customers`
--

INSERT INTO `customers` (`customer_id`, `first_name`, `last_name`, `phone`, `email`, `street`, `city`, `state`, `activo`, `fecha_creacion`, `fecha_actualizacion`) VALUES
(1, 'Carlos', 'Rodríguez', '70123456', 'carlos.rodriguez@email.com', 'Av. Libertador 123', 'La Paz', 'La Paz', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(2, 'María', 'González', '71234567', 'maria.gonzalez@email.com', 'Calle Murillo 456', 'Cochabamba', 'Cochabamba', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(3, 'José', 'Mamani', '72345678', 'jose.mamani@email.com', 'Av. 6 de Agosto 789', 'El Alto', 'La Paz', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(4, 'Ana', 'Quispe', '73456789', 'ana.quispe@email.com', 'Calle Ballivián 321', 'Santa Cruz', 'Santa Cruz', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(5, 'Pedro', 'Vargas', '74567890', 'pedro.vargas@email.com', 'Av. América 654', 'Oruro', 'Oruro', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `orders`
--

DROP TABLE IF EXISTS `orders`;
CREATE TABLE IF NOT EXISTS `orders` (
  `order_id` int(11) NOT NULL AUTO_INCREMENT,
  `customer_id` int(11) NOT NULL,
  `order_date` date NOT NULL,
  `status` enum('pendiente','procesando','enviado','entregado','cancelado') DEFAULT 'pendiente',
  `activo` tinyint(1) DEFAULT 1,
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp(),
  `fecha_actualizacion` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`order_id`),
  KEY `fk_orders_customer` (`customer_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `orders`
--

INSERT INTO `orders` (`order_id`, `customer_id`, `order_date`, `status`, `activo`, `fecha_creacion`, `fecha_actualizacion`) VALUES
(1, 1, '2024-10-01', 'entregado', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(2, 2, '2024-10-03', 'enviado', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(3, 3, '2024-10-05', 'procesando', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(4, 4, '2024-10-07', 'pendiente', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(5, 1, '2024-10-08', 'pendiente', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `order_items`
--

DROP TABLE IF EXISTS `order_items`;
CREATE TABLE IF NOT EXISTS `order_items` (
  `order_item_id` int(11) NOT NULL AUTO_INCREMENT,
  `order_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL DEFAULT 1,
  `price` decimal(10,2) NOT NULL,
  `discount` decimal(5,4) DEFAULT 0.0000,
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`order_item_id`),
  KEY `fk_order_items_order` (`order_id`),
  KEY `fk_order_items_product` (`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `order_items`
--

INSERT INTO `order_items` (`order_item_id`, `order_id`, `product_id`, `quantity`, `price`, `discount`, `fecha_creacion`) VALUES
(1, 1, 1, 1, 2500.00, 0.0000, '2025-10-10 04:02:07'),
(2, 1, 3, 2, 800.00, 0.1000, '2025-10-10 04:02:07'),
(3, 2, 2, 1, 1200.00, 0.0000, '2025-10-10 04:02:07'),
(4, 3, 4, 1, 4500.00, 0.0500, '2025-10-10 04:02:07'),
(5, 3, 6, 1, 1800.00, 0.0000, '2025-10-10 04:02:07'),
(6, 4, 5, 1, 3200.00, 0.0000, '2025-10-10 04:02:07'),
(7, 5, 2, 2, 1200.00, 0.0800, '2025-10-10 04:02:07');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `products`
--

DROP TABLE IF EXISTS `products`;
CREATE TABLE IF NOT EXISTS `products` (
  `product_id` int(11) NOT NULL AUTO_INCREMENT,
  `product_name` varchar(200) NOT NULL,
  `description` text DEFAULT NULL,
  `foto` varchar(255) DEFAULT NULL,
  `tags` varchar(500) DEFAULT NULL,
  `model_year` int(11) DEFAULT NULL,
  `price` decimal(10,2) NOT NULL,
  `stock` int(11) DEFAULT 0,
  `activo` tinyint(1) DEFAULT 1,
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp(),
  `fecha_actualizacion` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `products`
--

INSERT INTO `products` (`product_id`, `product_name`, `description`, `foto`, `tags`, `model_year`, `price`, `stock`, `activo`, `fecha_creacion`, `fecha_actualizacion`) VALUES
(1, 'Bicicleta Mountain Trek X1', 'Bicicleta de montaña con suspensión delantera y marco de aluminio', 'trek_x1.jpg', 'montaña,trek,aluminio,suspensión', 2024, 2500.00, 15, 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(2, 'Bicicleta Urbana City Comfort', 'Bicicleta urbana cómoda para uso diario en la ciudad', 'city_comfort.jpg', 'urbana,ciudad,cómoda,paseo', 2024, 1200.00, 25, 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(3, 'Bicicleta BMX Freestyle Pro', 'Bicicleta BMX para acrobacias y freestyle', 'bmx_pro.jpg', 'bmx,freestyle,acrobacias,jóvenes', 2023, 800.00, 10, 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(4, 'Bicicleta Eléctrica E-Bike 500W', 'Bicicleta eléctrica con motor de 500W y batería de larga duración', 'ebike_500w.jpg', 'eléctrica,motor,batería,ecológica', 2024, 4500.00, 8, 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(5, 'Bicicleta Carrera Speed Racing', 'Bicicleta de carrera ultraligera para competición', 'speed_racing.jpg', 'carrera,velocidad,ligera,competición', 2024, 3200.00, 12, 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(6, 'Bicicleta Híbrida Urban Explorer', 'Bicicleta híbrida perfecta para ciudad y senderos ligeros', 'urban_explorer.jpg', 'híbrida,versátil,ciudad,senderos', 2023, 1800.00, 20, 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `roles`
--

DROP TABLE IF EXISTS `roles`;
CREATE TABLE IF NOT EXISTS `roles` (
  `rol_id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_rol` varchar(50) NOT NULL,
  `descripcion` text DEFAULT NULL,
  `activo` tinyint(1) DEFAULT 1,
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp(),
  `fecha_actualizacion` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`rol_id`),
  UNIQUE KEY `unique_nombre_rol` (`nombre_rol`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `roles`
--

INSERT INTO `roles` (`rol_id`, `nombre_rol`, `descripcion`, `activo`, `fecha_creacion`, `fecha_actualizacion`) VALUES
(1, 'administrador', 'Acceso completo al sistema', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(2, 'vendedor', 'Acceso a ventas y consultas', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(3, 'supervisor', 'Acceso a reportes y supervisión', 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
CREATE TABLE IF NOT EXISTS `usuarios` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `usuario` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `email` varchar(100) NOT NULL,
  `rol_id` int(11) NOT NULL,
  `activo` tinyint(1) DEFAULT 1,
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp(),
  `fecha_actualizacion` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `unique_usuario` (`usuario`),
  UNIQUE KEY `unique_email_usuario` (`email`),
  KEY `fk_usuarios_rol` (`rol_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`user_id`, `usuario`, `password`, `email`, `rol_id`, `activo`, `fecha_creacion`, `fecha_actualizacion`) VALUES
(1, 'admin', '$2y$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'admin@bikestore.com', 1, 1, '2025-10-10 04:02:07', '2025-10-10 04:02:07'),
(2, 'superadmin', '$2y$10$DREaitGjcA7TxXMWOSr7aeAM3QSxsmv8MbVxCwfPRmTpKVGvHvrXi', 'superadmin@bikestore.com', 1, 1, '2025-10-10 04:23:25', '2025-10-10 04:40:39');

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `fk_orders_customer` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`);

--
-- Filtros para la tabla `order_items`
--
ALTER TABLE `order_items`
  ADD CONSTRAINT `fk_order_items_order` FOREIGN KEY (`order_id`) REFERENCES `orders` (`order_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_order_items_product` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`);

--
-- Filtros para la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD CONSTRAINT `fk_usuarios_rol` FOREIGN KEY (`rol_id`) REFERENCES `roles` (`rol_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
