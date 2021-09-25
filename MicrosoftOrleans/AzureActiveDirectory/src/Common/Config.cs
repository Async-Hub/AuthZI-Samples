using Authzi.AzureActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    // Azure Active Directory credentials. Do not use hardcoded values for production!
    public static class Config
    {
        private static string DirectoryId => "1c59e6e7-0a62-4dfe-bfbc-38aeb089b0b9";

        public static string Domain => "asynchub.onmicrosoft.com";

        public static AzureActiveDirectoryApp OrleansClientCredentials =>
            new AzureActiveDirectoryApp(DirectoryId,
                "f1011d87-15fc-4517-ac7c-5c98d259260e",
                "oXUt_LR2aM7xFl.4iI78q-U2W5~8x_ccE1",
                Api1Scopes);

        public static AzureActiveDirectoryApp ApiClientCredentials =>
            new AzureActiveDirectoryApp(DirectoryId,
                "20ac1601-2255-4bf6-849e-df44007621cd",
                "hYT2QBb5_K-KbFQ-..KOX9N._5O7Y4L5eK",
                Api1Scopes);

        public static AzureActiveDirectoryApp WebClientCredentials =>
            new AzureActiveDirectoryApp(DirectoryId,
                "e64ef6f7-eaef-4e43-92af-f25dba1f2de2",
                "Rb.n6J-z92_KYdc2UA6lR26RLgf5X-xd3G",
                Api1Scopes.Concat(OrleansScopes));

        public static IEnumerable<string> Api1Scopes =>
            new List<string> { "api://20ac1601-2255-4bf6-849e-df44007621cd/Api1" };

        public static IEnumerable<string> OrleansScopes =>
            new List<string> { "api://f1011d87-15fc-4517-ac7c-5c98d259260e/Orleans" };

        public static string SiloHostName => Resolver.SiloName ?? "SiloHost";

        public static int SiloHostSiloPort => Resolver.SiloPort ?? 10000;

        public static int SiloHostGatewayPort => Resolver.SiloGatewayPort ?? 30000;

        public static string InstrumentationKey => Resolver.InstrKey ?? "50903127-8e5f-4ad1";

        public static string ApiUrl => Resolver.ApiServerUrl ?? "http://localhost:5002";

        public static string WebClientUrl => Resolver.WebServerUrl ?? "http://localhost:5004";

        //public static string ApiUrl => "http://api.appi.asynchub.org";
        //public static string IdentityServerUrl => "https://identity.appi.asynchub.org";
        //public static string WebClientUrl => "http://webclient.appi.asynchub.org";

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
