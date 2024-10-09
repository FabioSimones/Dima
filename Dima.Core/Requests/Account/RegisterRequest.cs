using Dima.Core.Request;
using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Account
{
    public class RegisterRequest : Requisicao
    {
        [Required(ErrorMessage = "Email")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Seha inválida")]
        public string Password { get; set; } = string.Empty ;
    }
}
