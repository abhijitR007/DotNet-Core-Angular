using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public DataContext _context { get; }
    
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("allnames")]
        public async Task<IActionResult> GetAllNames()
        {
            var values= await _context.Values.ToListAsync();
            return Ok(values);

        }

        [HttpGet("{id}")]
        public IActionResult GetName( int id)
        {
            var values=_context.Values.FirstOrDefault(x=>x.id==id);
            return Ok(values);

        }
        
        [HttpPost("addname")]
        public async Task<IActionResult> AddNames(Value value12)
        {
         await _context.Values.AddAsync(value12);
           await _context.SaveChangesAsync();
            return Ok(value12);

        }
    }
}
