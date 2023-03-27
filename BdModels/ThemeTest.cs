namespace MpdaTest.BdModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u1723122_u1723122.ThemeTest")]
    public partial class ThemeTest
    {
        public int ID { get; set; }

        public int IDTestSistem { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
