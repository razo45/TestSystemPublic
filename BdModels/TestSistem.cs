namespace MpdaTest.BdModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u1723122_u1723122.TestSistem")]
    public partial class TestSistem
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DateOpen { get; set; }

        public string DateClose { get; set; }
    }
}
