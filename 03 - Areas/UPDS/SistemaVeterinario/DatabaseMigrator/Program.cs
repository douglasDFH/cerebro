using System;
using System.IO;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Iniciando ejecución de migraciones y seeders...");

        // Obtener la ruta base del repositorio (subir desde el directorio de ejecución hasta encontrar CapaDatos)
        string currentDir = Directory.GetCurrentDirectory();
        string repoRoot = currentDir;
        
        // Subir directorios hasta encontrar la carpeta CapaDatos
        while (repoRoot != null && !Directory.Exists(Path.Combine(repoRoot, "CapaDatos")))
        {
            var parent = Directory.GetParent(repoRoot);
            repoRoot = parent?.FullName;
        }
        
        if (repoRoot == null || !Directory.Exists(Path.Combine(repoRoot, "CapaDatos")))
        {
            Console.WriteLine("No se pudo encontrar la carpeta CapaDatos. Verifique la estructura del proyecto.");
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
            return;
        }
        
        string basePath = Path.Combine(repoRoot, "CapaDatos", "Sql");
        string migrationsPath = Path.Combine(basePath, "migrations");
        string seedersPath = Path.Combine(basePath, "seeders");

        Console.WriteLine($"Buscando migraciones en: {migrationsPath}");
        Console.WriteLine($"Buscando seeders en: {seedersPath}");

        // Cadena de conexión - ajusta con tus credenciales 
        string connectionStringSistemaVeterinario = CapaDatos.DbConnection.GetConnectionString();
        string connectionStringMaster = connectionStringSistemaVeterinario.Replace("Initial Catalog=Sistema_Veterinario;", "Initial Catalog=master;");

        try
        {
            // Borramos la base de datos Sistema_Veterinario si existe y la creamos nueva
            using (var connection = new SqlConnection(connectionStringMaster))
            {
                connection.Open();
                
                // Eliminar la base de datos si existe
                var dropDbCommand = new SqlCommand("IF DB_ID('Sistema_Veterinario') IS NOT NULL DROP DATABASE Sistema_Veterinario;", connection);
                dropDbCommand.ExecuteNonQuery();
                Console.WriteLine("Base de datos 'Sistema_Veterinario' eliminada si existía.");
                
                // Crear la base de datos
                var createDbCommand = new SqlCommand("CREATE DATABASE Sistema_Veterinario;", connection);
                createDbCommand.ExecuteNonQuery();
                Console.WriteLine("Base de datos 'Sistema_Veterinario' creada exitosamente.");
            }

            // Ejecutar migraciones primero
            Console.WriteLine("Ejecutando migraciones...");
            ExecuteSqlFiles(connectionStringSistemaVeterinario, migrationsPath);

            // Luego ejecutar seeders
            Console.WriteLine("Ejecutando seeders...");
            ExecuteSqlFiles(connectionStringSistemaVeterinario, seedersPath);

            Console.WriteLine("¡Proceso completado exitosamente!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void ExecuteSqlFiles(string connectionString, string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Directorio no encontrado: {directoryPath}");
            return;
        }

        // Obtener archivos SQL ordenados por nombre (para mantener el orden)
        var sqlFiles = Directory.GetFiles(directoryPath, "*.sql")
                               .OrderBy(f => f)
                               .ToList();

        if (sqlFiles.Count == 0)
        {
            Console.WriteLine($"No se encontraron archivos SQL en: {directoryPath}");
            return;
        }

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            foreach (var filePath in sqlFiles)
            {
                Console.WriteLine($"Ejecutando: {Path.GetFileName(filePath)}");

                try
                {
                    string sqlContent = File.ReadAllText(filePath);

                    // Dividir el script en comandos individuales usando "GO"
                    var commands = sqlContent.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var command in commands.Where(c => !string.IsNullOrWhiteSpace(c)))
                    {
                        string trimmedCommand = command.Trim();
                        if (!string.IsNullOrEmpty(trimmedCommand))
                        {
                            using (var sqlCommand = new SqlCommand(trimmedCommand, connection))
                            {
                                sqlCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    Console.WriteLine($"✓ {Path.GetFileName(filePath)} ejecutado correctamente");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ Error en {Path.GetFileName(filePath)}: {ex.Message}");
                    // Decide si quieres continuar con los siguientes archivos o detenerte
                    // throw; // Descomenta esta línea si quieres que falle en el primer error
                }
            }
        }
    }
}