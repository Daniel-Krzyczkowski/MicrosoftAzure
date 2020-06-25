using AdGraphClientTestApp.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AdGraphClientTestApp.Data
{
    public interface IDatabaseService
    {
        IList<NotAcceptedConsentData> GetUsersWithUnAcceptedConsents();
    }

    public class DatabaseService : IDatabaseService
    {
        private SqlConnectionStringBuilder _builder;

        public DatabaseService()
        {
            _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = "pdsantanderuniv-sql-srv.database.windows.net";
            _builder.UserID = "microserviceapp";
            _builder.Password = @"gK~Pg~\7X?9[)}W3";
            _builder.InitialCatalog = "pdsantanderuniv-sql-db";
        }

        public IList<NotAcceptedConsentData> GetUsersWithUnAcceptedConsents()
        {
            try
            {
                IList<NotAcceptedConsentData> notAcceptedConsentDataList = new List<NotAcceptedConsentData>();

                using (SqlConnection connection = new SqlConnection(_builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data for unaccepted consents:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT DISTINCT UserId ");
                    sb.Append("FROM UserConsentValues ");
                    sb.Append("WHERE Value = '0'");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0}", reader.GetGuid(0));
                                notAcceptedConsentDataList.Add(new NotAcceptedConsentData
                                {
                                    UserId = reader.GetGuid(0)
                                });
                            }
                        }
                    }
                }

                return notAcceptedConsentDataList;
            }

            catch(SqlException ex)
            {
                Console.WriteLine(nameof(SqlException) + " occurred when getting information about consents...");
                Console.WriteLine(nameof(SqlException) + " details: " + ex.Message);
                throw;
            }
        }
    }
}
