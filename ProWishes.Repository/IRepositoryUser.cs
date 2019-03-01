using System.Collections.Generic;
using System.Threading.Tasks;
using ProWishes.Domain;

namespace ProWishes.Repository
{
    public interface IRepositoryUser : IRepositoryGenerec
    {
         Task<User[]> GetAllUserAsync(bool includeWishes);                  
         Task<User[]> GetAllUserAsyncPagesAsync(bool includeWishes, int? page);
         Task<User[]> GetAllUserAsyncByName(string name, bool includeWishes);
         Task<User> GetUserAsyncById(int userId, bool includeWishes);
    }
}