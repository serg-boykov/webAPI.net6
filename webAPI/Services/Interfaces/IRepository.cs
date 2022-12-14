using webAPI.Models;

namespace webAPI.Services.Interfaces
{
    public interface IRepository
    {
        public Task<IEnumerable<Car>> GetAllAsync();

        public Task<Car>? GetByIdAsync(int id);

        public Task<Car> CreateAsync(Car model);

        public Task<Car>? UpdateAsync(Car model);

        public Task DeleteAsync(int id);

        public Task<IEnumerable<Car>> SearchAsync(string request);
    }
}
