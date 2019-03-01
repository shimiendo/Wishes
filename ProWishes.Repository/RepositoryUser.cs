using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProWishes.Domain;

namespace ProWishes.Repository
{
    public class RepositoryUser : IRepositoryUser
    {
        private readonly ProWishesContext _context;
        public RepositoryUser(ProWishesContext context)
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
        public async Task<User[]> GetAllUserAsync(bool includeWishes = false)
        {            
            IQueryable<User> query = _context.Users;       

            if(includeWishes)
            {
                query = query
                    .Include(w => w.Wishes);
            }

            query = query.AsNoTracking()
                        .OrderByDescending(c => c.Name); 

            //var paginatedUsers = new PaginatedList<User>(query, page ?? 0, pageSize);                        

            return await query.ToArrayAsync();                                   
        }



        //aqui kokokokkok
        public async Task<User[]> GetAllUserAsyncPagesAsync(bool includeWishes, int? page)
        {
            const int pageSize = 10;
            IQueryable<User> query = _context.Users;       

            if(includeWishes)
            {
                query = query
                    .Include(w => w.Wishes);
            }

            query = query.AsNoTracking()
                        .OrderByDescending(c => c.Name); 

            var paginatedUsers = new PaginatedList<User>(query, page ?? 0, pageSize); 
            var queryable = paginatedUsers.AsQueryable();

            return await queryable.ToArrayAsync();                      
        } 


        public async Task<User[]> GetAllUserAsyncByName(string name, bool includeWishes)
        {
            IQueryable<User> query = _context.Users;       

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

        public async Task<User> GetUserAsyncById(int userId, bool includeWishes)
        {
            IQueryable<User> query = _context.Users;       

            if(includeWishes)
            {
                query = query
                    .Include(w => w.Wishes);
            } 

            query = query.AsNoTracking()
                        .OrderByDescending(c => c.Name)
                        .Where(c => c.Id == userId);

            return await query.FirstOrDefaultAsync();            
        }
    }
}