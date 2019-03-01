using System.Collections.Generic;

namespace ProWishes.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Wishe> Wishes { get; set; }
    }
}