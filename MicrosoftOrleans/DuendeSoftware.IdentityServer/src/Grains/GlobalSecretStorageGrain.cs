using GrainsInterfaces;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grains
{
	public class GlobalSecretStorageGrain : Grain, IGlobalSecretStorageGrain
	{
		public Task<string> TakeUserSecret(string userId)
		{
			var secret = Secrets.GetUserSecret(userId);

			return Task.FromResult(secret);
		}

		private static class Secrets
		{
			private static readonly Dictionary<string, string> UsersSecrets;

			static Secrets()
			{
				UsersSecrets = new Dictionary<string, string>
				{
					// Alice - 1
					{ "1", "Alice super secret data..." },
					// Bob - 2
					{ "2", "Bob super secret data..." }
				};
			}

			public static string GetUserSecret(string userId) => UsersSecrets[userId];
		}
	}
}