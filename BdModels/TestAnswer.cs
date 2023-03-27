namespace MpdaTest.BdModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u1723122_u1723122.TestAnswer")]
    public partial class TestAnswer
    {
        public int ID { get; set; }

        public int IDTheme { get; set; }

        [Required]
        public string Question { get; set; }

        public bool necessarily { get; set; }
    }
}
