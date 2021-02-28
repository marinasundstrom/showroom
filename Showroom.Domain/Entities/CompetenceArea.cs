using System.ComponentModel.DataAnnotations;

namespace Showroom.Domain.Entities
{
    public class CompetenceArea
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        //public CompetenceArea Parent { get; set; }

        //public ICollection<CompetenceArea> Children { get; set; }

    }
}
