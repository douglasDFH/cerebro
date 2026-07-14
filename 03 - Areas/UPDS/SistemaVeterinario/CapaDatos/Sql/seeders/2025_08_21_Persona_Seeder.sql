-- =============================================
-- Script: 2025_08_21_Persona_Seeder.sql  
-- Descripción: Seeder para personas físicas y jurídicas con datos bolivianos
-- Fecha: 21 de agosto de 2025
-- Autor: Sistema Veterinario
-- Versión: 1.0
-- Genera: 100 personas físicas y 100 jurídicas
-- Usa: SP01_CreateOrUpdatePFisica, SP02_CreateOrUpdatePJuridica
-- =============================================

-- =============================================
-- PERSONAS FÍSICAS (100 registros)
-- =============================================

PRINT 'Insertando 100 Personas Físicas...'

-- 1-10
EXEC SP01_CreateOrUpdatePFisica @ci = '10000001', @nombre = 'Maria Elena', @apellido = 'Quispe Mamani', @email = 'maria.quispe1@gmail.com', @direccion = 'Av. Buenos Aires #1234, Zona Sur', @telefono = '78123456', @fecha_nacimiento = '1985-03-15', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000002', @nombre = 'Carlos Alberto', @apellido = 'Mamani Condori', @email = 'carlos.mamani2@hotmail.com', @direccion = 'Calle Comercio #567, El Alto', @telefono = '69876543', @fecha_nacimiento = '1978-11-22', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000003', @nombre = 'Ana Lucia', @apellido = 'Vargas Flores', @email = 'ana.vargas3@yahoo.com', @direccion = 'Av. 6 de Agosto #890, San Pedro', @telefono = '72345678', @fecha_nacimiento = '1992-07-08', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000004', @nombre = 'Roberto', @apellido = 'Choque Apaza', @email = 'roberto.choque4@gmail.com', @direccion = 'Calle Murillo #123, Centro', @telefono = '65432109', @fecha_nacimiento = '1980-12-03', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000005', @nombre = 'Patricia', @apellido = 'Torrez Limachi', @email = 'patricia.torrez5@outlook.com', @direccion = 'Av. América #456, Miraflores', @telefono = '76543210', @fecha_nacimiento = '1987-05-18', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000006', @nombre = 'Luis Fernando', @apellido = 'Gutierrez Poma', @email = 'luis.gutierrez6@gmail.com', @direccion = 'Calle Sagarnaga #789, Rosario', @telefono = '71234567', @fecha_nacimiento = '1975-09-25', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000007', @nombre = 'Silvia', @apellido = 'Mendoza Cruz', @email = 'silvia.mendoza7@hotmail.com', @direccion = 'Av. Ballivián #321, Calacoto', @telefono = '68901234', @fecha_nacimiento = '1990-02-14', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000008', @nombre = 'Juan Carlos', @apellido = 'Huanca Ticona', @email = 'juan.huanca8@yahoo.com', @direccion = 'Calle Ayacucho #654, Sopocachi', @telefono = '77890123', @fecha_nacimiento = '1983-08-07', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000009', @nombre = 'Carmen Rosa', @apellido = 'Chipana Yujra', @email = 'carmen.chipana9@gmail.com', @direccion = 'Av. Arce #987, San Jorge', @telefono = '73456789', @fecha_nacimiento = '1988-04-12', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000010', @nombre = 'Miguel Angel', @apellido = 'Colque Mamani', @email = 'miguel.colque10@outlook.com', @direccion = 'Calle Linares #147, Centro', @telefono = '74567890', @fecha_nacimiento = '1979-01-30', @genero = 'M';

-- 11-20
EXEC SP01_CreateOrUpdatePFisica @ci = '10000011', @nombre = 'Juana', @apellido = 'Mamani Quispe', @email = 'juana.mamani11@gmail.com', @direccion = 'Av. Del Ejército #234, Villa Fátima', @telefono = '75123890', @fecha_nacimiento = '1986-06-12', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000012', @nombre = 'Pedro', @apellido = 'Condori Flores', @email = 'pedro.condori12@hotmail.com', @direccion = 'Calle Murillo #567, Centro', @telefono = '68234567', @fecha_nacimiento = '1982-09-03', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000013', @nombre = 'Rosa', @apellido = 'Flores Apaza', @email = 'rosa.flores13@yahoo.com', @direccion = 'Av. Montes #890, San Pedro', @telefono = '71345678', @fecha_nacimiento = '1991-02-28', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000014', @nombre = 'Eduardo', @apellido = 'Apaza Limachi', @email = 'eduardo.apaza14@gmail.com', @direccion = 'Calle Comercio #123, El Alto', @telefono = '69456789', @fecha_nacimiento = '1977-12-15', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000015', @nombre = 'Sandra', @apellido = 'Limachi Poma', @email = 'sandra.limachi15@outlook.com', @direccion = 'Av. América #456, Miraflores', @telefono = '76567890', @fecha_nacimiento = '1989-05-07', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000016', @nombre = 'Fernando', @apellido = 'Poma Cruz', @email = 'fernando.poma16@gmail.com', @direccion = 'Calle Sagarnaga #789, Rosario', @telefono = '72678901', @fecha_nacimiento = '1984-08-20', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000017', @nombre = 'Gloria', @apellido = 'Cruz Ticona', @email = 'gloria.cruz17@hotmail.com', @direccion = 'Av. Ballivián #321, Calacoto', @telefono = '67789012', @fecha_nacimiento = '1993-11-11', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000018', @nombre = 'Raul', @apellido = 'Ticona Yujra', @email = 'raul.ticona18@yahoo.com', @direccion = 'Calle Ayacucho #654, Sopocachi', @telefono = '78890123', @fecha_nacimiento = '1981-04-03', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000019', @nombre = 'Marta', @apellido = 'Yujra Choque', @email = 'marta.yujra19@gmail.com', @direccion = 'Av. Arce #987, San Jorge', @telefono = '73901234', @fecha_nacimiento = '1988-07-25', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000020', @nombre = 'Daniel', @apellido = 'Choque Vargas', @email = 'daniel.choque20@outlook.com', @direccion = 'Calle Linares #147, Centro', @telefono = '74012345', @fecha_nacimiento = '1976-10-18', @genero = 'M';

