using System;
using System.Data;
using CapaNegocio;

namespace PedidosApp
{
    public static class BusinessValidator
    {
        // Validaciones para pedidos
        public static class OrderValidator
        {
            public static ValidationResult ValidateStock(int productId, int quantity)
            {
                try
                {
                    bool stockAvailable = Nproducts.ValidarStockDisponible(productId, quantity);
                    
                    if (!stockAvailable)
                    {
                        var stockData = Nproducts.VerificarStock(productId, quantity);
                        if (stockData != null && stockData.Rows.Count > 0)
                        {
                            var row = stockData.Rows[0];
                            int currentStock = Convert.ToInt32(row["stock"]);
                            string productName = row["product_name"].ToString();
                            
                            return new ValidationResult
                            {
                                IsValid = false,
                                ErrorMessage = $"Stock insuficiente para {productName}. Stock disponible: {currentStock}, Solicitado: {quantity}"
                            };
                        }
                        return new ValidationResult
                        {
                            IsValid = false,
                            ErrorMessage = "No hay suficiente stock para este producto"
                        };
                    }
                    
                    return new ValidationResult { IsValid = true };
                }
                catch (Exception ex)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Error al validar stock: " + ex.Message
                    };
                }
            }

            public static ValidationResult ValidateDiscount(decimal discount)
            {
                if (discount < 0)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El descuento no puede ser negativo"
                    };
                }
                
                if (discount > 0.5m) // Máximo 50% de descuento
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El descuento no puede ser mayor al 50%"
                    };
                }
                
                return new ValidationResult { IsValid = true };
            }

            public static ValidationResult ValidateQuantity(int quantity)
            {
                if (quantity <= 0)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "La cantidad debe ser mayor a cero"
                    };
                }
                
                if (quantity > 999)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "La cantidad no puede ser mayor a 999"
                    };
                }
                
                return new ValidationResult { IsValid = true };
            }

            public static ValidationResult ValidateDeliveryDate(DateTime requiredDate)
            {
                if (requiredDate < DateTime.Now.Date)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "La fecha de entrega no puede ser en el pasado"
                    };
                }
                
                if (requiredDate > DateTime.Now.AddMonths(6))
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "La fecha de entrega no puede ser mayor a 6 meses"
                    };
                }
                
                return new ValidationResult { IsValid = true };
            }
        }

        // Validaciones para productos
        public static class ProductValidator
        {
            public static ValidationResult ValidatePrice(decimal price)
            {
                if (price < 0)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El precio no puede ser negativo"
                    };
                }
                
                if (price > 999999.99m)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El precio no puede ser mayor a $999,999.99"
                    };
                }
                
                return new ValidationResult { IsValid = true };
            }

            public static ValidationResult ValidateModelYear(int modelYear)
            {
                int currentYear = DateTime.Now.Year;
                
                if (modelYear < 1990)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El año del modelo no puede ser menor a 1990"
                    };
                }
                
                if (modelYear > currentYear + 2)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = $"El año del modelo no puede ser mayor a {currentYear + 2}"
                    };
                }
                
                return new ValidationResult { IsValid = true };
            }

            public static ValidationResult ValidateStock(int stock, int stockMinimo)
            {
                if (stock < 0)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El stock no puede ser negativo"
                    };
                }
                
                if (stockMinimo < 0)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El stock mínimo no puede ser negativo"
                    };
                }
                
                if (stockMinimo > stock)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El stock mínimo no puede ser mayor al stock actual"
                    };
                }
                
                return new ValidationResult { IsValid = true };
            }
        }

        // Validaciones para clientes
        public static class CustomerValidator
        {
            public static ValidationResult ValidateEmail(string email)
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El email es requerido"
                    };
                }
                
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    if (addr.Address != email)
                    {
                        return new ValidationResult
                        {
                            IsValid = false,
                            ErrorMessage = "Formato de email inválido"
                        };
                    }
                }
                catch
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Formato de email inválido"
                    };
                }
                
                return new ValidationResult { IsValid = true };
            }

            public static ValidationResult ValidatePhone(string phone)
            {
                if (string.IsNullOrWhiteSpace(phone))
                {
                    return new ValidationResult { IsValid = true }; // Teléfono es opcional
                }
                
                // Permitir solo números, espacios, guiones y paréntesis
                string cleanPhone = phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
                
                if (cleanPhone.Length < 7 || cleanPhone.Length > 15)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El teléfono debe tener entre 7 y 15 dígitos"
                    };
                }
                
                foreach (char c in cleanPhone)
                {
                    if (!char.IsDigit(c))
                    {
                        return new ValidationResult
                        {
                            IsValid = false,
                            ErrorMessage = "El teléfono solo puede contener números, espacios, guiones y paréntesis"
                        };
                    }
                }
                
                return new ValidationResult { IsValid = true };
            }

            public static ValidationResult ValidateName(string name, string fieldName)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = $"{fieldName} es requerido"
                    };
                }
                
                if (name.Length > 255)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = $"{fieldName} no puede tener más de 255 caracteres"
                    };
                }
                
                return new ValidationResult { IsValid = true };
            }
        }

        // Validaciones para usuarios
        public static class UserValidator
        {
            public static ValidationResult ValidateUserName(string userName)
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El nombre de usuario es requerido"
                    };
                }
                
                if (userName.Length < 3)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El nombre de usuario debe tener al menos 3 caracteres"
                    };
                }
                
                if (userName.Length > 50)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "El nombre de usuario no puede tener más de 50 caracteres"
                    };
                }
                
                return new ValidationResult { IsValid = true };
            }

            public static ValidationResult ValidatePassword(string password)
            {
                if (string.IsNullOrWhiteSpace(password))
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "La contraseña es requerida"
                    };
                }
                
                if (password.Length < 6)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "La contraseña debe tener al menos 6 caracteres"
                    };
                }
                
                if (password.Length > 250)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "La contraseña es demasiado larga"
                    };
                }
                
                return new ValidationResult { IsValid = true };
            }
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        
        public ValidationResult()
        {
            IsValid = true;
            ErrorMessage = string.Empty;
        }
    }
}