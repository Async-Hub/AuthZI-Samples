using System.Threading.Tasks;
using Authzi.Security.Authorization;
using Orleans;

namespace GrainsInterfaces
{
    public interface IGlobalSecretStorageGrain : IGrainWithStringKey
    {
        [Authorize(Roles = "Admin")]
        Task<string> TakeUserSecret(string userId);
    }
}