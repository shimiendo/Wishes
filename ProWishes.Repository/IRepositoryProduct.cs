using System.Threading.Tasks;
using ProWishes.Domain;

namespace ProWishes.Repository
{
    public interface IRepositoryProduct : IRepositoryGenerec
    {
         Task<Product[]> GetAllProductAsync(bool includeWishes);
         Task<Product[]> GetAllProductAsyncByNameAsync(string name, bool includeWishes);
         Task<Product> GetProductAsyncByIdAsync(int productId, bool includeWishes);         
    }
}