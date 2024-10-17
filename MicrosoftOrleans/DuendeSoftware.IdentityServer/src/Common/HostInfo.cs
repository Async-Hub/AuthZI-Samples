﻿using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace Common
{
	public static class HostInfo
	{
		public static void Log(ILogger logger)
		{
			var builder = new StringBuilder();
			builder.Append($"SiloHostName : {Config.SiloHostName} \n");
			builder.Append($"{nameof(Dns.GetHostName)} : {Dns.GetHostName()} \n");

			foreach (var address in Dns.GetHostAddresses(Dns.GetHostName()))
			{
				builder.Append($"{nameof(IPAddress)} : {address} \n");
			}

			logger.LogInformation(builder.ToString());
		}
	}
}