using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.Utilities
{
    public static class ConfigurationManager
    {
        static public IConfigurationRoot AppSettings { get; set; }

        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            AppSettings = builder.Build();

            //Console.WriteLine($"option1 = {Configuration["option1"]}");
            //Console.WriteLine($"option2 = {Configuration["option2"]}");
            //Console.WriteLine(
            //    $"option1 = {Configuration["subsection:suboption1"]}");
        }

    }
}