-- 21-30
EXEC SP01_CreateOrUpdatePFisica @ci = '10000021', @nombre = 'Elena', @apellido = 'Vargas Torrez', @email = 'elena.vargas21@gmail.com', @direccion = 'Av. 16 de Julio #234, El Prado', @telefono = '75123456', @fecha_nacimiento = '1987-01-14', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000022', @nombre = 'Antonio', @apellido = 'Torrez Gutierrez', @email = 'antonio.torrez22@hotmail.com', @direccion = 'Calle Santa Cruz #567, Centro', @telefono = '68234789', @fecha_nacimiento = '1983-04-08', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000023', @nombre = 'Lucia', @apellido = 'Gutierrez Mendoza', @email = 'lucia.gutierrez23@yahoo.com', @direccion = 'Av. Camacho #890, Centro', @telefono = '71345012', @fecha_nacimiento = '1990-06-22', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000024', @nombre = 'Francisco', @apellido = 'Mendoza Huanca', @email = 'francisco.mendoza24@gmail.com', @direccion = 'Calle Potosí #123, San Pedro', @telefono = '69456345', @fecha_nacimiento = '1979-09-05', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000025', @nombre = 'Isabel', @apellido = 'Huanca Chipana', @email = 'isabel.huanca25@outlook.com', @direccion = 'Av. Illimani #456, San Antonio', @telefono = '76567678', @fecha_nacimiento = '1985-12-17', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000026', @nombre = 'Ricardo', @apellido = 'Chipana Colque', @email = 'ricardo.chipana26@gmail.com', @direccion = 'Calle Sucre #789, Centro', @telefono = '72678901', @fecha_nacimiento = '1982-03-30', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000027', @nombre = 'Angela', @apellido = 'Colque Quispe', @email = 'angela.colque27@hotmail.com', @direccion = 'Av. Mcal. Santa Cruz #321, San Jorge', @telefono = '67789234', @fecha_nacimiento = '1994-05-13', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000028', @nombre = 'Gonzalo', @apellido = 'Quispe Mamani', @email = 'gonzalo.quispe28@yahoo.com', @direccion = 'Calle Jaén #654, Rosario', @telefono = '78890567', @fecha_nacimiento = '1981-08-26', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000029', @nombre = 'Teresa', @apellido = 'Mamani Condori', @email = 'teresa.mamani29@gmail.com', @direccion = 'Av. Tejada Sorzano #987, Miraflores', @telefono = '73901890', @fecha_nacimiento = '1986-11-09', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000030', @nombre = 'Hernan', @apellido = 'Condori Flores', @email = 'hernan.condori30@outlook.com', @direccion = 'Calle Cochabamba #147, Centro', @telefono = '74012123', @fecha_nacimiento = '1978-02-21', @genero = 'M';

-- 31-40
EXEC SP01_CreateOrUpdatePFisica @ci = '10000031', @nombre = 'Roxana', @apellido = 'Flores Apaza', @email = 'roxana.flores31@gmail.com', @direccion = 'Av. Busch #234, Sopocachi', @telefono = '75123456', @fecha_nacimiento = '1989-07-04', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000032', @nombre = 'Alberto', @apellido = 'Apaza Limachi', @email = 'alberto.apaza32@hotmail.com', @direccion = 'Calle Indaburo #567, Centro', @telefono = '68234789', @fecha_nacimiento = '1984-10-17', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000033', @nombre = 'Margarita', @apellido = 'Limachi Poma', @email = 'margarita.limachi33@yahoo.com', @direccion = 'Av. Saavedra #890, Miraflores', @telefono = '71345012', @fecha_nacimiento = '1991-01-30', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000034', @nombre = 'Victor', @apellido = 'Poma Cruz', @email = 'victor.poma34@gmail.com', @direccion = 'Calle Yanacocha #123, Zona Sur', @telefono = '69456345', @fecha_nacimiento = '1977-05-12', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000035', @nombre = 'Blanca', @apellido = 'Cruz Ticona', @email = 'blanca.cruz35@outlook.com', @direccion = 'Av. Los Sauces #456, Calacoto', @telefono = '76567678', @fecha_nacimiento = '1993-08-25', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000036', @nombre = 'Alfredo', @apellido = 'Ticona Yujra', @email = 'alfredo.ticona36@gmail.com', @direccion = 'Calle Bolívar #789, Centro', @telefono = '72678901', @fecha_nacimiento = '1980-11-07', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000037', @nombre = 'Veronica', @apellido = 'Yujra Choque', @email = 'veronica.yujra37@hotmail.com', @direccion = 'Av. Kantutani #321, Villa Fátima', @telefono = '67789234', @fecha_nacimiento = '1988-02-19', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000038', @nombre = 'Julio', @apellido = 'Choque Vargas', @email = 'julio.choque38@yahoo.com', @direccion = 'Calle Genaro Sanjinés #654, Centro', @telefono = '78890567', @fecha_nacimiento = '1985-06-01', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000039', @nombre = 'Monica', @apellido = 'Vargas Torrez', @email = 'monica.vargas39@gmail.com', @direccion = 'Av. Arce #987, San Jorge', @telefono = '73901890', @fecha_nacimiento = '1992-09-14', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000040', @nombre = 'Sergio', @apellido = 'Torrez Gutierrez', @email = 'sergio.torrez40@outlook.com', @direccion = 'Calle México #147, Centro', @telefono = '74012123', @fecha_nacimiento = '1976-12-27', @genero = 'M';

-- 41-50
EXEC SP01_CreateOrUpdatePFisica @ci = '10000041', @nombre = 'Graciela', @apellido = 'Gutierrez Mendoza', @email = 'graciela.gutierrez41@gmail.com', @direccion = 'Av. Panorámica #234, Villa San Antonio', @telefono = '75123789', @fecha_nacimiento = '1987-03-11', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000042', @nombre = 'Jaime', @apellido = 'Mendoza Huanca', @email = 'jaime.mendoza42@hotmail.com', @direccion = 'Calle Final Landaeta #567, Sopocachi', @telefono = '68234012', @fecha_nacimiento = '1983-06-24', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000043', @nombre = 'Adriana', @apellido = 'Huanca Chipana', @email = 'adriana.huanca43@yahoo.com', @direccion = 'Av. Montenegro #890, San Antonio', @telefono = '71345345', @fecha_nacimiento = '1990-09-06', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000044', @nombre = 'Marcelo', @apellido = 'Chipana Colque', @email = 'marcelo.chipana44@gmail.com', @direccion = 'Calle Calama #123, San Pedro', @telefono = '69456678', @fecha_nacimiento = '1979-12-19', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000045', @nombre = 'Claudia', @apellido = 'Colque Quispe', @email = 'claudia.colque45@outlook.com', @direccion = 'Av. Ballivián #456, Calacoto', @telefono = '76567901', @fecha_nacimiento = '1985-01-01', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000046', @nombre = 'Oscar', @apellido = 'Quispe Mamani', @email = 'oscar.quispe46@gmail.com', @direccion = 'Calle Rosendo Gutierrez #789, Sopocachi', @telefono = '72678234', @fecha_nacimiento = '1982-04-14', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000047', @nombre = 'Paola', @apellido = 'Mamani Condori', @email = 'paola.mamani47@hotmail.com', @direccion = 'Av. Arce #321, San Jorge', @telefono = '67789567', @fecha_nacimiento = '1994-07-27', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000048', @nombre = 'Ramiro', @apellido = 'Condori Flores', @email = 'ramiro.condori48@yahoo.com', @direccion = 'Calle Loayza #654, Centro', @telefono = '78890890', @fecha_nacimiento = '1981-10-09', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000049', @nombre = 'Ximena', @apellido = 'Flores Apaza', @email = 'ximena.flores49@gmail.com', @direccion = 'Av. 20 de Octubre #987, Sopocachi', @telefono = '73901123', @fecha_nacimiento = '1988-01-22', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000050', @nombre = 'Diego', @apellido = 'Apaza Limachi', @email = 'diego.apaza50@outlook.com', @direccion = 'Calle Almirante Grau #147, Centro', @telefono = '74012456', @fecha_nacimiento = '1977-05-04', @genero = 'M';

