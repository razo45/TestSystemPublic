using MpdaTest.BdModels;
using System.Collections.Generic;
namespace MpdaTest.Models.PassingModels
{
    public class ClosePassing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRec { get; set; }
        public List<AnswerT> answerTs { get; set; }
        public int AnswerTSelect { get; set; }


    }
}
