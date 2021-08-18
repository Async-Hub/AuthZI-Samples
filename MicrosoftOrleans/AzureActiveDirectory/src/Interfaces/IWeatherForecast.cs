using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrainsInterfaces
{
    public interface IWeatherForecastGrain : IGrainWithStringKey
    {
        Task<IEnumerable<WeatherForecast>> Get();
    }
}
