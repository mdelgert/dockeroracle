using Dapper;
using Oracle.ManagedDataAccess.Client;

namespace dockeroracle.Services
{
    public class WeatherRepository
    {
        private readonly IConfiguration _config;
        private string ConnStr => _config.GetConnectionString("OracleDb");

        public WeatherRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            var sql = @"
                SELECT 
                    Id,
                    ForecastDate,
                    TemperatureC,
                    Summary
                FROM WeatherForecast
                ORDER BY ForecastDate
                ;
            ";

            using var conn = new OracleConnection(ConnStr);

            return await conn.QueryAsync<WeatherForecast>(sql);
        }
    }
}
