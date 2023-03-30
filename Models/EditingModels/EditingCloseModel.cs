using System;
using System.Collections.Generic;

namespace MpdaTest.Models.EditingModels
{
    public class EditingCloseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NewQues { get; set; }
        public bool IsRec { get; set; }

        public List<KeyValuePair<int, string>> AnsweT { get; set; }

    }

}
