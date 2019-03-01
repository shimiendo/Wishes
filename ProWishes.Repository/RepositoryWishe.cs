using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProWishes.Domain;

namespace ProWishes.Repository
{
    public class RepositoryWishe : IRepositoryWishe
    {
        private readonly ProWishesContext _context;
        public RepositoryWishe(ProWishesContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;          
        }        
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }        
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }        

        public async Task<Wishe[]> GetAllWisheAsync(bool includeProduct, bool includeUser)
        {
            IQueryable<Wishe> query = _context.Wishes;       

            if(includeProduct)
            {
                query = query
                    .Include(p => p.Product);
            }
            
            if(includeUser)
            {
                query = query
                    .Include(u => u.User);
            }

            query = query.AsNoTracking()
                         .OrderByDescending(c => c.Name); ;

            return await query.ToArrayAsync();                                               
        }

        public async Task<Wishe[]> GetAllWisheAsyncByNameAsync(string name, bool includeProduct, bool includeUser)
        {
            IQueryable<Wishe> query = _context.Wishes;       

            if(includeProduct)
            {
                query = query
                    .Include(p => p.Product);
            }
            
            if(includeUser)
            {
                query = query
                    .Include(u => u.User);
            }

            query = query.AsNoTracking()
                        .OrderByDescending(c => c.Name)
                        .Where(c => c.Name.ToLower().Contains(name.ToLower()));; 

            return await query.ToArrayAsync();              
        }

        public async Task<Wishe> GetWisheAsyncByIdAsync(int wishetId, bool includeProduct, bool includeUser)
        {
            IQueryable<Wishe> query = _context.Wishes;       

            if(includeProduct)
            {
                query = query
                    .Include(p => p.Product);
            }
            
            if(includeUser)
            {
                query = query
                    .Include(u => u.User);
            }

            query = query.AsNoTracking()
                        .OrderByDescending(c => c.Name)
                        .Where(c => c.Id == wishetId);

            return await query.FirstOrDefaultAsync();   
        }
    }
}