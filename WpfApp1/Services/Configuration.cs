using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Services
{

    public static class Configuration
    {
        public static string GetConfiguration(string Key, string Value)
        {
            var builder = new ConfigurationBuilder().
                    AddJsonFile("appsettings.json");
            return builder.Build().GetSection(Key)[Value];
        }
    }

}
