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
    public static class InsertDatas
    {
        public static async Task InsertData(dynamic obj)
        {
            SqlConnection connection = new SqlConnection(Configuration.GetConfiguration("ConnectionStrings", "SqlConnectionString"));
            await connection.OpenAsync();
            string query = "\r\ninsert into Books\r\n" +
                            "values(@Id,@name,@pages,@year,@Id_themes,@Id_category,@Id_author,@Id_Press,@Comment,@Quantity)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", obj.Id);
            command.Parameters.AddWithValue("@name", obj.Name);
            command.Parameters.AddWithValue("@pages", obj.Pages);
            command.Parameters.AddWithValue("@year", obj.YearPress);
            command.Parameters.AddWithValue("@Id_themes", obj.Id_Themes);
            command.Parameters.AddWithValue("@Id_category", obj.Id_Category);
            command.Parameters.AddWithValue("@Id_author", obj.Id_Author);
            command.Parameters.AddWithValue("@Id_Press", obj.Id_Press);
            command.Parameters.AddWithValue("@Comment", obj.Comment);
            command.Parameters.AddWithValue("@Quantity", obj.Quantity);

            try
            {
                await command.ExecuteNonQueryAsync();
                MessageBox.Show("inserted");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { await connection.CloseAsync(); }

        }
    }
}
