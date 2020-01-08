using System.ComponentModel.DataAnnotations;

namespace Essiq.Showroom.Server.Models
{
    public class UserProfileLink
    {
        [Key]
        public string Id { get; set; }

        public UserProfileLinkType LinkType { get; set; }

        public string Title { get; set; }

        [Required]
        [Url]
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}
