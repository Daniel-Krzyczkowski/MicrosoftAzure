using AdGraphClientTestApp.Authentication;
using AdGraphClientTestApp.Configuration;
using AdGraphClientTestApp.Data;
using AdGraphClientTestApp.Model;
using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdGraphClientTestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Microsoft Graph Test App started...");
            var userId = new Guid("6061cc39-844c-4450-842e-171de08e9817");

            AzureAdGraphSettings azureAdGraphSettings = new AzureAdGraphSettings();
            IGraphApiService graphApiService = new GraphApiService(azureAdGraphSettings);
            graphApiService.Initialize(true);

            //Console.WriteLine("Getting users which did not accepted consents...");

            //IDatabaseService databaseService = new DatabaseService();
            //var usersWhichNotAcceptedConsents = databaseService.GetUsersWithUnAcceptedConsents();

            //Console.WriteLine("\nGot users which did not accepted consents...");

            //Console.WriteLine("=================================================");

            //Console.WriteLine("Getting user login information from the recent days...");

            //DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 6);

            //var loginData = await graphApiService.GetLatestUserLoginData();
            //var distinctLoginData = loginData.value.Select(x => x.userId).Distinct();

            //foreach (var loginDataValue in loginData.value)
            //{
            //    Console.WriteLine($"Login data: {loginDataValue.createdDateTime} for user {loginDataValue.userId} {loginDataValue.userDisplayName} with provider: {loginDataValue.authenticationMethodsUsed.FirstOrDefault()}");
            //}

            //Console.WriteLine("Getting user login information from the recent days...");

            //var usersToDelete = distinctLoginData.Where(p => !usersWhichNotAcceptedConsents.Any(p2 => p2.UserId.ToString() == p)).ToList();

            //Console.WriteLine("Users which should be deleted:");

            //usersToDelete.ForEach(u => Console.WriteLine("User ID to delete: " + u));

            Console.WriteLine("=================================================");

            //var users = await graphApiService.GetAllRegisteredUsers();

            var auditLogs = await graphApiService.GetAuditLogsForUsers();

            foreach(var log in auditLogs.value)
            {
                DateTime convertedDate = DateTime.SpecifyKind(
                                                           DateTime.Parse(log.activityDateTime.ToString()),
                                                           DateTimeKind.Utc);
                DateTime dt = convertedDate.ToLocalTime();
                Console.WriteLine($"Found logins for:  with date and time: {dt}");
            }

            Console.ReadKey();
        }
    }
}
