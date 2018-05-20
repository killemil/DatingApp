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
            => this.context.Add(entity);

        public void Delete<T>(T entity) where T : class
            => this.context.Remove(entity);

        public async Task<UserForDetailsSM> GetUser(int id)
        {
            var mainPhotoUrl = await this.context
                .Users
                .Where(u => u.Id == id)
                .Select(p => p.Photos.FirstOrDefault(s => s.IsMain).Url)
                .FirstOrDefaultAsync();

            var user = await this.context
                   .Users
                   .Where(u => u.Id == id)
                   .ProjectTo<UserForDetailsSM>()
                   .FirstOrDefaultAsync();

            user.PhotoUrl = mainPhotoUrl;

            return user;
        }

        public async Task<IEnumerable<UserForListingSM>> GetUsers()
        {
            var users = await this.context
                   .Users
                   .ProjectTo<UserForListingSM>()
                   .ToListAsync();

            foreach (var user in users)
            {
                user.PhotoUrl = this.context
                    .Users
                    .Where(u => u.Id == user.Id)
                    .Select(p => p.Photos.FirstOrDefault(s => s.IsMain).Url)
                    .FirstOrDefault();
            }

            return users;
        }

        public async Task<bool> SaveAll()
            => await this.context.SaveChangesAsync() > 0;
    }
}
