using BlazorEcommerce.Server.Servicios.PersonaSV;
using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaServicio _personaServicio;
        public PersonaController(IPersonaServicio personaServicio)
        {
            _personaServicio = personaServicio;
        }

        // Modificado para usar una ruta más simple y robusta
        [HttpGet("Lista")]
        public async Task<IActionResult> Lista([FromQuery] string? rol = "", [FromQuery] string? valor = "")
        {
            rol = rol ?? "";
            valor = valor ?? "";

            return Ok(await _personaServicio.Lista(rol, valor));
        }

        // Mantener también el endpoint original por compatibilidad
        [HttpGet("Lista/{Rol}/{Valor?}")]
        public async Task<IActionResult> ListaConParams(string Rol, string? Valor = "")
        {
            // Si el rol es "NA", lo convertimos a cadena vacía para obtener todos los roles
            if (Rol == "NA") Rol = "";

            // Si Valor es null, asignamos cadena vacía
            Valor = Valor ?? "";

            return Ok(await _personaServicio.Lista(Rol, Valor));
        }

        [HttpGet("Obtener/{Id:int}")]
        public async Task<IActionResult> Obtener(int Id)
        {
            return Ok(await _personaServicio.Obtener(Id));
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] PersonaDTO modelo)
        {
            return Ok(await _personaServicio.Crear(modelo));
        }

        [HttpPost("Autorizacion")]
        public async Task<IActionResult> Autorizacion([FromBody] LoginDTO modelo)
        {
            return Ok(await _personaServicio.Autorizacion(modelo));
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> Editar([FromBody] PersonaDTO modelo)
        {
            return Ok(await _personaServicio.Editar(modelo));
        }

        [HttpDelete("Eliminar/{Id:int}")]
        public async Task<IActionResult> Eliminar(int Id)
        {
            return Ok(await _personaServicio.Eliminar(Id));
        }
        [HttpPost("AutorizacionFacial")]
        public async Task<IActionResult> AutorizacionFacial([FromBody] FacialLoginDTO modelo)
        {
            return Ok(await _personaServicio.AutorizacionFacial(modelo));
        }
    }
}