﻿using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcBackend.Services
{
    public class WeatherService : WeatherForecasts.WeatherForecastsBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public override Task<GetWeatherForecastsResponse> GetWeatherForecasts(Empty request, ServerCallContext context)
        {
            var rng = new Random();
            var results = Enumerable.Range(1, 15).Select(index => new WeatherForecast
            {
                Date = DateTime.UtcNow.AddDays(index).ToTimestamp(),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();

            var response = new GetWeatherForecastsResponse();
            response.Forecasts.AddRange(results);

            return Task.FromResult(response);
        }
    }
}