-- 51-60
EXEC SP01_CreateOrUpdatePFisica @ci = '10000051', @nombre = 'Carla', @apellido = 'Limachi Poma', @email = 'carla.limachi51@gmail.com', @direccion = 'Av. Hernando Siles #234, Calacoto', @telefono = '75123012', @fecha_nacimiento = '1989-08-17', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000052', @nombre = 'Rolando', @apellido = 'Poma Cruz', @email = 'rolando.poma52@hotmail.com', @direccion = 'Calle Tarija #567, Centro', @telefono = '68234345', @fecha_nacimiento = '1984-11-30', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000053', @nombre = 'Beatriz', @apellido = 'Cruz Ticona', @email = 'beatriz.cruz53@yahoo.com', @direccion = 'Av. Costanera #890, Zona Sur', @telefono = '71345678', @fecha_nacimiento = '1991-03-12', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000054', @nombre = 'Gustavo', @apellido = 'Ticona Yujra', @email = 'gustavo.ticona54@gmail.com', @direccion = 'Calle Evaristo Valle #123, Sopocachi', @telefono = '69456901', @fecha_nacimiento = '1978-06-25', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000055', @nombre = 'Lorena', @apellido = 'Yujra Choque', @email = 'lorena.yujra55@outlook.com', @direccion = 'Av. Ecuador #456, San Jorge', @telefono = '76567234', @fecha_nacimiento = '1993-09-07', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000056', @nombre = 'Ernesto', @apellido = 'Choque Vargas', @email = 'ernesto.choque56@gmail.com', @direccion = 'Calle Capitán Ravelo #789, Centro', @telefono = '72678567', @fecha_nacimiento = '1980-12-20', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000057', @nombre = 'Vanessa', @apellido = 'Vargas Torrez', @email = 'vanessa.vargas57@hotmail.com', @direccion = 'Av. Villazón #321, Miraflores', @telefono = '67789890', @fecha_nacimiento = '1987-02-02', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000058', @nombre = 'Mario', @apellido = 'Torrez Gutierrez', @email = 'mario.torrez58@yahoo.com', @direccion = 'Calle Oruro #654, Centro', @telefono = '78890123', @fecha_nacimiento = '1985-05-15', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000059', @nombre = 'Natalia', @apellido = 'Gutierrez Mendoza', @email = 'natalia.gutierrez59@gmail.com', @direccion = 'Av. Sanchez Lima #987, Sopocachi', @telefono = '73901456', @fecha_nacimiento = '1992-08-28', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000060', @nombre = 'Hector', @apellido = 'Mendoza Huanca', @email = 'hector.mendoza60@outlook.com', @direccion = 'Calle Beni #147, Centro', @telefono = '74012789', @fecha_nacimiento = '1976-11-10', @genero = 'M';

-- 61-70
EXEC SP01_CreateOrUpdatePFisica @ci = '10000061', @nombre = 'Andrea', @apellido = 'Huanca Chipana', @email = 'andrea.huanca61@gmail.com', @direccion = 'Av. Libertador Bolívar #234, Villa Fátima', @telefono = '75123345', @fecha_nacimiento = '1989-01-23', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000062', @nombre = 'Gabriel', @apellido = 'Chipana Colque', @email = 'gabriel.chipana62@hotmail.com', @direccion = 'Calle Chuquisaca #567, Centro', @telefono = '68234678', @fecha_nacimiento = '1983-04-05', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000063', @nombre = 'Daniela', @apellido = 'Colque Quispe', @email = 'daniela.colque63@yahoo.com', @direccion = 'Av. Los Alamos #890, Calacoto', @telefono = '71345901', @fecha_nacimiento = '1990-07-18', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000064', @nombre = 'Felipe', @apellido = 'Quispe Mamani', @email = 'felipe.quispe64@gmail.com', @direccion = 'Calle Estudiantes #123, San Pedro', @telefono = '69456234', @fecha_nacimiento = '1979-10-31', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000065', @nombre = 'Valeria', @apellido = 'Mamani Condori', @email = 'valeria.mamani65@outlook.com', @direccion = 'Av. Prado #456, Centro', @telefono = '76567567', @fecha_nacimiento = '1985-12-13', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000066', @nombre = 'Rodrigo', @apellido = 'Condori Flores', @email = 'rodrigo.condori66@gmail.com', @direccion = 'Calle Illampu #789, Centro', @telefono = '72678890', @fecha_nacimiento = '1982-03-26', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000067', @nombre = 'Alejandra', @apellido = 'Flores Apaza', @email = 'alejandra.flores67@hotmail.com', @direccion = 'Av. Kantutani #321, Villa Fátima', @telefono = '67789123', @fecha_nacimiento = '1994-06-08', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000068', @nombre = 'Andres', @apellido = 'Apaza Limachi', @email = 'andres.apaza68@yahoo.com', @direccion = 'Calle Santa Bárbara #654, San Jorge', @telefono = '78890456', @fecha_nacimiento = '1981-09-21', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000069', @nombre = 'Karina', @apellido = 'Limachi Poma', @email = 'karina.limachi69@gmail.com', @direccion = 'Av. Montes #987, San Pedro', @telefono = '73901789', @fecha_nacimiento = '1988-12-03', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000070', @nombre = 'Cristian', @apellido = 'Poma Cruz', @email = 'cristian.poma70@outlook.com', @direccion = 'Calle Max Paredes #147, Centro', @telefono = '74012012', @fecha_nacimiento = '1977-02-16', @genero = 'M';

-- 71-80
EXEC SP01_CreateOrUpdatePFisica @ci = '10000071', @nombre = 'Soledad', @apellido = 'Cruz Ticona', @email = 'soledad.cruz71@gmail.com', @direccion = 'Av. 6 de Agosto #234, San Pedro', @telefono = '75123678', @fecha_nacimiento = '1989-05-29', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000072', @nombre = 'Mauricio', @apellido = 'Ticona Yujra', @email = 'mauricio.ticona72@hotmail.com', @direccion = 'Calle Sagarnaga #567, Rosario', @telefono = '68234901', @fecha_nacimiento = '1984-08-11', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000073', @nombre = 'Fabiola', @apellido = 'Yujra Choque', @email = 'fabiola.yujra73@yahoo.com', @direccion = 'Av. Arce #890, San Jorge', @telefono = '71345234', @fecha_nacimiento = '1991-11-24', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000074', @nombre = 'Ivan', @apellido = 'Choque Vargas', @email = 'ivan.choque74@gmail.com', @direccion = 'Calle Sucre #123, Centro', @telefono = '69456567', @fecha_nacimiento = '1978-01-06', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000075', @nombre = 'Pamela', @apellido = 'Vargas Torrez', @email = 'pamela.vargas75@outlook.com', @direccion = 'Av. Del Poeta #456, Calacoto', @telefono = '76567890', @fecha_nacimiento = '1993-04-19', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000076', @nombre = 'Aldo', @apellido = 'Torrez Gutierrez', @email = 'aldo.torrez76@gmail.com', @direccion = 'Calle Colón #789, Centro', @telefono = '72678123', @fecha_nacimiento = '1980-07-02', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000077', @nombre = 'Miriam', @apellido = 'Gutierrez Mendoza', @email = 'miriam.gutierrez77@hotmail.com', @direccion = 'Av. Ballivián #321, Calacoto', @telefono = '67789456', @fecha_nacimiento = '1987-10-14', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000078', @nombre = 'Armando', @apellido = 'Mendoza Huanca', @email = 'armando.mendoza78@yahoo.com', @direccion = 'Calle Junín #654, Centro', @telefono = '78890789', @fecha_nacimiento = '1985-01-27', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000079', @nombre = 'Jimena', @apellido = 'Huanca Chipana', @email = 'jimena.huanca79@gmail.com', @direccion = 'Av. 20 de Octubre #987, Sopocachi', @telefono = '73901012', @fecha_nacimiento = '1992-05-09', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000080', @nombre = 'Esteban', @apellido = 'Chipana Colque', @email = 'esteban.chipana80@outlook.com', @direccion = 'Calle Federico Zuazo #147, Centro', @telefono = '74012345', @fecha_nacimiento = '1976-08-22', @genero = 'M';

