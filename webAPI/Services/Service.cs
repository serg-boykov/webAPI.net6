using webAPI.Models;
using webAPI.Services.Interfaces;

namespace webAPI.Services
{
    public class Service : IService
    {
        private readonly IRepository _repo;

        public Service(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
            => await _repo.GetAllAsync();


        public async Task<Car> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return await _repo.GetByIdAsync(id)!;
        }


        public async Task<Car> CreateAsync(Car model)
        {
            if (model.Id != 0)
                throw new ArgumentException("Id must be only 0", nameof(model));

            var result = await _repo.CreateAsync(model);

            if (result == null)
                throw new InvalidOperationException("Create error");

            return result;
        }


        public async Task<Car> UpdateAsync(Car model)
        {
            if (model.Id <= 0)
                throw new ArgumentException("Id must be > 0", nameof(model));

            var existCar = await _repo.GetByIdAsync(model.Id)!;

            if (existCar == null)
            {
                throw new Exception($"Information with Id = {model.Id} not found in the database");
            }

            if (model.Name == existCar!.Name)
                throw new ArgumentException("The Name should be different", nameof(model));

            var updateCar = await _repo.UpdateAsync(model)!;

            return updateCar!;
        }


        public async Task<string> DeleteAsync(int id)
        {
            bool success = true;
            var car = _repo.GetByIdAsync(id);

            try
            {
                if (car != null)
                {
                    await _repo.DeleteAsync(car.Result.Id);
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success ? "Delete successful" : "Delete was not successful";
        }


        public async Task<IEnumerable<Car>> SearchAsync(string request)
            => await _repo.SearchAsync(request);
    }
}
