namespace DatingApp.Services
{
    using DatingApp.Data.Models;
    using DatingApp.Services.Models;
    using System.Threading.Tasks;

    public interface IPhotoService
    {
        Task<PhotosForDetailsSM> ById(int id);

        Task<int> AddPhoto(int userId, string url, string description, string publicId);
    }
}
