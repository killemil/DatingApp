﻿namespace DatingApp.Services
{
    using DatingApp.Data.Models;
    using DatingApp.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveAll();

        Task<IEnumerable<UserForListingSM>> GetUsers();

        Task<UserForDetailsSM> GetUser(int id);
    }
}
