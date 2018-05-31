namespace DatingApp.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using System.Linq;
    using System.Threading.Tasks;
    using DatingApp.Data;
    using DatingApp.Data.Models;
    using DatingApp.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class PhotoService : IPhotoService
    {
        private readonly DatingAppDbContext db;

        public PhotoService(DatingAppDbContext db)
        {
            this.db = db;
        }

        public async Task<PhotosForDetailsSM> ById(int id)
            => await this.db.Photos
                .Where(p => p.Id == id)
                .ProjectTo<PhotosForDetailsSM>()
                .FirstOrDefaultAsync();

        public async Task<int> AddPhoto(int userId, string url, string description, string publicId)
        {
            var user = await this.db.Users.FindAsync(userId);

            var photo = new Photo
            {
                DateAdded = DateTime.Now,
                Description = description,
                PublicId = publicId,
                Url = url,
                User = user,
                IsMain = user.Photos.Any() ? false : true
            };

            await this.db.Photos.AddAsync(photo);
            await this.db.SaveChangesAsync();

            return photo.Id;
        }
    }
}
