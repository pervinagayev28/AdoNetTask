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
    public static class DeleteDatas
    {
        public static async Task DeleteData(string ItemName, string TableName)
        {
            SqlConnection connection = new SqlConnection(Configuration.GetConfiguration("ConnectionStrings", "SqlConnectionString"));
            await connection.OpenAsync();
            string query = $"delete from {TableName} where Name = @itemname";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@itemname", ItemName);
            try
            {
                await command.ExecuteNonQueryAsync();
                MessageBox.Show("deleted");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { await connection.CloseAsync(); }

        }
    }
}
