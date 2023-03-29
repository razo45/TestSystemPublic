using MpdaTest.Models;
using MpdaTest.Models.AdminModels;
using System.Collections.Generic;

namespace MpdaTest.ViewModels
{
    public class AdminViewModel
    {
        public List<ListTestAdmin> listTests { get; set; }
        public NewTestModel NewTestModel { get; set; }
        public CopyingTestModel CopyingTestModel { get; set; }
        public CreateUserModel CreateUserModel { get; set; }
    }
}
