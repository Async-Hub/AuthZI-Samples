using Authzi.AzureActiveDirectory;
using System;
using System.Collections.Generic;

namespace Common
{
    // Azure Active Directory credentials. Do not use hardcoded values for production!
    public static class Config
    {
        private static string DirectoryId => "1c59e6e7-0a62-4dfe-bfbc-38aeb089b0b9";

        public static string Domain => "asynchub.onmicrosoft.com";

        public static AzureActiveDirectoryApp ApiClientCredentials =>
            new AzureActiveDirectoryApp(DirectoryId,
                "20ac1601-2255-4bf6-849e-df44007621cd",
                "hYT2QBb5_K-KbFQ-..KOX9N._5O7Y4L5eK",
                Api1Scopes);

        public static AzureActiveDirectoryApp WebClientCredentials =>
            new AzureActiveDirectoryApp(DirectoryId,
                "e64ef6f7-eaef-4e43-92af-f25dba1f2de2",
                "Rb.n6J-z92_KYdc2UA6lR26RLgf5X-xd3G",
                Api1Scopes);

        public static IEnumerable<string> Api1Scopes =>
            new List<string> { "api://20ac1601-2255-4bf6-849e-df44007621cd/Api1" };

        public static string InstrumentationKey => Resolver.InstrKey ?? "50903127-8e5f-4ad1";

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
        }
    }
}
