using Orleans;
using System.Threading.Tasks;

namespace GrainsInterfaces
{
    public interface ITimeGrain : IGrainWithGuidKey
    {
        Task<string> GetCurrentTime();
    }
}
