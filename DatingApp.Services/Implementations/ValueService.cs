namespace DatingApp.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using DatingApp.Data;
    using DatingApp.Services.Models;
    using Microsoft.EntityFrameworkCore;

    public class ValueService : IValueService
    {
        private readonly DatingAppDbContext db;

        public ValueService(DatingAppDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ValueDetailsServiceModel>> All()
        => await this.db
            .Values
            .ProjectTo<ValueDetailsServiceModel>()
            .ToListAsync();

        public async Task<ValueDetailsServiceModel> ById(int id)
        => await this.db
            .Values
            .Where(v => v.Id == id)
            .ProjectTo<ValueDetailsServiceModel>()
            .FirstOrDefaultAsync();
    }
}
