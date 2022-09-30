using Microsoft.EntityFrameworkCore;
using webAPI.Database;
using webAPI.Models;
using webAPI.Services.Interfaces;

namespace webAPI.Services
{
    public class Repository : IRepository
    {
        private readonly ApplicationContext _db;

        public Repository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _db.Set<Car>()
                .Include(c => c.Transmissions)
                .Include(c => c.Brakes)
                .Include(c => c.Color)
                .ToListAsync();
        }

        public async Task<Car>? GetByIdAsync(int id)
        {
            var result = await _db.Set<Car>()
                .Include(c => c.Transmissions)
                .Include(c => c.Brakes)
                .Include(c => c.Color)
                .FirstOrDefaultAsync(m => m.Id == id);

            return result!;
        }

        public async Task<Car> CreateAsync(Car model)
        {
            await _db.Set<Car>().AddAsync(model);

            await _db.Set<CarColor>().AddAsync(model.Color!);
            await _db.Set<CarBrake>().AddRangeAsync(model.Brakes!);
            await _db.Set<CarTransmission>().AddRangeAsync(model.Transmissions!);
            await _db.SaveChangesAsync();

            return model;
        }

        public async Task<Car>? UpdateAsync(Car model)
        {
            var toUpdate = await _db.Set<Car>().FirstOrDefaultAsync(m => m.Id == model.Id);

            if (toUpdate != null)
            {
                toUpdate = model;
            }

            _db.Update(toUpdate!);
            await _db.SaveChangesAsync();

            return toUpdate!;
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _db.Set<Car>().FirstOrDefaultAsync(m => m.Id == id);
            _db.Set<Car>().Remove(toDelete!);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> SearchAsync(string request)
        {
            return await _db.Set<Car>()
                .Include(c => c.Transmissions)
                .Include(c => c.Brakes)
                .Include(c => c.Color)
                .Where(q => q.Name!.ToLower().Contains(request)
                        || q.Price.ToString()!.Contains(request)
                        || q.Model!.ToLower().Contains(request)
                        || (q.Transmissions!.Any(t => t.Name!.Contains(request) && t.Checked == true))
                        || (q.Brakes!.Any(b => b.Name!.Contains(request) && b.Checked == true))
                        || q.Color!.Name!.ToLower().Contains(request)
                        || q.Date.ToString()!.ToLower().Contains(request)
                        || q.Description!.ToLower().Contains(request))
                .ToListAsync();
        }
    }
}
