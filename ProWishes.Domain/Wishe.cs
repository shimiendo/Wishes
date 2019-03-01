using System.Collections.Generic;

namespace ProWishes.Domain
{
    public class Wishe
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int UserId { get; set; } 
        public virtual User User { get; set; }
    }
}