using System.ComponentModel.DataAnnotations;

namespace Showroom.Application.Dtos
{
    public class CompetenceAreaDto
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
