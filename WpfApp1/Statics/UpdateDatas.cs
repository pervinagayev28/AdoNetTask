using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Services;

namespace WpfApp1.Statics
{
    public static class UpdateDatas
    {
        public static async Task UpdateData(string OldName, string NewName)
        {
            SqlConnection connection = new SqlConnection(Configuration.GetConfiguration("ConnectionStrings", "SqlConnectionString"));
            await connection.OpenAsync();
            string query =  "update Books\r\n" +
                            "set Name = @newname\r\n" +
                            "where name = @oldname";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@oldname", OldName);
            command.Parameters.AddWithValue("@newname", NewName);
            try
            {
                await command.ExecuteNonQueryAsync();
                MessageBox.Show("updated");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { await connection.CloseAsync(); }

        }
    }
}
