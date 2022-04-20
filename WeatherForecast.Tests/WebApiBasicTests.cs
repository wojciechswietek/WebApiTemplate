using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Data;
using Xunit;

namespace WeatherForecast.Tests
{
    public class WebApiBasicTests : IClassFixture<WebApplicationFactory<Startup>>, IClassFixture<DbContextFactory>
    {
        //private readonly WebApplicationFactory<Startup> _webClientFactory;
        private readonly DbContextFactory _contextFactory;

        private HttpClient _httpClient;

        public WebApiBasicTests(WebApplicationFactory<Startup> webClientFactory, DbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;

            _httpClient = webClientFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services
                            .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<WeatherForecastDbContext>));

                    services.Remove(dbContextOptions);

                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .AddEnvironmentVariables()
                        .Build();

                    services.AddDbContext<WeatherForecastDbContext>(options =>
                        options.UseSqlServer(configuration["ConnectionStrings:WeatherForecastDb"]));
                });
            }).CreateClient();
        }

        [Fact]
        public async Task GetAll()
        {
            int amount = 5;

            using var context = _contextFactory.GetContext();

            await context.Database.EnsureDeletedAsync();
            await context.Database.MigrateAsync();

            await context.AddDummyWeatherForecast(amount);

            var p = await _httpClient.GetFromJsonAsync<IEnumerable<WeatherForecast>>("/WeatherForecast");
            Assert.NotNull(p);
            Assert.Equal(amount, p.Count());
        }
    }
}