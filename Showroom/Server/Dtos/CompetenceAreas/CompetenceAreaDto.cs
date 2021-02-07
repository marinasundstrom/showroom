using System.ComponentModel.DataAnnotations;

namespace Showroom.Server.Dtos
{
    public class CompetenceAreaDto
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
