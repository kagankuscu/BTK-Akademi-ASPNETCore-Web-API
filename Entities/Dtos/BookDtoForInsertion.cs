using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record BookDtoForInsertion : BookDtoForManipulation
    {
        [Required(ErrorMessage = "Category Id is required")]
        public int CategoryId { get; set; }
    }
}