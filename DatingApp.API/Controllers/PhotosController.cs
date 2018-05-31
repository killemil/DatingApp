namespace DatingApp.API.Controllers
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using DatingApp.API.Dtos;
    using DatingApp.Common.Infrastructure.Cloudinary;
    using DatingApp.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using static WebConstants;

    [Authorize]
    [Route("api/users/{userId}/photos")]
    public class PhotosController : BaseController
    {
        private readonly IUserService users;
        private readonly IPhotoService photos;
        private readonly IOptions<CloudinarySettings> cloudinaryConfig;

        private Cloudinary cloudinary;

        public PhotosController(IUserService users, IPhotoService photos, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this.users = users;
            this.photos = photos;
            this.cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                this.cloudinaryConfig.Value.CloudName,
                this.cloudinaryConfig.Value.ApiKey,
                this.cloudinaryConfig.Value.ApiSecret);

            cloudinary = new Cloudinary(acc);
        }

        [HttpGet(WithId, Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            return this.Ok(await this.photos.ById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, PhotoForCreationDto photoData)
        {
            var user = await this.users.GetUser(userId);

            if (user == null)
            {
                return this.BadRequest("Could not find user!");
            }

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (currentUserId != user.Id)
            {
                return this.Unauthorized();
            }

            var file = photoData.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = this.cloudinary.Upload(uploadParams);
                }
            }

            photoData.Url = uploadResult.Uri.ToString();
            photoData.PublicId = uploadResult.PublicId;

            var photoId = await this.photos
                 .AddPhoto(userId, photoData.Url, photoData.Description, photoData.PublicId);
            var photoToReturn = await this.photos.ById(photoId);

            return this.CreatedAtRoute("GetPhoto", new { id = photoId }, photoToReturn);
        }
    }
}
