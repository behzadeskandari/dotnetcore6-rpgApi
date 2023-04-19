﻿using dotnet_rpg.Models;

namespace dotnet_rpg.Data
{
    public class Weapon
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