-- 81-90
EXEC SP01_CreateOrUpdatePFisica @ci = '10000081', @nombre = 'Rebeca', @apellido = 'Colque Quispe', @email = 'rebeca.colque81@gmail.com', @direccion = 'Av. Costanera #234, Zona Sur', @telefono = '75123901', @fecha_nacimiento = '1989-11-04', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000082', @nombre = 'Ignacio', @apellido = 'Quispe Mamani', @email = 'ignacio.quispe82@hotmail.com', @direccion = 'Calle Yanacocha #567, Zona Sur', @telefono = '68234234', @fecha_nacimiento = '1983-12-17', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000083', @nombre = 'Sonia', @apellido = 'Mamani Condori', @email = 'sonia.mamani83@yahoo.com', @direccion = 'Av. Hernando Siles #890, Calacoto', @telefono = '71345567', @fecha_nacimiento = '1990-02-28', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000084', @nombre = 'Nestor', @apellido = 'Condori Flores', @email = 'nestor.condori84@gmail.com', @direccion = 'Calle Campero #123, Centro', @telefono = '69456890', @fecha_nacimiento = '1979-06-12', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000085', @nombre = 'Lilian', @apellido = 'Flores Apaza', @email = 'lilian.flores85@outlook.com', @direccion = 'Av. Los Alamos #456, Calacoto', @telefono = '76567123', @fecha_nacimiento = '1985-09-25', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000086', @nombre = 'Walter', @apellido = 'Apaza Limachi', @email = 'walter.apaza86@gmail.com', @direccion = 'Calle Plaza Villarroel #789, Centro', @telefono = '72678456', @fecha_nacimiento = '1982-01-07', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000087', @nombre = 'Gladys', @apellido = 'Limachi Poma', @email = 'gladys.limachi87@hotmail.com', @direccion = 'Av. Montenegro #321, San Antonio', @telefono = '67789789', @fecha_nacimiento = '1994-04-20', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000088', @nombre = 'Gerardo', @apellido = 'Poma Cruz', @email = 'gerardo.poma88@yahoo.com', @direccion = 'Calle Baptista #654, Villa Fátima', @telefono = '78890012', @fecha_nacimiento = '1981-07-03', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000089', @nombre = 'Carolina', @apellido = 'Cruz Ticona', @email = 'carolina.cruz89@gmail.com', @direccion = 'Av. Panorámica #987, Villa San Antonio', @telefono = '73901345', @fecha_nacimiento = '1988-10-16', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000090', @nombre = 'Rolber', @apellido = 'Ticona Yujra', @email = 'rolber.ticona90@outlook.com', @direccion = 'Calle Pichincha #147, Centro', @telefono = '74012678', @fecha_nacimiento = '1977-12-28', @genero = 'M';

-- 91-100
EXEC SP01_CreateOrUpdatePFisica @ci = '10000091', @nombre = 'Evelyn', @apellido = 'Yujra Choque', @email = 'evelyn.yujra91@gmail.com', @direccion = 'Av. Villazon #234, Miraflores', @telefono = '75123234', @fecha_nacimiento = '1989-03-11', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000092', @nombre = 'Ruben', @apellido = 'Choque Vargas', @email = 'ruben.choque92@hotmail.com', @direccion = 'Calle Ingavi #567, Centro', @telefono = '68234567', @fecha_nacimiento = '1984-06-24', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000093', @nombre = 'Susana', @apellido = 'Vargas Torrez', @email = 'susana.vargas93@yahoo.com', @direccion = 'Av. Sanchez Lima #890, Sopocachi', @telefono = '71345890', @fecha_nacimiento = '1991-09-06', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000094', @nombre = 'Freddy', @apellido = 'Torrez Gutierrez', @email = 'freddy.torrez94@gmail.com', @direccion = 'Calle Catacora #123, Villa Fátima', @telefono = '69456123', @fecha_nacimiento = '1978-12-19', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000095', @nombre = 'Elizabeth', @apellido = 'Gutierrez Mendoza', @email = 'elizabeth.gutierrez95@outlook.com', @direccion = 'Av. Busch #456, Sopocachi', @telefono = '76567456', @fecha_nacimiento = '1993-01-01', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000096', @nombre = 'Wilson', @apellido = 'Mendoza Huanca', @email = 'wilson.mendoza96@gmail.com', @direccion = 'Calle Calama #789, San Pedro', @telefono = '72678789', @fecha_nacimiento = '1980-04-14', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000097', @nombre = 'Jenny', @apellido = 'Huanca Chipana', @email = 'jenny.huanca97@hotmail.com', @direccion = 'Av. Libertador Bolívar #321, Villa Fátima', @telefono = '67789012', @fecha_nacimiento = '1987-07-27', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000098', @nombre = 'Jhonny', @apellido = 'Chipana Colque', @email = 'jhonny.chipana98@yahoo.com', @direccion = 'Calle Loayza #654, Centro', @telefono = '78890345', @fecha_nacimiento = '1985-10-09', @genero = 'M';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000099', @nombre = 'Rosario', @apellido = 'Colque Quispe', @email = 'rosario.colque99@gmail.com', @direccion = 'Av. Arce #987, San Jorge', @telefono = '73901678', @fecha_nacimiento = '1992-01-22', @genero = 'F';
EXEC SP01_CreateOrUpdatePFisica @ci = '10000100', @nombre = 'Edgar', @apellido = 'Quispe Mamani', @email = 'edgar.quispe100@outlook.com', @direccion = 'Calle Almirante Grau #147, Centro', @telefono = '74012901', @fecha_nacimiento = '1976-05-04', @genero = 'M';

PRINT 'Personas Físicas insertadas correctamente.'

-- =============================================
-- PERSONAS JURÍDICAS (100 registros)
-- =============================================

PRINT 'Insertando 100 Personas Jurídicas...'

