namespace MpdaTest.BdModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u1723122_u1723122.TestSort")]
    public partial class TestSort
    {
        public int ID { get; set; }

        public int IDtheme { get; set; }

        public int IDques { get; set; }

        public int Number { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
