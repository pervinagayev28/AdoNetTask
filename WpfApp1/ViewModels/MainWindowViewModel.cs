using ForecastDesign.Commands;
using ForecastDesign.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Services;
using WpfApp1.Statics;

namespace WpfApp1.ViewModels
{
    class MainWindowViewModel : ServiceINotifyPropertyChanged
    {
        private ObservableCollection<string> categoryNames = new();
        private ObservableCollection<string> authorsNames = new();

        public ObservableCollection<string> CategoryNames { get => categoryNames; set { categoryNames = value; OnPropertyChanged(); } }
        public ObservableCollection<string> AuthorsNames { get => authorsNames; set { authorsNames = value; OnPropertyChanged(); } }
        public ICommand CategoryComboboxSelected { get; set; }
        public ICommand CommandSearchButton { get; set; }
        public ICommand CommandDeleteButton { get; set; }
        public ICommand CommandUpdateButton { get; set; }
        public ICommand CommandInsertButton { get; set; }

        public MainWindowViewModel()
        {
            StartDatas();
            CategoryComboboxSelected = new Command(ExecuteCategoryComboboxSelected);
            CommandSearchButton = new Command(ExecuteCommandSearchButton, CanExecuteCommandSearchButton);
            CommandDeleteButton = new Command(ExecuteCommandDeleteButton, CanExecuteCommandDeleteButton);
            CommandUpdateButton = new Command(ExecuteCommandUpdateButton, CanExecuteCommandUpdateButton);
            CommandInsertButton = new Command(ExecuteCommandInsertButton, CanExecuteCommandInsertButton);
        }

        private bool CanExecuteCommandInsertButton(object obj) =>
                    !string.IsNullOrEmpty(((TextBox)(((StackPanel)obj).FindName("id_textbox"))).Text)
                    && !string.IsNullOrEmpty(((TextBox)(((StackPanel)obj).FindName("Pages_textbox"))).Text)
                    && !string.IsNullOrEmpty(((TextBox)(((StackPanel)obj).FindName("Id_Themes_textbox"))).Text)
                    && !string.IsNullOrEmpty(((TextBox)(((StackPanel)obj).FindName("Id_Category_textbox"))).Text)
                    && !string.IsNullOrEmpty(((TextBox)(((StackPanel)obj).FindName("Id_Author_textbox"))).Text)
                    && !string.IsNullOrEmpty(((TextBox)(((StackPanel)obj).FindName("Id_Press_textbox"))).Text)
                    && !string.IsNullOrEmpty(((TextBox)(((StackPanel)obj).FindName("Comment_textbox"))).Text);

        private async void ExecuteCommandInsertButton(object obj)
        {
            var type = new
            {
                Id = ((TextBox)(((StackPanel)obj).FindName("id_textbox"))).Text,
                Name = ((TextBox)(((StackPanel)obj).FindName("Name_textbox"))).Text,
                Pages = ((TextBox)(((StackPanel)obj).FindName("Pages_textbox"))).Text,
                YearPress = ((TextBox)(((StackPanel)obj).FindName("YearPress_textbox"))).Text,
                Id_Themes = ((TextBox)(((StackPanel)obj).FindName("Id_Themes_textbox"))).Text,
                Id_Category = ((TextBox)(((StackPanel)obj).FindName("Id_Category_textbox"))).Text,
                Id_Author = ((TextBox)(((StackPanel)obj).FindName("Id_Author_textbox"))).Text,
                Id_Press = ((TextBox)(((StackPanel)obj).FindName("Id_Press_textbox"))).Text,
                Comment = ((TextBox)(((StackPanel)obj).FindName("Comment_textbox"))).Text,
                Quantity = ((TextBox)(((StackPanel)obj).FindName("Quantity_textbox"))).Text
            };
            await InsertDatas.InsertData(type);

        }

        private bool CanExecuteCommandUpdateButton(object obj) =>
             !string.IsNullOrEmpty(((TextBox)(((StackPanel)obj).FindName("update_oldname_textbox"))).Text)
                && !string.IsNullOrEmpty(((TextBox)(((StackPanel)obj).FindName("update_newname_textbox"))).Text);




        private async void ExecuteCommandUpdateButton(object obj) =>
                    await UpdateDatas.UpdateData(((TextBox)(((StackPanel)obj).FindName("update_oldname_textbox"))).Text,
                                        ((TextBox)(((StackPanel)obj).FindName("update_newname_textbox"))).Text);


        private bool CanExecuteCommandDeleteButton(object obj) =>
            !string.IsNullOrEmpty(obj.ToString());

        private async void ExecuteCommandDeleteButton(object obj) =>
            await DeleteDatas.DeleteData(obj.ToString()!, "Books");

        private bool CanExecuteCommandSearchButton(object obj) =>
            !string.IsNullOrEmpty(obj.ToString());

        private async void ExecuteCommandSearchButton(object obj) =>
            await GetDatas.GetBooks(obj.ToString()!);


        private async void StartDatas()
        {
            CategoryNames = new(await GetDatas.GetDataFromDatabase("select Name from Categories"));
            AuthorsNames = new(await GetDatas.GetDataFromDatabase("select FirstName from Authors"));
        }


        private void ExecuteCategoryComboboxSelected(object obj) =>
                        ((ComboBox)obj).IsEnabled = true;



    }
}
