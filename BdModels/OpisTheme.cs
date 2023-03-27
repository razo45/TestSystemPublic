namespace MpdaTest.BdModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u1723122_u1723122.OpisTheme")]
    public partial class OpisTheme
    {
        public int ID { get; set; }

        public string ImageName { get; set; }

        public string Opis { get; set; }

        public byte[] ImageBit { get; set; }

        public int IdTheme { get; set; }

        public string TypeImage { get; set; }

        public string link { get; set; }
    }
}
