
INSERT INTO
    personal (
        nombre,
        apellido,
        email,
        usuario,
        contrasena,
        telefono,
        rol,
        activo,
        creado_por
    )
VALUES
    (
        'Administrador',
        'del Sistema',
        'admin@veterinaria.com',
        'admin',
        'admin123',
        '0000000000',
        'Administrador',
        1,
        'Sistema'
    );


GO PRINT 'Base de datos Sistema_Veterinario creada exitosamente con usuario administrador por defecto';

PRINT 'Usuario: admin';

PRINT 'Contrasena: admin123';

PRINT '';