using AzureCoreOne.AppContexts;
using AzureCoreOne.Models;
using AzureCoreOne.Models.Quizs;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

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
            var optionsBuilder = new DbContextOptionsBuilder<QuizContext>();
            optionsBuilder.UseInMemoryDatabase();
            using (var context = new QuizContext(optionsBuilder.Options))
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
            var optionsBuilder = new DbContextOptionsBuilder<QuizContext>();
            optionsBuilder.UseInMemoryDatabase();
            using (var context = new QuizContext(optionsBuilder.Options))
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
                context.SaveChanges();
            }
        }
    }
}
