using AzureCoreOne.AppContexts;
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
        public static void UseSampleQuestions(this IApplicationBuilder app, string path)
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
            var optionsBuilder = new
            DbContextOptionsBuilder<QuizContext>();
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
    }
}
