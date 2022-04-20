using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherForecast.Data;
using Xunit;

// Disable parallel test execution because this test suite
// contains integration tests accessing a real database (LocalDB).
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace WeatherForecast.Tests
{
    public sealed class DbContextFactory
    {
        private readonly DbContextOptions<WeatherForecastDbContext> options;

        public DbContextFactory()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<WeatherForecastDbContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:WeatherForecastDb"]);
            options = optionsBuilder.Options;
        }

        public WeatherForecastDbContext GetContext() => new(options);
    }
}
