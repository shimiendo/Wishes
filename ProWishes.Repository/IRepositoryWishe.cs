using System.Threading.Tasks;
using ProWishes.Domain;

namespace ProWishes.Repository
{
    public interface IRepositoryWishe : IRepositoryGenerec
    {
         Task<Wishe[]> GetAllWisheAsync(bool includeProduct, bool includeUser);
         Task<Wishe[]> GetAllWisheAsyncByNameAsync(string name, bool includeProduct, bool includeUser);
         Task<Wishe> GetWisheAsyncByIdAsync(int wishetId, bool includeProduct, bool includeUser);
    }
}