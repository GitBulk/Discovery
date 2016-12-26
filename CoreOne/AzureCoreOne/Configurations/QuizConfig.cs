using AzureCoreOne.AppContexts;
using AzureCoreOne.Models;
using AzureCoreOne.Models.Quizs;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using AzureCoreOne.Models.CustomerManagement;

namespace AzureCoreOne.Configurations
{
    public static class QuizConfig
    {
        public static void ImportQuizData(this IApplicationBuilder app, string path)
        {
            // load a sample JSON file of questions
            string json = File.ReadAllText(Path.Combine(path,
            "questions.json"));
            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All
            };
            List<Quiz> quizzes = JsonConvert.DeserializeObject<List<Quiz>>(json, settings);
            // Configure the in-memory database option
            var optionsBuilder = new DbContextOptionsBuilder<TamContext>();
            optionsBuilder.UseInMemoryDatabase();
            using (var context = new TamContext(optionsBuilder.Options))
            {
                foreach (Quiz quiz in quizzes)
                {
                    context.Add(quiz);
                }
                context.SaveChanges();
            }
        }

        public static void ImportData(this IApplicationBuilder app)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TamContext>();
            optionsBuilder.UseInMemoryDatabase();
            using (var context = new TamContext(optionsBuilder.Options))
            {
                ImportProduct(context);
                ImportCustomer(context);
                context.SaveChanges();
            }
        }

        private static void ImportCustomer(TamContext context)
        {
            var customerOne = new Customer
            {
                Id = 1,
                FirstName = "Cris",
                LastName = "Ronaldo",
                Email = "cr7@gmail.com",
                Address = "7 Real Madrid",
                City = "Madrid",
                Phone = "0169 777 777"
            };
            var customerTwo = new Customer
            {
                Id = 2,
                FirstName = "Leo",
                LastName = "Messi",
                Email = "messi@yahoo.com",
                Address = "10 Barca",
                City = "Barcelona",
                Phone = "0168 101 010"
            };
            context.Customers.AddRange(customerOne, customerTwo);
        }

        private static void ImportProduct(TamContext context)
        {
            var product = new Product()
            {
                ProductId = 1,
                Category = "Tablet",
                Description = "iPad mini 2016 description",
                Name = "iPad mini 2016",
                Price = 200
            };
            context.Products.Add(product);
        }
    }
}
