namespace Essiq.Showroom.Client.Models
{
    public class Organization
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
