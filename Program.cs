﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Assessment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
			
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try {
					var configuration = services.GetRequiredService<IConfiguration>();
                    DbInitializer.Initialize(configuration);
                } catch(Exception ex) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
				.UseUrls("http://0.0.0.0:5000")
				.UseStartup<Startup>()
				.Build();
    }
}
