using Dima.Core.Request;
using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories
{
    public class CreateCategoryRequest : Requisicao
    {        
        [Required(ErrorMessage = "Título inválido.")]
        [MaxLength(80, ErrorMessage = "Título deve conter no máximo 80 caractéres.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição inválida.")]
        public string Description { get; set; } = string.Empty ;
    }
}
