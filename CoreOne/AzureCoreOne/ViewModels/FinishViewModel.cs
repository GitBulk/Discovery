using AzureCoreOne.Models.Quizs;
using System.Collections.Generic;

namespace AzureCoreOne.ViewModels
{
    public class FinishViewModel
    {
        public Quiz Quiz { get; set; }
        public Dictionary<int, string> Answers { get; set; }
        public int CorrectAnswers { get; set; }
    }
}
