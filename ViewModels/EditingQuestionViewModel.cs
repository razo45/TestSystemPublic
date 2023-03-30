using MpdaTest.Models.EditingModels;

namespace MpdaTest.ViewModels
{
    public class EditingQuestionViewModel
    {
        public string Type { get; set; }
        public int IdTest { get; set; }

        public EditingOpenModel editingOpenModel { get; set; }
        public EditingCloseModel editingCloseModel { get; set; }
        public EditingTableModel editingTableModel { get; set; }
    }
}