-- 1-10
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Veterinaria San Francisco SRL', @nit = '20000001', @encargado_nombre = 'Dr. Eduardo Mamani', @encargado_cargo = 'Director Veterinario', @email = 'info@vetsanfrancisco1.bo', @direccion = 'Av. 6 de Agosto #1250, La Paz', @telefono = '22451234';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria Esperanza', @nit = '20000002', @encargado_nombre = 'Dra. Carmen Quispe', @encargado_cargo = 'Gerente General', @email = 'contacto@veterinariaesperanza2.com', @direccion = 'Calle Comercio #890, El Alto', @telefono = '28901234';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Pet Shop Los Amigos EIRL', @nit = '20000003', @encargado_nombre = 'Carlos Vargas', @encargado_cargo = 'Propietario', @email = 'ventas@petshopamigos3.bo', @direccion = 'Av. Heroínas #567, Cochabamba', @telefono = '44123456';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Distribuidora Agroveterinaria Bolivia', @nit = '20000004', @encargado_nombre = 'Ana Torrez', @encargado_cargo = 'Gerente de Ventas', @email = 'distribucion@agrovet4.com.bo', @direccion = 'Zona Industrial, Villa El Carmen, Santa Cruz', @telefono = '33789012';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro Veterinario Paws & Claws', @nit = '20000005', @encargado_nombre = 'Dr. Roberto Choque', @encargado_cargo = 'Veterinario Jefe', @email = 'consultas@pawsandclaws5.bo', @direccion = 'Av. América #234, Miraflores, La Paz', @telefono = '22567890';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Laboratorio Veterinario del Altiplano', @nit = '20000006', @encargado_nombre = 'Dra. Patricia Mendoza', @encargado_cargo = 'Directora Técnica', @email = 'laboratorio@vetaltiplano6.com', @direccion = 'Calle Potosí #123, La Paz', @telefono = '22345678';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Granja Avícola San Martín', @nit = '20000007', @encargado_nombre = 'Luis Gutierrez', @encargado_cargo = 'Gerente de Producción', @email = 'produccion@granjasanmartin7.bo', @direccion = 'Km 15 Carretera a Oruro, La Paz', @telefono = '22901234';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Veterinaria Tropical Santa Cruz', @nit = '20000008', @encargado_nombre = 'Dra. Silvia Huanca', @encargado_cargo = 'Propietaria', @email = 'info@vettropical8.com', @direccion = 'Av. Banzer #789, Santa Cruz', @telefono = '33456789';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Importadora de Medicamentos Veterinarios SRL', @nit = '20000009', @encargado_nombre = 'Juan Carlos Chipana', @encargado_cargo = 'Director Comercial', @email = 'importaciones@medvet9.bo', @direccion = 'Zona Franca, El Alto, La Paz', @telefono = '28123456';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria del Valle', @nit = '20000010', @encargado_nombre = 'Carmen Colque', @encargado_cargo = 'Administradora', @email = 'atencion@vetvalle10.com', @direccion = 'Av. Libertador #456, Tarija', @telefono = '46678901';

-- 11-20
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Cooperativa Ganadera Altiplano', @nit = '20000011', @encargado_nombre = 'Miguel Angel Mamani', @encargado_cargo = 'Presidente', @email = 'cooperativa@ganaderaltiplano11.bo', @direccion = 'Plaza Principal, Patacamaya, La Paz', @telefono = '22234567';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Pet Care Center Sucre', @nit = '20000012', @encargado_nombre = 'Ana Maria Vargas', @encargado_cargo = 'Gerente', @email = 'cuidados@petcaresucre12.com', @direccion = 'Calle España #234, Sucre', @telefono = '46445566';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Alimentos Balanceados del Oriente', @nit = '20000013', @encargado_nombre = 'Roberto Torrez', @encargado_cargo = 'Jefe de Producción', @email = 'ventas@alimentosoriente13.bo', @direccion = 'Parque Industrial, Santa Cruz', @telefono = '33789123';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Veterinaria Andina Potosí', @nit = '20000014', @encargado_nombre = 'Patricia Cruz', @encargado_cargo = 'Veterinaria Principal', @email = 'consultas@vetandina14.com', @direccion = 'Av. Universitaria #567, Potosí', @telefono = '26223344';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Hacienda Ganadera La Esperanza', @nit = '20000015', @encargado_nombre = 'Luis Fernando Mendoza', @encargado_cargo = 'Administrador', @email = 'hacienda@esperanza15.com.bo', @direccion = 'Provincia Warnes, Santa Cruz', @telefono = '33556677';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria Beni', @nit = '20000016', @encargado_nombre = 'Silvia Gutierrez', @encargado_cargo = 'Directora', @email = 'clinica@vetbeni16.bo', @direccion = 'Av. Mamoré #123, Trinidad, Beni', @telefono = '36778899';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Distribuidora de Insumos Pecuarios', @nit = '20000017', @encargado_nombre = 'Juan Huanca', @encargado_cargo = 'Gerente de Distribución', @email = 'distribuidora@pecuarios17.bo', @direccion = 'Av. Circunvalación #789, Cochabamba', @telefono = '44112233';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Investigación Veterinaria UMSA', @nit = '20000018', @encargado_nombre = 'Dra. Carmen Chipana', @encargado_cargo = 'Directora de Investigación', @email = 'investigacion@umsa18.edu.bo', @direccion = 'Campus Universitario, Cota Cota, La Paz', @telefono = '22345567';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Farmacia Veterinaria Central', @nit = '20000019', @encargado_nombre = 'Miguel Colque', @encargado_cargo = 'Farmacéutico Responsable', @email = 'farmacia@vetcentral19.com', @direccion = 'Calle Ayacucho #345, La Paz', @telefono = '22667788';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Servicios Veterinarios Móviles Bolivia', @nit = '20000020', @encargado_nombre = 'Ana Rosa Mamani', @encargado_cargo = 'Coordinadora de Servicios', @email = 'servicios@vetmovil20.bo', @direccion = 'Av. del Ejército #678, La Paz', @telefono = '22889900';

-- 21-30
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria Los Andes', @nit = '20000021', @encargado_nombre = 'Dr. Javier Quispe', @encargado_cargo = 'Director Médico', @email = 'info@clinicaandes21.bo', @direccion = 'Av. Arce #456, La Paz', @telefono = '22112233';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Pet Shop Animal Kingdom', @nit = '20000022', @encargado_nombre = 'Sofia Mamani', @encargado_cargo = 'Gerente', @email = 'ventas@animalkingdom22.com', @direccion = 'Calle Comercio #789, Santa Cruz', @telefono = '33445566';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Laboratorio Diagnóstico Veterinario', @nit = '20000023', @encargado_nombre = 'Dr. Carlos Condori', @encargado_cargo = 'Jefe de Laboratorio', @email = 'diagnostico@labvet23.bo', @direccion = 'Av. 6 de Agosto #234, Cochabamba', @telefono = '44778899';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Granja Porcina San Juan', @nit = '20000024', @encargado_nombre = 'Maria Flores', @encargado_cargo = 'Administradora', @email = 'granja@sanjuan24.com.bo', @direccion = 'Km 20 Carretera a Oruro', @telefono = '22334455';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Veterinaria Express', @nit = '20000025', @encargado_nombre = 'Dr. Pedro Apaza', @encargado_cargo = 'Propietario', @email = 'consultas@vetexpress25.bo', @direccion = 'Av. América #567, El Alto', @telefono = '28556677';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Reproducción Animal', @nit = '20000026', @encargado_nombre = 'Dra. Rosa Limachi', @encargado_cargo = 'Especialista', @email = 'reproduccion@centroanimal26.com', @direccion = 'Calle Bolívar #890, Tarija', @telefono = '46889900';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Distribuidora Farmacéutica Vet', @nit = '20000027', @encargado_nombre = 'Ing. Fernando Poma', @encargado_cargo = 'Gerente General', @email = 'distribuidora@farmavet27.bo', @direccion = 'Zona Industrial, Santa Cruz', @telefono = '33112233';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria Norte', @nit = '20000028', @encargado_nombre = 'Dra. Gloria Cruz', @encargado_cargo = 'Directora', @email = 'info@vetnorte28.com', @direccion = 'Av. Max Paredes #123, La Paz', @telefono = '22445566';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa Ganadera Boliviana', @nit = '20000029', @encargado_nombre = 'Lic. Raul Ticona', @encargado_cargo = 'Gerente', @email = 'empresa@ganadera29.bo', @direccion = 'Provincia Ichilo, Santa Cruz', @telefono = '33778899';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Hospital Veterinario Central', @nit = '20000030', @encargado_nombre = 'Dr. Marta Yujra', @encargado_cargo = 'Jefa de Cirugía', @email = 'hospital@vetcentral30.com', @direccion = 'Av. Villazón #456, Cochabamba', @telefono = '44334455';

