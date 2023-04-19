namespace dotnet_rpg.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] passwordSalt { get; set; }

        
    }
}
