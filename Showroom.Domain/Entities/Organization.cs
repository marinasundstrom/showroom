using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace Showroom.Domain.Entities
{
    public class Organization
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Address Address { get; set; }

        [JsonIgnore]
        public ICollection<ManagerProfile> Managers { get; set; }

        [JsonIgnore]
        public ICollection<ClientProfile> Clients { get; set; }

        [JsonIgnore]
        public ICollection<ConsultantProfile> Consultants { get; set; }
    }
}
