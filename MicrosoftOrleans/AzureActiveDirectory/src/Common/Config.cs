using System;
using System.Collections.Generic;
using Authzi.AzureActiveDirectory;

namespace Common
{
    // Azure Active Directory credentials. Do not use hardcoded values for production!
    public static class Config
    {
        private static string DirectoryId => "1c59e6e7-xxx-38aeb089b0b9";

        public static string Domain => "xxx.onmicrosoft.com";

        public static AzureActiveDirectoryApp ApiClientCredentials =>
            new AzureActiveDirectoryApp(DirectoryId,
                "20ac1601-xxx",
                "xxx",
                Api1Scopes);

        public static AzureActiveDirectoryApp WebClientCredentials =>
            new AzureActiveDirectoryApp(DirectoryId,
                "e64ef6f7-xxx",
                "xxx",
                Api1Scopes);

        public static IEnumerable<string> Api1Scopes =>
            new List<string> { "api://20ac1601-xxx/Api1" };

        public static string SiloHostName => Resolver.SiloName ?? "SiloHost";

        public static int SiloHostSiloPort => Resolver.SiloPort ?? 10000;

        public static int SiloHostGatewayPort => Resolver.SiloGatewayPort ?? 30000;

        public static string InstrumentationKey => Resolver.InstrKey ?? "50903127-xxx";

        public static string ApiUrl => Resolver.ApiServerUrl ?? "http://localhost:5002";

        public static string WebClientUrl => Resolver.WebServerUrl ?? "http://localhost:5004";

        private static class Resolver
        {
            public static string ApiServerUrl => 
                Environment.GetEnvironmentVariable(EnvironmentVariables.SimpleClusterApiServerUrl);

            public static string WebServerUrl =>
                Environment.GetEnvironmentVariable(EnvironmentVariables.SimpleClusterWebClientServerUrl);

            public static string InstrKey =>
                Environment.GetEnvironmentVariable(EnvironmentVariables.SimpleClusterInstrumentationKey);

            public static string SiloName =>
                Environment.GetEnvironmentVariable(EnvironmentVariables.SimpleClusterSiloHostName);

            public static int? SiloPort
            {
                get
                {
                    var val = Environment.GetEnvironmentVariable(EnvironmentVariables.SimpleClusterSiloHostSiloPort);
                    if (string.IsNullOrWhiteSpace(val)) return null;
                    return int.Parse(val);
                }
            }

            public static int? SiloGatewayPort
            {
                get
                {
                    var val = Environment.GetEnvironmentVariable(EnvironmentVariables.SimpleClusterSiloHostGatewayPort);
                    if (string.IsNullOrWhiteSpace(val)) return null;
                    return int.Parse(val);
                }
            }
        }
    }
}
