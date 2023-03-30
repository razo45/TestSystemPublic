using System.Collections.Generic;

namespace MpdaTest.Models.EditingModels
{
    public class EditingTableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool IsRec { get; set; }


        public string NewQues { get; set; }
        public string NewAnswer { get; set; }
        public List<KeyValuePair<int, string>> AnswerList { get; set; }
        public List<KeyValuePair<int, string>> QuesList { get; set; }
    }
}
