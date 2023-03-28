namespace MpdaTest.Models.PassingModels
{
    public class OpenPassing
    {
        string Name { get; set; }
        string Text { get; set; }
        int ID { get; set; }    

        public OpenPassing(string Name, string Text, int ID)
        {
            this.Name = Name;
            this.Text = Text;
            this.ID = ID;

        }
    }
}
