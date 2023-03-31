using System.Collections.Generic;

namespace MpdaTest.Models.PassingModels
{
    public class OpenPassing
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsRec { get; set; }
        public int ID { get; set; }

        public List<string> Otv { get; set; }


    }
}