-- 31-40
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Alimentos Premium para Mascotas', @nit = '20000031', @encargado_nombre = 'Daniel Choque', @encargado_cargo = 'Director Comercial', @email = 'ventas@premium31.bo', @direccion = 'Parque Industrial, El Alto', @telefono = '28667788';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria Sur', @nit = '20000032', @encargado_nombre = 'Dra. Elena Vargas', @encargado_cargo = 'Propietaria', @email = 'consultas@vetsur32.com', @direccion = 'Av. Costanera #789, Zona Sur, La Paz', @telefono = '22990011';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Cooperativa Lechera Valle Alto', @nit = '20000033', @encargado_nombre = 'Antonio Torrez', @encargado_cargo = 'Presidente', @email = 'cooperativa@valleyalto33.bo', @direccion = 'Quillacollo, Cochabamba', @telefono = '44556677';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Importadora de Equipos Veterinarios', @nit = '20000034', @encargado_nombre = 'Lucia Gutierrez', @encargado_cargo = 'Gerente de Importaciones', @email = 'equipos@importvet34.com', @direccion = 'Zona Franca, Santa Cruz', @telefono = '33223344';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Veterinaria 24 Horas', @nit = '20000035', @encargado_nombre = 'Dr. Francisco Mendoza', @encargado_cargo = 'Director', @email = 'emergencias@vet24h35.bo', @direccion = 'Av. Busch #234, Sopocachi, La Paz', @telefono = '22778899';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Capacitación Veterinaria', @nit = '20000036', @encargado_nombre = 'Dra. Isabel Huanca', @encargado_cargo = 'Coordinadora Académica', @email = 'capacitacion@centrovet36.edu.bo', @direccion = 'Av. Universitaria #567, Sucre', @telefono = '46445566';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Granja Avícola del Este', @nit = '20000037', @encargado_nombre = 'Ricardo Chipana', @encargado_cargo = 'Gerente de Producción', @email = 'avicola@este37.com.bo', @direccion = 'Provincia Warnes, Santa Cruz', @telefono = '33667788';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria Especializada', @nit = '20000038', @encargado_nombre = 'Dra. Angela Colque', @encargado_cargo = 'Especialista en Exóticos', @email = 'especializada@vetesp38.com', @direccion = 'Av. Arce #890, San Jorge, La Paz', @telefono = '22112244';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Distribuidora Nacional de Vacunas', @nit = '20000039', @encargado_nombre = 'Gonzalo Quispe', @encargado_cargo = 'Gerente de Ventas', @email = 'vacunas@distnacional39.bo', @direccion = 'Av. América #123, El Alto', @telefono = '28445566';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Salud Animal', @nit = '20000040', @encargado_nombre = 'Teresa Mamani', @encargado_cargo = 'Administradora', @email = 'salud@centroanimal40.com', @direccion = 'Calle Sagarnaga #456, Potosí', @telefono = '26778899';

-- 41-50
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Laboratorio de Análisis Clínicos Veterinarios', @nit = '20000041', @encargado_nombre = 'Dr. Hernan Condori', @encargado_cargo = 'Director Técnico', @email = 'analisis@labclinico41.bo', @direccion = 'Av. 16 de Julio #789, La Paz', @telefono = '22334466';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Pet Spa y Estética Canina', @nit = '20000042', @encargado_nombre = 'Roxana Flores', @encargado_cargo = 'Propietaria', @email = 'spa@petspa42.com', @direccion = 'Calle Rosendo Gutierrez #234, Cochabamba', @telefono = '44556688';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Transporte de Ganado', @nit = '20000043', @encargado_nombre = 'Alberto Apaza', @encargado_cargo = 'Gerente Operativo', @email = 'transporte@ganado43.bo', @direccion = 'Km 5 Carretera a Viacha', @telefono = '22667799';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria de Pequeños Animales', @nit = '20000044', @encargado_nombre = 'Dra. Margarita Limachi', @encargado_cargo = 'Especialista', @email = 'pequenos@vetpeq44.com', @direccion = 'Av. Ballivián #567, Calacoto, La Paz', @telefono = '22889911';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Distribuidora de Suplementos Nutricionales', @nit = '20000045', @encargado_nombre = 'Victor Poma', @encargado_cargo = 'Director Comercial', @email = 'suplementos@nutri45.bo', @direccion = 'Zona Industrial, Santa Cruz', @telefono = '33445577';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Rehabilitación Animal', @nit = '20000046', @encargado_nombre = 'Blanca Cruz', @encargado_cargo = 'Fisioterapeuta Veterinaria', @email = 'rehab@centrorehab46.com', @direccion = 'Av. Costanera #890, Tarija', @telefono = '46223344';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Inseminación Artificial', @nit = '20000047', @encargado_nombre = 'Alfredo Ticona', @encargado_cargo = 'Técnico Especializado', @email = 'inseminacion@artificial47.bo', @direccion = 'Provincia Cordillera, Santa Cruz', @telefono = '33778800';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Veterinaria Rural del Altiplano', @nit = '20000048', @encargado_nombre = 'Dra. Veronica Yujra', @encargado_cargo = 'Coordinadora Rural', @email = 'rural@altiplano48.com', @direccion = 'Plaza Principal, Patacamaya', @telefono = '22556699';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Diagnóstico por Imágenes', @nit = '20000049', @encargado_nombre = 'Dr. Julio Choque', @encargado_cargo = 'Radiólogo Veterinario', @email = 'imagenes@diagnostico49.bo', @direccion = 'Av. Arce #123, Sopocachi, La Paz', @telefono = '22445577';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Granja Experimental de la Universidad', @nit = '20000050', @encargado_nombre = 'Monica Vargas', @encargado_cargo = 'Coordinadora de Investigación', @email = 'experimental@univet50.edu.bo', @direccion = 'Campus Universitario, Cochabamba', @telefono = '44667788';

-- 51-60
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria de Emergencias', @nit = '20000051', @encargado_nombre = 'Dr. Sergio Torrez', @encargado_cargo = 'Jefe de Emergencias', @email = 'emergencias@vetemerge51.com', @direccion = 'Av. 6 de Agosto #456, Santa Cruz', @telefono = '33112244';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Productos Orgánicos para Mascotas', @nit = '20000052', @encargado_nombre = 'Graciela Gutierrez', @encargado_cargo = 'Gerente de Producción', @email = 'organicos@products52.bo', @direccion = 'Valle de Sacaba, Cochabamba', @telefono = '44335577';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Adopción de Animales', @nit = '20000053', @encargado_nombre = 'Jaime Mendoza', @encargado_cargo = 'Coordinador', @email = 'adopcion@centro53.org', @direccion = 'Av. América #789, Miraflores, La Paz', @telefono = '22668899';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Laboratorio de Genética Animal', @nit = '20000054', @encargado_nombre = 'Dra. Adriana Huanca', @encargado_cargo = 'Genetista', @email = 'genetica@labgen54.bo', @direccion = 'Parque Tecnológico, Santa Cruz', @telefono = '33557788';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Veterinaria Especializada en Equinos', @nit = '20000055', @encargado_nombre = 'Dr. Marcelo Chipana', @encargado_cargo = 'Especialista Equino', @email = 'equinos@vetequino55.com', @direccion = 'Club Hípico, La Paz', @telefono = '22779900';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Medicina Preventiva Animal', @nit = '20000056', @encargado_nombre = 'Claudia Colque', @encargado_cargo = 'Epidemióloga Veterinaria', @email = 'preventiva@medprev56.bo', @direccion = 'Av. Heroínas #234, Cochabamba', @telefono = '44446677';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Biotecnología Veterinaria', @nit = '20000057', @encargado_nombre = 'Oscar Quispe', @encargado_cargo = 'Director de I+D', @email = 'biotech@vetbio57.com', @direccion = 'Zona Franca, El Alto', @telefono = '28778899';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria Móvil', @nit = '20000058', @encargado_nombre = 'Paola Mamani', @encargado_cargo = 'Coordinadora', @email = 'movil@vetmovil58.bo', @direccion = 'Av. del Ejército #567, La Paz', @telefono = '22334455';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Entrenamiento Canino', @nit = '20000059', @encargado_nombre = 'Ramiro Condori', @encargado_cargo = 'Entrenador Profesional', @email = 'entrenamiento@canino59.com', @direccion = 'Zona Sur, La Paz', @telefono = '22556677';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Control de Plagas Veterinario', @nit = '20000060', @encargado_nombre = 'Ximena Flores', @encargado_cargo = 'Técnica en Control', @email = 'plagas@control60.bo', @direccion = 'Av. Circunvalación #890, Santa Cruz', @telefono = '33668899';

