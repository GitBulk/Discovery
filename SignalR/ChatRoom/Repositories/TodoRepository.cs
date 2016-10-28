using ChatRoom.Hubs;
using ChatRoom.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChatRoom.Repositories
{
    public class TodoRepository
    {
        public IEnumerable<Todo> GetData()
        {

            using (var connection = new SqlConnection(ApplicationConnection.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Id], [Description], [Status]
                                                            FROM [Todo]", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    //dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                    dependency.OnChange += Dependency_OnChange;

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        return reader.Cast<IDataRecord>()
                            .Select(x => new Todo()
                            {
                                Id = x.GetInt32(0),
                                Description = x.GetString(1),
                                Status = x.GetString(2)
                            }).ToList();
                    }


                }
            }
        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            TodoHub.Show();
        }
    }
}