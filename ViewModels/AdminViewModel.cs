using MpdaTest.Models;
using System.Collections.Generic;

namespace MpdaTest.ViewModels
{
    public class AdminViewModel
    {
       public List<ListTestAdmin> listTests { get; set; }
       public NewTestModel NewTestModel { get; set; }
    }
}
