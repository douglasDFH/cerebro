using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlazorEcommerce.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArchivoController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ArchivoController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("guardar")]
        public async Task<IActionResult> GuardarArchivo([FromBody] GuardarArchivoRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Contenido) || string.IsNullOrEmpty(request.Ruta))
                {
                    return BadRequest(new { Exito = false, Mensaje = "La ruta y el contenido son obligatorios" });
                }

                // Convertir Base64 a bytes (eliminar prefijo "data:image/png;base64," si existe)
                string contenidoBase64 = request.Contenido;
                if (contenidoBase64.Contains(","))
                {
                    contenidoBase64 = contenidoBase64.Split(',')[1];
                }

                byte[] bytes = Convert.FromBase64String(contenidoBase64);

                // Construir la ruta absoluta en el servidor
                string rutaCompleta = Path.Combine(_env.WebRootPath, request.Ruta.TrimStart('/'));

                // Asegurarse de que la ruta existe
                string directorio = Path.GetDirectoryName(rutaCompleta);
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                // Guardar el archivo
                await System.IO.File.WriteAllBytesAsync(rutaCompleta, bytes);

                return Ok(new { Exito = true, Ruta = request.Ruta });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }
    }

    public class GuardarArchivoRequest
    {
        public string Ruta { get; set; }
        public string Contenido { get; set; }
    }
}