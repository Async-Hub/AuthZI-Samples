using Authzi.Security.Authorization;
using Orleans;
using System.Threading.Tasks;

namespace GrainsInterfaces
{
    public interface IGlobalSecretStorageGrain : IGrainWithStringKey
    {
        [Authorize(Roles = "Admin")]
        Task<string> TakeUserSecret(string userId);
    }
}