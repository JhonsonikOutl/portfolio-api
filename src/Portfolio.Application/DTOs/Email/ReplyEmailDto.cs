using System.ComponentModel.DataAnnotations;

namespace Portfolio.Application.DTOs.Email
{
    /// <summary>
    /// DTO para responder a mensajes de contacto.
    /// </summary>
    public class ReplyEmailDto
    {
        [Required(ErrorMessage = "El cuerpo de la respuesta es requerido")]
        [MinLength(10, ErrorMessage = "La respuesta debe tener al menos 10 caracteres")]
        public string Body { get; set; } = string.Empty;

        public bool IsHtml { get; set; } = false;
    }
}
