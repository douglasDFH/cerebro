using BlazorEcommerce.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;

namespace BlazorEcommerce.Client.Servicios
{
    public class PersonaServicio : IPersonaServicio
    {
        private readonly HttpClient _httpClient;

        public PersonaServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<List<PersonaDTO>>> Lista(string Rol, string Valor)
        {
            try
            {
                // Usar una URL más simple con parámetros de consulta para evitar problemas de formato
                var response = await _httpClient.GetFromJsonAsync<ResponseDTO<List<PersonaDTO>>>
                    ($"/api/Persona/Lista?rol={Uri.EscapeDataString(Rol)}&valor={Uri.EscapeDataString(Valor)}");

                return response!;
            }
            catch (Exception ex)
            {
                // En caso de error, devolver una respuesta con el mensaje de error
                return new ResponseDTO<List<PersonaDTO>>
                {
                    EsCorrecto = false,
                    Mensaje = $"Error al obtener la lista: {ex.Message}",
                    Resultado = new List<PersonaDTO>()
                };
            }
        }

        public async Task<ResponseDTO<PersonaDTO>> Obtener(int Id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResponseDTO<PersonaDTO>>($"/api/Persona/Obtener/{Id}")
                    ?? new ResponseDTO<PersonaDTO> { EsCorrecto = false, Mensaje = "Error al obtener datos" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<PersonaDTO>
                {
                    EsCorrecto = false,
                    Mensaje = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO<SesionDTO>> Autorizacion(LoginDTO modelo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Persona/Autorizacion", modelo);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResponseDTO<SesionDTO>>();
                    return result!;
                }
                else
                {
                    return new ResponseDTO<SesionDTO>
                    {
                        EsCorrecto = false,
                        Mensaje = $"Error HTTP: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO<SesionDTO>
                {
                    EsCorrecto = false,
                    Mensaje = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO<PersonaDTO>> Crear(PersonaDTO modelo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Persona/Crear", modelo);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResponseDTO<PersonaDTO>>();
                    return result!;
                }
                else
                {
                    return new ResponseDTO<PersonaDTO>
                    {
                        EsCorrecto = false,
                        Mensaje = $"Error HTTP: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO<PersonaDTO>
                {
                    EsCorrecto = false,
                    Mensaje = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO<bool>> Editar(PersonaDTO modelo)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/api/Persona/Editar", modelo);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
                    return result!;
                }
                else
                {
                    return new ResponseDTO<bool>
                    {
                        EsCorrecto = false,
                        Mensaje = $"Error HTTP: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>
                {
                    EsCorrecto = false,
                    Mensaje = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO<bool>> Eliminar(int Id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Persona/Eliminar/{Id}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
                    return result!;
                }
                else
                {
                    return new ResponseDTO<bool>
                    {
                        EsCorrecto = false,
                        Mensaje = $"Error HTTP: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>
                {
                    EsCorrecto = false,
                    Mensaje = $"Error: {ex.Message}"
                };
            }
        }
        // Añadir este método a la clase existente

        public async Task<ResponseDTO<SesionDTO>> AutorizacionFacial(FacialLoginDTO modelo)
        {
            try
            {
                // Llamada al endpoint de autenticación facial
                var response = await _httpClient.PostAsJsonAsync("/api/Persona/AutorizacionFacial", modelo);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResponseDTO<SesionDTO>>();
                    return result!;
                }
                else
                {
                    // Si hay un error HTTP, crear una respuesta con el error
                    return new ResponseDTO<SesionDTO>
                    {
                        EsCorrecto = false,
                        Mensaje = $"Error HTTP: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                // Si hay una excepción, crear una respuesta con el error
                return new ResponseDTO<SesionDTO>
                {
                    EsCorrecto = false,
                    Mensaje = $"Error: {ex.Message}"
                };
            }
        }
    }
}