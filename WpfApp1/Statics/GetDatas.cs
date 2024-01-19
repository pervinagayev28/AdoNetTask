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
    public static  class GetDatas
    {
        public static  async Task<List<string>> GetDataFromDatabase(string query)
        {

            List<string> datas = new();
            SqlConnection connection = new SqlConnection(Configuration.GetConfiguration("ConnectionStrings", "SqlConnectionString"));
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(query, connection);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                datas.Add(reader[0].ToString()!);
            }
            await connection.CloseAsync();
            return datas;
        }


        public static async Task GetBooks(string BookName)
        {
            string query = "select \r\n\tb.Name,b.Pages,b.YearPress,t.Name as Themes,a.FirstName + ' ' +a.LastName as AuthorFullName ,p.Name as PressName,b.Comment,b.Quantity\r\n" +
                "from Books b\r\n" +
                "join Themes t on t.Id =b.Id_Themes\r\n" +
                "join Categories c on c.Id = b.Id_Category\r\n" +
                "join Authors a on a.Id = b.Id_Author\r\n" +
                "join Press p on p.Id = b.Id_Press " +
                "where b.Name = @bookname";
            List<string> datas = new();
            SqlConnection connection = new SqlConnection(Configuration.GetConfiguration("ConnectionStrings", "SqlConnectionString"));
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@bookname",BookName);   
            var reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows)
                MessageBox.Show($"not exsists the book: {BookName}");
            while (await reader.ReadAsync())
            {
                StringBuilder informs = new();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    informs.AppendLine($"{reader.GetName(i)}: {reader[i]}");
                }
                MessageBox.Show(informs.ToString(), "BookInforms");
            }
            await connection.CloseAsync();
        }
    }
}
