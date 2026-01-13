using dockeroracle.Services;
using Microsoft.AspNetCore.Mvc;

namespace dockeroracle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherRepository _repo;

        public WeatherController(WeatherRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather()
        {
            var data = await _repo.GetAllAsync();
            return Ok(data);
        }
    }
}
