using System.Threading.Tasks;

namespace ProWishes.Repository
{
    public interface IRepositoryGenerec
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();
    }
}