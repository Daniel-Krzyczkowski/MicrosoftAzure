using System;
using System.Collections.Generic;
using System.Text;

namespace AdGraphClientTestApp.Configuration
{
    public class AzureAdGraphSettings
    {
        public string AzureAdB2CTenant { get; private set; } = "xxx.onmicrosoft.com";
        public string ClientId { get; private set; } = "xxx";
        public string ClientSecret { get; private set; } = "xxx";
        public string ExtensionClientId { get; private set; } = "xxx";
        public string ApiVersion { get; private set; } = "v1.0";
        public string GraphApiBaseUrl { get; private set; } = "https://graph.microsoft.com/";
        public string BetaGraphApiBaseUrl { get; private set; } = "https://graph.microsoft.com/beta/";
    }
}
