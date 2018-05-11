namespace DatingApp.Services.Models
{
    using DatingApp.Common.Mapping;
    using DatingApp.Data.Models;

    public class ValueDetailsServiceModel : IMapFrom<Value>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
