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



        public List<PassingThemeModel> passingThemes { get; set; }  

   
    }
}
