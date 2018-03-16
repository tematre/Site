using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TemaTre.Site.Resume
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", true)
                                .Build();

            var result = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseWebRoot("wwwroot")
                .UseConfiguration(config)
                .Build();

            return result;
        }
            
    }
}
