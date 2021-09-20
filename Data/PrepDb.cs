﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public  static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(),isProd);
            }
        }
        private static void SeedData(AppDbContext context,bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("Applying Migrations ...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine($"Could not run Migrations : {Ex.Message}");
                }
               
            }
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data");

                context.Platforms.AddRange(
                    new Platform() { Name="Dot Net", Publisher="Microsoft",Cost="Free"},
                    new Platform() { Name = "SQL Sever Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                    );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have Data");
            }
        }
    }
}
