namespace URLShort.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Passwood { get; set; }
        public int? RoleId { get; set; }

        public Role Role { get; set; }
    }
}
