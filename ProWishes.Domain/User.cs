using System.Collections.Generic;

namespace ProWishes.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual List<Wishe> Wishes { get; set; }
    }
}