using MpdaTest.BdModels;
using MpdaTest.Models.PassingModels;
using System.Collections.Generic;

namespace MpdaTest.Models
{
    public class PassingThemeModel
    {
        public string Name { get; set; }    
        public int ID { get; set; }

        public List<TestSort> testSortsList { get; set; }

        public List<OpenPassing> openPassingsList{ get; set; }
        public List<ClosePassing> ClosePassingsList { get; set; }


    }
}
