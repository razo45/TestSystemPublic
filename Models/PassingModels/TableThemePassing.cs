using System.Collections.Generic;

namespace MpdaTest.Models.PassingModels
{
    public class TableThemePassing
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<TableQuesPassing> TableQues { get; set; }
    }
}
