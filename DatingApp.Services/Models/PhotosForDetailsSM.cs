namespace DatingApp.Services.Models
{
    using DatingApp.Common.Mapping;
    using DatingApp.Data.Models;
    using System;

    public class PhotosForDetailsSM : IMapFrom<Photo>
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set; }
    }
}