-- 61-70
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Hospital Veterinario de Especialidades', @nit = '20000061', @encargado_nombre = 'Dr. Diego Apaza', @encargado_cargo = 'Director Médico', @email = 'hospital@vetesp61.com', @direccion = 'Av. Montes #123, Potosí', @telefono = '26445566';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Alimentos Concentrados', @nit = '20000062', @encargado_nombre = 'Carla Limachi', @encargado_cargo = 'Nutricionista Animal', @email = 'concentrados@alimentos62.bo', @direccion = 'Parque Industrial, Cochabamba', @telefono = '44778800';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Cirugía Veterinaria Avanzada', @nit = '20000063', @encargado_nombre = 'Dr. Rolando Poma', @encargado_cargo = 'Cirujano Especialista', @email = 'cirugia@avanzada63.com', @direccion = 'Av. Arce #456, San Jorge, La Paz', @telefono = '22889911';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Laboratorio de Microbiología Veterinaria', @nit = '20000064', @encargado_nombre = 'Beatriz Cruz', @encargado_cargo = 'Microbióloga', @email = 'micro@labmicro64.bo', @direccion = 'Av. Universidad #789, Tarija', @telefono = '46223355';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria de Animales Exóticos', @nit = '20000065', @encargado_nombre = 'Dr. Gustavo Ticona', @encargado_cargo = 'Especialista en Exóticos', @email = 'exoticos@vetexotic65.com', @direccion = 'Calle Comercio #234, Santa Cruz', @telefono = '33445566';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Reproducción Asistida Animal', @nit = '20000066', @encargado_nombre = 'Lorena Yujra', @encargado_cargo = 'Embrióloga', @email = 'reproduccion@asistida66.bo', @direccion = 'Zona Experimental, Cochabamba', @telefono = '44667788';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Servicios Veterinarios Integrales', @nit = '20000067', @encargado_nombre = 'Ernesto Choque', @encargado_cargo = 'Gerente General', @email = 'integrales@servicios67.com', @direccion = 'Av. América #567, El Alto', @telefono = '28556677';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Terapias Alternativas para Animales', @nit = '20000068', @encargado_nombre = 'Vanessa Vargas', @encargado_cargo = 'Terapeuta Holística', @email = 'terapias@alternativas68.bo', @direccion = 'Av. Ballivián #890, Calacoto, La Paz', @telefono = '22778899';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Distribuidora de Material Quirúrgico Veterinario', @nit = '20000069', @encargado_nombre = 'Mario Torrez', @encargado_cargo = 'Especialista en Equipos', @email = 'quirurgico@material69.com', @direccion = 'Zona Industrial, Santa Cruz', @telefono = '33112233';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria Comunitaria', @nit = '20000070', @encargado_nombre = 'Natalia Gutierrez', @encargado_cargo = 'Coordinadora Social', @email = 'comunitaria@vetcom70.org', @direccion = 'Villa El Carmen, El Alto', @telefono = '28445577';

-- 71-80
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Investigación en Enfermedades Animales', @nit = '20000071', @encargado_nombre = 'Dr. Hector Mendoza', @encargado_cargo = 'Investigador Principal', @email = 'investigacion@enfermedades71.bo', @direccion = 'Universidad Mayor, La Paz', @telefono = '22667788';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Cremación de Mascotas', @nit = '20000072', @encargado_nombre = 'Andrea Huanca', @encargado_cargo = 'Directora de Servicios', @email = 'cremacion@mascotas72.com', @direccion = 'Periférico, Cochabamba', @telefono = '44889900';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Oncología Veterinaria', @nit = '20000073', @encargado_nombre = 'Dr. Gabriel Chipana', @encargado_cargo = 'Oncólogo Veterinario', @email = 'oncologia@vetoncol73.com', @direccion = 'Av. Arce #123, Sopocachi, La Paz', @telefono = '22334466';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Laboratorio de Patología Veterinaria', @nit = '20000074', @encargado_nombre = 'Daniela Colque', @encargado_cargo = 'Patóloga', @email = 'patologia@labpatol74.bo', @direccion = 'Hospital Veterinario, Santa Cruz', @telefono = '33556677';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria Geriátrica', @nit = '20000075', @encargado_nombre = 'Dr. Felipe Quispe', @encargado_cargo = 'Geriatra Veterinario', @email = 'geriatrica@vetgeriat75.com', @direccion = 'Av. 6 de Agosto #456, Tarija', @telefono = '46778899';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Fisioterapia Animal', @nit = '20000076', @encargado_nombre = 'Valeria Mamani', @encargado_cargo = 'Fisioterapeuta', @email = 'fisio@terapia76.bo', @direccion = 'Calle Sucre #789, Potosí', @telefono = '26112233';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Seguros para Mascotas', @nit = '20000077', @encargado_nombre = 'Rodrigo Condori', @encargado_cargo = 'Gerente de Seguros', @email = 'seguros@mascotas77.com', @direccion = 'Av. 16 de Julio #234, La Paz', @telefono = '22445566';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Nutrición Animal Especializada', @nit = '20000078', @encargado_nombre = 'Alejandra Flores', @encargado_cargo = 'Nutricionista', @email = 'nutricion@especializada78.bo', @direccion = 'Valle de Tiquipaya, Cochabamba', @telefono = '44667788';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria de Comportamiento Animal', @nit = '20000079', @encargado_nombre = 'Dr. Andres Apaza', @encargado_cargo = 'Etólogo', @email = 'comportamiento@etologia79.com', @direccion = 'Av. Costanera #567, Santa Cruz', @telefono = '33889900';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Acuicultura Veterinaria', @nit = '20000080', @encargado_nombre = 'Karina Limachi', @encargado_cargo = 'Especialista en Peces', @email = 'acuicultura@vetaqua80.bo', @direccion = 'Lago Titicaca, Copacabana', @telefono = '22778800';

