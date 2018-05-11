namespace DatingApp.Services
{
    using DatingApp.Services.Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IValueService
    {
        Task<IEnumerable<ValueDetailsServiceModel>> All();

        Task<ValueDetailsServiceModel> ById(int id);
    }
}
