namespace DatingApp.Services.Models
{
    using AutoMapper;
    using DatingApp.Common.Infrastructure.Extensions;
    using DatingApp.Common.Mapping;
    using DatingApp.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserForDetailsSM : IMapFrom<User>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public IEnumerable<PhotosForDetailsSM> Photos { get; set; }

        public string PhotoUrl { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            //TODO not mapping photoUrl

            //mapper.CreateMap<User, UserForListingSM>()
            //      .ForMember(u => u.PhotoUrl,
            //          cfg => cfg.MapFrom(u => u.Photos.FirstOrDefault(p => p.IsMain).Url));

            mapper.CreateMap<User, UserForDetailsSM>()
                .ForMember(u => u.Age,
                    cfg => cfg.MapFrom(u => u.DateOfBirth.CalculateAge()));
        }
    }
}
