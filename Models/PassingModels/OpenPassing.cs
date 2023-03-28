namespace MpdaTest.Models.PassingModels
{
    public class OpenPassing
    {
       public string Name { get; set; }
        public string Text { get; set; }
        public int ID { get; set; }    

        public OpenPassing(string Name, string Text, int ID)
        {
            this.Name = Name;
            this.Text = Text;
            this.ID = ID;

        }
    }
}
