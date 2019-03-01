using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProWishes.Domain;

namespace ProWishes.Repository
{
    public class RepositoryProduct : IRepositoryProduct
    {
        private readonly ProWishesContext _context;
        public RepositoryProduct(ProWishesContext context)
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

        public async Task<Product[]> GetAllProductAsync(bool includeWishes = false)
        {
            IQueryable<Product> query = _context.Products;       

            if(includeWishes)
            {
                query = query
                    .Include(w => w.Wishes);
            }

            query = query.AsNoTracking()
                        .OrderByDescending(c => c.Name); 

            return await query.ToArrayAsync();                                               
        }

        public async Task<Product[]> GetAllProductAsyncByNameAsync(string name, bool includeWishes)
        {
            IQueryable<Product> query = _context.Products;       

            if(includeWishes)
            {
                query = query
                    .Include(w => w.Wishes);
            }   

            query = query.AsNoTracking()
                        .OrderByDescending(c => c.Name)
                        .Where(c => c.Name.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();            
        }

        public async Task<Product> GetProductAsyncByIdAsync(int productId, bool includeWishes)
        {
            IQueryable<Product> query = _context.Products;       

            if(includeWishes)
            {
                query = query
                    .Include(w => w.Wishes);
            } 

            query = query.AsNoTracking()
                        .OrderByDescending(c => c.Name)
                        .Where(c => c.Id == productId);

            return await query.FirstOrDefaultAsync();            
        }                    
    }
}