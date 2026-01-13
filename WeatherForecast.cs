namespace dockeroracle
{
    public class WeatherForecast
    {
        public int Id { get; set; }
        //public DateOnly Date { get; set; }
        public DateTime ForecastDate { get; set; }
        public DateOnly Date => DateOnly.FromDateTime(ForecastDate);
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string? Summary { get; set; }
    }
}
