using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Data;

namespace WeatherForecast.Tests
{
    public static class DbContextExtensions
    {
        public static async Task AddDummyWeatherForecast(this WeatherForecastDbContext context, int amount)
        {
            Random rnd = new();

            for (var i = 0; i < amount; i++)
            {
                context.WeatherForecasts.Add(new WeatherForecast()
                {
                    Date = DateTime.UtcNow,
                    TemperatureC = rnd.Next(-273, 1000),
                    Summary = $"Dummy summary."
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