-- 81-90
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Hospital Veterinario Universitario', @nit = '20000081', @encargado_nombre = 'Dr. Cristian Poma', @encargado_cargo = 'Director Académico', @email = 'universitario@vetuni81.edu.bo', @direccion = 'Campus Central, La Paz', @telefono = '22556677';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Biotecnología Reproductiva', @nit = '20000082', @encargado_nombre = 'Soledad Cruz', @encargado_cargo = 'Biotecnóloga', @email = 'biorepro@biotech82.com', @direccion = 'Parque Tecnológico, Cochabamba', @telefono = '44445566';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Medicina Deportiva Canina', @nit = '20000083', @encargado_nombre = 'Mauricio Ticona', @encargado_cargo = 'Médico Deportivo', @email = 'deportiva@canideport83.bo', @direccion = 'Club Canino, Santa Cruz', @telefono = '33223344';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Laboratorio de Análisis de Alimentos', @nit = '20000084', @encargado_nombre = 'Fabiola Yujra', @encargado_cargo = 'Analista de Calidad', @email = 'alimentos@labanalisis84.com', @direccion = 'Zona Industrial, El Alto', @telefono = '28667788';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria Pediátrica', @nit = '20000085', @encargado_nombre = 'Dr. Ivan Choque', @encargado_cargo = 'Pediatra Veterinario', @email = 'pediatrica@vetped85.com', @direccion = 'Av. América #890, Miraflores, La Paz', @telefono = '22889911';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Donación de Sangre Animal', @nit = '20000086', @encargado_nombre = 'Pamela Vargas', @encargado_cargo = 'Hematóloga', @email = 'sangre@donacion86.bo', @direccion = 'Hospital Central, Cochabamba', @telefono = '44112233';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Productos Farmacéuticos Veterinarios', @nit = '20000087', @encargado_nombre = 'Aldo Torrez', @encargado_cargo = 'Director de Producción', @email = 'farma@productos87.com', @direccion = 'Zona Franca, Santa Cruz', @telefono = '33445566';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Rehabilitación de Fauna Silvestre', @nit = '20000088', @encargado_nombre = 'Miriam Gutierrez', @encargado_cargo = 'Bióloga', @email = 'fauna@silvestre88.org', @direccion = 'Parque Nacional, Madidi', @telefono = '22667788';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria de Medicina Interna', @nit = '20000089', @encargado_nombre = 'Dr. Armando Mendoza', @encargado_cargo = 'Internista', @email = 'interna@medicina89.com', @direccion = 'Av. Ballivián #123, Tarija', @telefono = '46889900';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Telemedicina Veterinaria', @nit = '20000090', @encargado_nombre = 'Jimena Huanca', @encargado_cargo = 'Coordinadora Tecnológica', @email = 'tele@medicina90.bo', @direccion = 'Centro Tecnológico, La Paz', @telefono = '22112244';

-- 91-100
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Logística Veterinaria', @nit = '20000091', @encargado_nombre = 'Esteban Chipana', @encargado_cargo = 'Gerente Logístico', @email = 'logistica@vetlog91.com', @direccion = 'Centro de Distribución, El Alto', @telefono = '28334455';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Investigación en Zoonosis', @nit = '20000092', @encargado_nombre = 'Rebeca Colque', @encargado_cargo = 'Epidemióloga', @email = 'zoonosis@investigacion92.bo', @direccion = 'Instituto Nacional, Cochabamba', @telefono = '44556677';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria de Urgencias Nocturnas', @nit = '20000093', @encargado_nombre = 'Dr. Ignacio Quispe', @encargado_cargo = 'Jefe de Urgencias', @email = 'nocturnas@urgencias93.com', @direccion = 'Av. 6 de Agosto #456, Santa Cruz', @telefono = '33778899';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Capacitación en Bienestar Animal', @nit = '20000094', @encargado_nombre = 'Sonia Mamani', @encargado_cargo = 'Especialista en Bienestar', @email = 'bienestar@capacitacion94.org', @direccion = 'Universidad Católica, La Paz', @telefono = '22445577';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Consultoría Veterinaria', @nit = '20000095', @encargado_nombre = 'Nestor Condori', @encargado_cargo = 'Consultor Senior', @email = 'consultoria@vetcons95.com', @direccion = 'Centro Empresarial, Santa Cruz', @telefono = '33112244';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Medicina Forense Veterinaria', @nit = '20000096', @encargado_nombre = 'Lilian Flores', @encargado_cargo = 'Médico Forense', @email = 'forense@medicina96.bo', @direccion = 'Instituto Forense, Potosí', @telefono = '26223355';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Empresa de Gestión de Residuos Veterinarios', @nit = '20000097', @encargado_nombre = 'Walter Apaza', @encargado_cargo = 'Especialista Ambiental', @email = 'residuos@gestion97.com', @direccion = 'Planta de Tratamiento, Cochabamba', @telefono = '44667799';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro de Educación Veterinaria Continua', @nit = '20000098', @encargado_nombre = 'Gladys Limachi', @encargado_cargo = 'Coordinadora Educativa', @email = 'educacion@continua98.edu.bo', @direccion = 'Colegio Veterinario, La Paz', @telefono = '22889922';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Clínica Veterinaria de Medicina Tradicional', @nit = '20000099', @encargado_nombre = 'Dr. Gerardo Poma', @encargado_cargo = 'Especialista en Medicina Tradicional', @email = 'tradicional@vetmed99.bo', @direccion = 'Centro Ancestral, Uyuni', @telefono = '26445566';
EXEC SP02_CreateOrUpdatePJuridica @razon_social = 'Centro Internacional de Salud Animal', @nit = '20000100', @encargado_nombre = 'Carolina Cruz', @encargado_cargo = 'Directora Internacional', @email = 'internacional@salud100.org', @direccion = 'Zona Diplomática, La Paz', @telefono = '22778811';

PRINT 'Personas Jurídicas insertadas correctamente.'

-- =============================================
-- VERIFICACIÓN DE DATOS INSERTADOS
-- =============================================

PRINT 'Verificando datos insertados...'

-- Contar personas físicas
DECLARE @CountFisicas INT
SELECT @CountFisicas = COUNT(*)
FROM persona p 
INNER JOIN persona_fisica pf ON p.id = pf.id
WHERE p.activo = 1

PRINT 'Total de Personas Físicas insertadas: ' + CAST(@CountFisicas AS VARCHAR(10))

-- Contar personas jurídicas
DECLARE @CountJuridicas INT
SELECT @CountJuridicas = COUNT(*)
FROM persona p 
INNER JOIN persona_juridica pj ON p.id = pj.id
WHERE p.activo = 1

PRINT 'Total de Personas Jurídicas insertadas: ' + CAST(@CountJuridicas AS VARCHAR(10))

-- Mostrar resumen
SELECT 
    'Física' AS TipoPersona,
    COUNT(*) AS Cantidad
FROM persona p 
INNER JOIN persona_fisica pf ON p.id = pf.id
WHERE p.activo = 1

UNION ALL

SELECT 
    'Jurídica' AS TipoPersona,
    COUNT(*) AS Cantidad
FROM persona p 
INNER JOIN persona_juridica pj ON p.id = pj.id
WHERE p.activo = 1

PRINT '=========================================='
PRINT 'SEEDER COMPLETADO EXITOSAMENTE'
PRINT '=========================================='
PRINT 'Se han insertado 100 Personas Físicas'
PRINT 'Se han insertado 100 Personas Jurídicas'
PRINT 'Total: 200 personas registradas'
PRINT 'Usando procedimientos almacenados:'
PRINT '- SP01_CreateOrUpdatePFisica'
PRINT '- SP02_CreateOrUpdatePJuridica'
PRINT 'Fecha de ejecución: ' + CONVERT(VARCHAR(20), GETDATE(), 120)
PRINT '=========================================='

-- =============================================
-- FIN DEL SCRIPT
-- =============================================
