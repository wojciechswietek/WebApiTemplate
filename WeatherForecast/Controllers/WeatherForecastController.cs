using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Services;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService wfService;

        public WeatherForecastController(IWeatherForecastService wfService)
        {
            this.wfService = wfService;
        }

        [HttpGet("{id}")]
        public ActionResult<WeatherForecast> GetById([FromRoute] int id)
        {
            return Ok(wfService.GetById(id));
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<WeatherForecast>> GetAll()
        //{
        //    return Ok(wfService.GetAll());
        //}

        [HttpGet]
        public IEnumerable<WeatherForecast> GetAll()
        {
            return wfService.GetAll();
        }

        [HttpPost]
        public ActionResult<WeatherForecast> Create([FromBody]WeatherForecast wf)
        {
            return Created("", wfService.Create(wf));
        }

        [HttpGet("Generate")]
        public ActionResult<WeatherForecast> Generate()
        {
            return Ok(wfService.Generate());
        }
    }
}
