using Authzi.Security.Authorization;
using Orleans;
using System.Threading.Tasks;

namespace GrainsInterfaces
{
    public interface IUserGrain : IGrainWithStringKey
    {
        [Authorize(Policy = "ManagerPolicy")]
        Task<string> TakeSecret();
    }
}
