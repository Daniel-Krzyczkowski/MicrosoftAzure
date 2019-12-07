using AdGraphClientTestApp.Authentication;
using AdGraphClientTestApp.Configuration;
using AdGraphClientTestApp.Model;
using AdGraphClientTestApp.Model.Audit;
using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdGraphClientTestApp
{
    public interface IGraphApiService
    {
        void Initialize(bool shouldUseBetaEndpoint);
        Task DeleteUserAsync(string userId);
        Task<SignInData> GetLatestUserLoginData(DateTime? fromDate = null);
        Task<List<Microsoft.Graph.User>> GetAllRegisteredUsers();
        Task<AuditLogsData> GetAuditLogsForUsers();
    }

    public class GraphApiService : IGraphApiService
    {
        private IGraphServiceClient _graphServiceClient;
        private IAuthenticationProvider _authenticationProvider;
        private IGraphApiConnector _graphApiConnector;
        private AzureAdGraphSettings _azureAdGraphSettings;

        public GraphApiService(AzureAdGraphSettings azureAdGraphSettings)
        {
            _azureAdGraphSettings = azureAdGraphSettings;
        }

        public void Initialize(bool shouldUseBetaEndpoint)
        {
            _authenticationProvider = new AzureAdGraphAuthenticationProvider(_azureAdGraphSettings);
            _graphApiConnector = new GraphApiConnector(_authenticationProvider, _azureAdGraphSettings);

            _graphServiceClient = _graphApiConnector.GetAuthenticatedGraphServiceClient(shouldUseBetaEndpoint);
        }

        public async Task DeleteUserAsync(string userId)
        {
            await _graphServiceClient
                .Users[userId]
                .Request()
                .DeleteAsync();
        }

        public async Task<SignInData> GetLatestUserLoginData(DateTime? fromDate = null)
        {
            string query = string.Empty;

            if (fromDate != null)
            {
                var fromDateFormatted = fromDate?.ToString("yyyy-MM-dd");
                query = $"{_azureAdGraphSettings.BetaGraphApiBaseUrl}auditLogs/signIns?&$filter=createdDateTime ge {fromDateFormatted}";
            }
            else
            {
                query = $"{_azureAdGraphSettings.BetaGraphApiBaseUrl}auditLogs/signIns";
            }

            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(query, UriKind.Absolute)
            };

            await _authenticationProvider.AuthenticateRequestAsync(message);

            var response = await _graphServiceClient.HttpProvider.SendAsync(message);
            var responseJson = await response.Content.ReadAsStringAsync();

            var deserializedSignInData = JsonConvert.DeserializeObject<SignInData>(responseJson);

            return deserializedSignInData;
        }

        public async Task<AuditLogsData> GetAuditLogsForUsers()
        {
            var query = $"{_azureAdGraphSettings.BetaGraphApiBaseUrl}auditLogs/directoryAudits?&$filter=activityDisplayName eq 'Issue an id_token to the application' and targetResources/any(t: t/id eq '933be5ca-54e3-4c66-ab7d-083c1986c69b')";
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(query, UriKind.Absolute)
            };

            await _authenticationProvider.AuthenticateRequestAsync(message);

            var response = await _graphServiceClient.HttpProvider.SendAsync(message);
            var responseJson = await response.Content.ReadAsStringAsync();


            var deserializedAuditLogsData = JsonConvert.DeserializeObject<AuditLogsData>(responseJson);

            return deserializedAuditLogsData;
        }

        public async Task<List<Microsoft.Graph.User>> GetAllRegisteredUsers()
        {
           var users = await _graphServiceClient.Users.Request().GetAsync();
           var currentPageUsers = users.CurrentPage.ToList();
            currentPageUsers.ForEach(u => Console.WriteLine($"User: {u.Surname} with ID: {u.Id}"));
            return currentPageUsers;
        }
    }
}
