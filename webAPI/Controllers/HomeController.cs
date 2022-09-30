using Microsoft.AspNetCore.Mvc;
using webAPI.Models;
using webAPI.Services.Interfaces;

namespace webAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IService _service;

        public HomeController(IService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IEnumerable<Car>> Get()
            => await _service.GetAllAsync();


        [HttpGet("{id}")]
        public async Task<Car> GetById(int id)
            => await _service.GetByIdAsync(id)!;


        [HttpPost]
        public async Task<Car> Post(Car car)
            => await _service.CreateAsync(car);


        [HttpPut]
        public async Task<Car> Put(Car car)
            => await _service.UpdateAsync(car);


        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
            => await _service.DeleteAsync(id);


        [HttpGet]
        [Route("Search/{request}")]
        public async Task<IEnumerable<Car>> Search(string request)
            => await _service.SearchAsync(request);
    }
}
