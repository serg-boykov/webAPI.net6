using NSubstitute;
using webAPI.Models;
using webAPI.Services;
using webAPI.Services.Interfaces;

namespace webAPI.Tests
{
    public class ServiceTests
    {
        private readonly IService _service;

        private readonly IRepository _repository = Substitute.For<IRepository>();

        public ServiceTests()
        {
            _service = new Service(_repository);
        }


        [Test]
        [TestCase(1, true)]
        [TestCase(-1, false)]
        public async Task Check_GetById(int id, bool expectedResult)
        {
            // Arrange
            var car = new Car() { Id = id };
            Car result = null!;

            _repository.GetByIdAsync(id).Returns(car);

            // Act
            if (expectedResult == false)
                Assert.ThrowsAsync(Is.TypeOf<ArgumentOutOfRangeException>(), async () =>
                {
                    result = await _service.GetByIdAsync(id)!;
                });
            else
            {
                result = await _service.GetByIdAsync(id)!;

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(id));
                    Assert.That(result != null, Is.EqualTo(expectedResult));
                });
            }
        }


        [Test]
        [TestCase(0, 1, true)]
        [TestCase(1, 1, false)]
        public async Task Check_CreateAsync(int newId, int expectedId, bool expectedResult)
        {
            // Arrange
            var car = new Car() { Id = newId };
            var carSave = new Car() { Id = expectedId };

            _repository.CreateAsync(car).Returns(carSave);

            // Act
            if (expectedResult == false)
                Assert.ThrowsAsync(Is.TypeOf<ArgumentException>(), async () =>
                {
                    carSave = await _service.CreateAsync(car)!;
                });
            else
            {
                carSave = await _service.CreateAsync(car)!;

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(carSave.Id, Is.EqualTo(expectedId));
                    Assert.That(carSave != null, Is.EqualTo(expectedResult));
                });
            }
        }


        [Test]
        [TestCase(1, 1, "Merced", "Mercedes", true)]
        [TestCase(1, 1, "Mercedes", "Mercedes", false)]
        [TestCase(0, 1, "Merced", "Mercedes", false)]
        [TestCase(0, 1, "Mercedes", "Mercedes", false)]
        public async Task Check_UpdateAsync(int newId, int expectedId, string newName, string expectedName, bool expectedResult)
        {
            // Arrange
            var car = new Car() { Id = newId, Name = newName };
            var carSave = new Car() { Id = expectedId, Name = expectedName };
            Car carUpdated = null!;

            _repository.GetByIdAsync(car.Id).Returns(car);
            _repository.UpdateAsync(carSave).Returns(carSave);

            // Act
            if (expectedResult == false)
                Assert.ThrowsAsync(Is.TypeOf<ArgumentException>(), async () =>
                {
                    carUpdated = await _service.UpdateAsync(carSave)!;
                });
            else
            {
                carUpdated = await _service.UpdateAsync(carSave)!;

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(carUpdated.Id, Is.EqualTo(expectedId));
                    Assert.That(carUpdated != null, Is.EqualTo(expectedResult));
                    Assert.That(carUpdated!.Name, Is.EqualTo(expectedName));
                });
            }
        }


        [Test]
        public async Task Check_GetAllAsync()
        {
            // Arrange
            var cars = new List<Car>()
            {
                new Car { Id = 1 },
                new Car { Id = 2 }
            };

            _repository.GetAllAsync().Returns(cars);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.That(result.Any());
        }


        [Test]
        [TestCase("Fiat")]
        public async Task Check_SearchAsync(string request)
        {
            // Arrange
            var cars = new List<Car>()
            {
                new Car { Id = 2, Name = "Fiat" }
            };

            _repository.SearchAsync(request).Returns(cars);

            // Act
            var result = await _service.SearchAsync(request);

            // Assert
            Assert.That(result.Any());
        }
    }
}