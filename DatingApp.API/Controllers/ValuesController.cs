namespace DatingApp.API.Controllers
{
    using Infrastructure.Extensions;
    using System.Threading.Tasks;
    using DatingApp.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using static WebConstants;

    [AllowAnonymous]
    public class ValuesController : BaseController
    {
        private readonly IValueService values;

        public ValuesController(IValueService values)
        {
            this.values = values;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => this.Ok(await this.values.All());

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.values.ById(id));
            
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
