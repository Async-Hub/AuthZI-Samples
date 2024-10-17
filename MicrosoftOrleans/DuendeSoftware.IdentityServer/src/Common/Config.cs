using System;

namespace Common
{
    public static class Config
    {
        public static string SiloHostName => Resolver.SiloName ?? "SiloHost";

        public static int SiloHostSiloPort => Resolver.SiloPort ?? 10000;

        public static int SiloHostGatewayPort => Resolver.SiloGatewayPort ?? 30000;

        public static string InstrumentationKey => Resolver.InstrKey ?? "50903127-8e5f-4ad1";

        public static string NativeClientUrl => "http://127.0.0.1:5010";

        public static string ApiUrl => Resolver.ApiServerUrl ?? "https://localhost:5003";
        public static string IdentityServerUrl => Resolver.IdsServerUrl ?? "https://localhost:5001";
        public static string WebClientUrl => Resolver.WebServerUrl ?? "https://localhost:5005";

        //public static string ApiUrl => "http://api.appi.asynchub.org";
        //public static string IdentityServerUrl => "https://identity.appi.asynchub.org";
        //public static string WebClientUrl => "http://webclient.appi.asynchub.org";

        private static class Resolver
        {
            public static string ApiServerUrl => 
                Environment.GetEnvironmentVariable(EnvironmentVariables.SimpleClusterApiServerUrl);

            public static string IdsServerUrl =>
                Environment.GetEnvironmentVariable(EnvironmentVariables.SimpleClusterIdentityServerUrl);

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