using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BlazorEcommerce.Server.Modelos;

namespace BlazorEcommerce.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class UsuarioController : ControllerBase
    {
        private readonly DbtiendaBlazorContext _context;

        public UsuarioController(DbtiendaBlazorContext context)
        {
            _context = context;
        }

        // Endpoints existentes...

        [HttpGet("imagen/{id}")]
        public async Task<IActionResult> ObtenerImagenUsuario(int id)
        {
            // NOTA: Ajusta "Persona" al nombre real de tu tabla de usuarios
            // y "RutaImagen" al nombre del campo donde guardas la ruta de la imagen
            var usuario = await _context.Personas
                .Where(u => u.IdPersona == id)
                .Select(u => new { u.Imagen })
                .FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound(new { Mensaje = "Usuario no encontrado" });

            return Ok(new { RutaImagen = usuario.Imagen ?? "/images/default-profile.png" });
        }

        [HttpGet("imagen/email/{email}")]
        public async Task<IActionResult> ObtenerImagenUsuarioPorEmail(string email)
        {
            // NOTA: Ajusta "Persona" al nombre real de tu tabla de usuarios
            // y "Correo" y "RutaImagen" a los nombres reales de los campos
            var usuario = await _context.Personas
                .Where(u => u.Correo == email)
                .Select(u => new { u.Imagen})
                .FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound(new { Mensaje = "Usuario no encontrado" });

            return Ok(new { RutaImagen = usuario.Imagen ?? "/images/default-profile.png" });
        }
    }
}