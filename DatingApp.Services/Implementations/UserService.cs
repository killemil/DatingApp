namespace DatingApp.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DatingApp.Data;
    using DatingApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using DatingApp.Services.Models;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly DatingAppDbContext context;

        public UserService(DatingAppDbContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
            => context.Add(entity);

        public void Delete<T>(T entity) where T : class
            => context.Remove(entity);

        public async Task<UserForDetailsSM> GetUser(int id)
            => await context
                .Users
                .Where(u=> u.Id == id)
                .ProjectTo<UserForDetailsSM>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<UserForListingSM>> GetUsers()
            => await context
                .Users
                .Include(p => p.Photos)
                .ProjectTo<UserForListingSM>()
                .ToListAsync();

        public async Task<bool> SaveAll()
            => await context.SaveChangesAsync() > 0;
    }
}
