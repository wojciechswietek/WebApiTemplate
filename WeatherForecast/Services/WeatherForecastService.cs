using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Data;

namespace WeatherForecast.Services
{
    public interface IWeatherForecastService
    {
        public WeatherForecast Generate();
        WeatherForecast Create(WeatherForecast wf);
        IEnumerable<WeatherForecast> GetAll();
        WeatherForecast GetById(int id);
    }

    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly WeatherForecastDbContext dbContext;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastService(WeatherForecastDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public WeatherForecast Generate()
        {
            var rng = new Random();

            var wf = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };

            return wf;
        }

        public WeatherForecast Create(WeatherForecast wf)
        {
            dbContext.WeatherForecasts.Add(wf);
            dbContext.SaveChanges();

            return wf;
        }

        public WeatherForecast GetById(int id)
        {
            return dbContext.WeatherForecasts.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<WeatherForecast> GetAll()
        {
            return dbContext.WeatherForecasts.ToList();
        }

    }
}
