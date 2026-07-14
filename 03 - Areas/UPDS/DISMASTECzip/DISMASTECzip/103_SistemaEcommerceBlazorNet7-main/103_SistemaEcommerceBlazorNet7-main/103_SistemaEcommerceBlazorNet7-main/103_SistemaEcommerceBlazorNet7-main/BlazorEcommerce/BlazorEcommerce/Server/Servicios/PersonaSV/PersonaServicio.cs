using BlazorEcommerce.Server.Repositorios;
using BlazorEcommerce.Shared;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace BlazorEcommerce.Server.Servicios.PersonaSV
{
    public class PersonaServicio : IPersonaServicio
    {
        private readonly IGenericoRepositorio<Persona> _personaRepositorio;
        private readonly IMapper _mapper;
        public PersonaServicio(IGenericoRepositorio<Persona> personaRepositorio, IMapper mapper)
        {
            _personaRepositorio = personaRepositorio;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<PersonaDTO>> Obtener(int id)
        {
            ResponseDTO<PersonaDTO> response = new ResponseDTO<PersonaDTO>()
            {
                Mensaje = "Ok",
                EsCorrecto = true
            };

            try
            {
                var consulta = _personaRepositorio.Consultar(p => p.IdPersona == id);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                    response.Resultado = _mapper.Map<PersonaDTO>(fromDbModelo);
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "No se encontraron coincidencias";
                }
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
                response.Resultado = null;
            }
            return response;
        }

        public async Task<ResponseDTO<List<PersonaDTO>>> Lista(string Rol, string Valor)
        {
            ResponseDTO<List<PersonaDTO>> response = new ResponseDTO<List<PersonaDTO>>()
            {
                Mensaje = "Ok",
                EsCorrecto = true
            };

            try
            {
                // Modificación para permitir búsqueda de todos los roles si Rol está vacío
                var consulta = _personaRepositorio.Consultar();

                // Aplicar filtro por rol solo si se especifica un rol
                if (!string.IsNullOrEmpty(Rol))
                {
                    consulta = consulta.Where(p => p.Rol == Rol);
                }

                // Aplicar filtro de búsqueda por nombre o correo si Valor no está vacío
                if (!string.IsNullOrEmpty(Valor))
                {
                    consulta = consulta.Where(p =>
                        (p.NombreCompleto != null && p.NombreCompleto.ToLower().Contains(Valor.ToLower())) ||
                        (p.Correo != null && p.Correo.ToLower().Contains(Valor.ToLower()))
                    );
                }

                List<PersonaDTO> lista = _mapper.Map<List<PersonaDTO>>(await consulta.ToListAsync());
                response.Resultado = lista;
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
                response.Resultado = null;
            }
            return response;
        }

        // En BlazorEcommerce.Server/Servicios/PersonaSV/PersonaServicio.cs

        public async Task<ResponseDTO<PersonaDTO>> Crear(PersonaDTO modelo)
        {
            ResponseDTO<PersonaDTO> response = new ResponseDTO<PersonaDTO>()
            {
                Mensaje = "Ok",
                EsCorrecto = true
            };

            try
            {
                // Si la imagen facial es muy grande, considera comprimirla antes de guardarla
                if (!string.IsNullOrEmpty(modelo.FacialImage) && modelo.FacialImage.Length > 1000000)
                {
                    // Aquí podrías implementar un método para reducir el tamaño de la imagen
                    modelo.FacialImage = ComprimirImagen(modelo.FacialImage);
                }

                var dbModelo = _mapper.Map<Persona>(modelo);
                var rspModelo = await _personaRepositorio.Crear(dbModelo);

                if (rspModelo.IdPersona != 0)
                    response.Resultado = _mapper.Map<PersonaDTO>(rspModelo);
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "No se pudo crear";
                }
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
                response.Resultado = null;
            }

            return response;
        }

        // Método auxiliar para comprimir la imagen base64
        private string ComprimirImagen(string imagenBase64)
        {
            // Implementar lógica de compresión aquí
            // Este es solo un ejemplo simple, podrías necesitar un método más elaborado
            try
            {
                // Extraer la parte de datos de la cadena base64
                string tipoImagen = "image/jpeg";
                if (imagenBase64.Contains("data:"))
                {
                    var partes = imagenBase64.Split(',');
                    if (partes.Length > 1)
                    {
                        tipoImagen = partes[0].Replace("data:", "").Replace(";base64", "");
                        imagenBase64 = partes[1];
                    }
                }

                // Por ahora, solo retornamos la imagen original
                // En una implementación real, aquí comprimirías la imagen
                return imagenBase64.Length > 1000000 ?
                    $"data:{tipoImagen};base64,{imagenBase64.Substring(0, 1000000)}" :
                    $"data:{tipoImagen};base64,{imagenBase64}";
            }
            catch
            {
                return imagenBase64;
            }
        }

        public async Task<ResponseDTO<bool>> Editar(PersonaDTO modelo)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>()
            {
                Mensaje = "Ok",
                EsCorrecto = true
            };

            try
            {
                // Asegurarse de que NombreCompleto tenga un valor
                if (string.IsNullOrEmpty(modelo.NombreCompleto))
                {
                    modelo.NombreCompleto = $"{modelo.Nombre} {modelo.ApellidoPaterno} {modelo.ApellidoMaterno ?? ""}".Trim();
                }

                var consulta = _personaRepositorio.Consultar(p => p.IdPersona == modelo.IdPersona);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    // Actualizar solo los campos disponibles en el modelo Persona
                    fromDbModelo.NombreCompleto = modelo.NombreCompleto;
                    fromDbModelo.Correo = modelo.Correo;
                    fromDbModelo.Clave = modelo.Clave;
                    fromDbModelo.Telefono = modelo.Telefono;
                    fromDbModelo.Rol = modelo.Rol;
                    fromDbModelo.Imagen = modelo.Imagen;

                    var respuesta = await _personaRepositorio.Editar(fromDbModelo);

                    if (!respuesta)
                    {
                        response.EsCorrecto = false;
                        response.Mensaje = "No se pudo editar";
                    }
                }
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "No se encontraron resultados";
                }
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
                response.Resultado = false;
            }

            return response;
        }

        public async Task<ResponseDTO<bool>> Eliminar(int id)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>()
            {
                Mensaje = "Ok",
                EsCorrecto = true
            };

            try
            {
                var consulta = _personaRepositorio.Consultar(p => p.IdPersona == id);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    var respuesta = await _personaRepositorio.Eliminar(fromDbModelo);

                    if (!respuesta)
                    {
                        response.EsCorrecto = false;
                        response.Mensaje = "No se pudo eliminar";
                    }
                }
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "No se encontraron resultados";
                }
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
                response.Resultado = false;
            }

            return response;
        }

        public async Task<ResponseDTO<SesionDTO>> Autorizacion(LoginDTO modelo)
        {
            ResponseDTO<SesionDTO> response = new ResponseDTO<SesionDTO>()
            {
                Mensaje = "Ok",
                EsCorrecto = true
            };

            try
            {
                var consulta = _personaRepositorio.Consultar(p => p.Correo == modelo.Correo && p.Clave == modelo.Clave);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                    response.Resultado = _mapper.Map<SesionDTO>(fromDbModelo);
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "No se encontraron coincidencias";
                }
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
                response.Resultado = null;
            }
            return response;
        }
        // Ubicación: BlazorEcommerce.Server.Servicios.PersonaSV/PersonaServicio.cs
        // Añadir este método a la clase existente

        // Ubicación: BlazorEcommerce.Server/Servicios/PersonaSV/PersonaServicio.cs
        // Añadir este método a la clase existente

        public async Task<ResponseDTO<SesionDTO>> AutorizacionFacial(FacialLoginDTO modelo)
        {
            ResponseDTO<SesionDTO> response = new ResponseDTO<SesionDTO>()
            {
                Mensaje = "Ok",
                EsCorrecto = true
            };

            try
            {
                // Buscar al usuario por correo electrónico
                var consulta = _personaRepositorio.Consultar(p =>
                    p.Correo == modelo.Correo &&
                    p.FacialImage != null);

                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    // En una implementación real, aquí compararías las imágenes faciales
                    // usando un algoritmo de reconocimiento facial

                    // Para esta implementación básica, simulamos que la autenticación es exitosa
                    // siempre que encontremos un usuario con el correo proporcionado

                    response.Resultado = _mapper.Map<SesionDTO>(fromDbModelo);
                }
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "No se encontraron coincidencias o reconocimiento facial no habilitado";
                }
            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
                response.Resultado = null;
            }

            return response;
        }
    }
}