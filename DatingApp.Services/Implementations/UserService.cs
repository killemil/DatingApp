namespace DatingApp.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DatingApp.Data;
    using Microsoft.EntityFrameworkCore;
    using DatingApp.Services.Models;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly DatingAppDbContext db;

        public UserService(DatingAppDbContext db)
        {
            this.db = db;
        }

        public void Add<T>(T entity) where T : class
            => this.db.Add(entity);

        public void Delete<T>(T entity) where T : class
            => this.db.Remove(entity);

        public async Task<UserForDetailsSM> GetUser(int id)
        {
            var mainPhotoUrl = await this.db
                .Users
                .Where(u => u.Id == id)
                .Select(p => p.Photos.FirstOrDefault(s => s.IsMain).Url)
                .FirstOrDefaultAsync();

            var user = await this.db
                   .Users
                   .Where(u => u.Id == id)
                   .ProjectTo<UserForDetailsSM>()
                   .FirstOrDefaultAsync();

            user.PhotoUrl = mainPhotoUrl;

            return user;
        }

        public async Task<IEnumerable<UserForListingSM>> GetUsers()
        {
            var users = await this.db
                   .Users
                   .ProjectTo<UserForListingSM>()
                   .ToListAsync();

            foreach (var user in users)
            {
                user.PhotoUrl = this.db
                    .Users
                    .Where(u => u.Id == user.Id)
                    .Select(p => p.Photos.FirstOrDefault(s => s.IsMain).Url)
                    .FirstOrDefault();
            }

            return users;
        }

        public async Task<bool> SaveAll()
            => await this.db.SaveChangesAsync() > 0;

        public async Task<bool> IsUserExists(int id)
        {
            var user = await this.db.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public async void UpdateUser(int id, string interests, string lookingFor, string introducton, string city, string country)
        {
            var currentUser = await this.db.Users.FindAsync(id);
            currentUser.Interests = interests;
            currentUser.LookingFor = lookingFor;
            currentUser.Introduction = introducton;
            currentUser.City = city;
            currentUser.Country = country;

            await this.db.SaveChangesAsync();
        }
    }
}
