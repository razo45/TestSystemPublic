using MpdaTest.Models.PassingModels;
using System.Collections.Generic;

namespace MpdaTest.Models
{
    public class PassingTestModel
    {
        /// <summary>
        /// Описание курса
        /// </summary>
        public string Opisanie {get; set;}
        /// <summary>
        /// Название курса
        /// </summary>
        public string Name {get; set;}
        /// <summary>
        /// Id Курса
        /// </summary>
        public int ID { get; set;}
        public bool IsPassing {get; set;}

        public byte[] bytes { get; set; }

        public string url { get; set; }

        public CreateTheme createTheme { get; set; }

        public OpisCreate OpisCreate { get; set; }

        public CreateTable CreateTable { get; set; }
        public CreateOpen createOpen { get; set; }
        public CreateAnswer createAnswer { get; set; }  

        public List<PassingThemeModel> passingThemes { get; set; }  

   
    }
}
