using ForecastDesign.Commands;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Services;

namespace WpfApp1.ViewModels
{
    class MainWindowViewModel
    {


        public ObservableCollection<string> CategoryNames { get; set; } = new();
        public ObservableCollection<string> AuthorsNames { get; set; } = new();
        public ICommand CategoryComboboxSelected { get; set; }

        public MainWindowViewModel()
        {
            StartDatas();
            CategoryComboboxSelected = new Command(ExecuteCategoryComboboxSelected);
        }

        private async void StartDatas()
        {
            await FillCategoryNames();
            await FillAuthorsNames();
        }

        private async Task FillCategoryNames()
        {

            SqlConnection connection = new SqlConnection(Configuration.GetConfiguration("ConnectionStrings", "SqlConnectionString"));
            await connection.OpenAsync();

            string query = "select Name from Categories";
            SqlCommand command = new SqlCommand(query, connection);
            var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                CategoryNames.Add(reader[0].ToString()!);
            }
            await connection.CloseAsync();
        }

        private async Task FillAuthorsNames()
        {

            SqlConnection connection = new SqlConnection(Configuration.GetConfiguration("ConnectionStrings", "SqlConnectionString"));
            await connection.OpenAsync();

            string query = "select FirstName from Authors";
            SqlCommand command = new SqlCommand(query, connection);
            var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                AuthorsNames.Add(reader[0].ToString()!);
            }
            await connection.CloseAsync();
        }
        private void ExecuteCategoryComboboxSelected(object obj)
        {
            ((ComboBox)obj).IsEnabled = true;
        }
    }
}
