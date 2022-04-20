using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Controllers;
using WeatherForecast.Data;
using WeatherForecast.Services;
using Xunit;

namespace WeatherForecast.Tests
{
    public class WeatherForecastControllerTests : IClassFixture<DbContextFactory>
    {
        private readonly DbContextFactory _factory;

        public WeatherForecastControllerTests(DbContextFactory factory) => _factory = factory;

        [Fact]
        public void AlwaysPassing()
        {
            Assert.True(1 == 1);
        }

        [Fact]
        public async Task WeatherForecastControllerIntegrationTest()
        {
            using var context = _factory.GetContext();

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var wfService = new WeatherForecastService(context);
            var controller = new WeatherForecastController(wfService);

            var wf = new WeatherForecast
            {
                Date = DateTime.UtcNow,
                TemperatureC = 666,
                Summary = "Hell"
            };

            controller.Create(wf);

            var result = controller.GetAll();

            Assert.Single(result);
            Assert.Equal("Hell", result.First().Summary);
        }
    }
}
