using Microsoft.AspNetCore.Http;

namespace MpdaTest.Models.PassingModels
{
    public class OpisCreate
    {
        public int IDTestSistem { get; set; }
        public IFormFile FileBinar { get; set; }
        public string Opis { get; set; }
        public string Link { get; set; }
    }
}
