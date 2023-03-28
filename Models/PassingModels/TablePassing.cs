using System.Collections.Generic;
namespace MpdaTest.Models.PassingModels
{
    public class TablePassing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Opisanie { get; set; }
      
        public bool IsRec { get; set; }

        public List<TableThemePassing> TableThemes { get; set; }

    }
}
