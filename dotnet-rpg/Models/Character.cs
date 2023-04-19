using dotnet_rpg.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_rpg.Models
{
    public class Character
    {
        public Character()
        {
            Users = new User();
        }
        public int Id { get; set; }

        public string Name { get; set; } = "Frodo";

        public int HitPoints { get; set; } = 100;

        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;

        public int Intelligence { get; set; } = 10;
 
        //[ForeignKey(nameof(User))]
        //public int UserId { get; set; }
        public User? Users { get; set; }
        public RpgClass Class { get; set; } = RpgClass.Knight;

        public Weapon Weapon { get; set; }
        public List<Skill> Skills { get; set; }
    } 
}
