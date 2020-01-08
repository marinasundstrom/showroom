namespace Essiq.Showroom.Shared
{
    public static class RoleConstants
    {
        public const string Administrator = nameof(Administrator);
        public const string Manager = nameof(Manager);
        public const string Consultant = nameof(Consultant);
        public const string AdministratorAndManager = nameof(Administrator) + ", " + nameof(Manager);
        public const string Client = nameof(Client);
    }
}
