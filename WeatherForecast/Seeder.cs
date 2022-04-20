using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Data;
using WeatherForecast.Services;

namespace WeatherForecast
{
    public interface ISeeder
    {
        Task Seed();
    }

    public class Seeder : ISeeder
    {
        private readonly WeatherForecastDbContext _dbContext;
        private readonly IWeatherForecastService _wfService;

        public Seeder(WeatherForecastDbContext dbContext, IWeatherForecastService wfService)
        {
            _dbContext = dbContext;
            _wfService = wfService;
        }

        public async Task Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Result.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (_dbContext.WeatherForecasts.Any() == false)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _wfService.Generate();
                    }
                }

            }
        }
    }
}
