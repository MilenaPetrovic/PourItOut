using System;

namespace PourItOut.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Player player &&
                   Name == player.Name;
        }
    }
}