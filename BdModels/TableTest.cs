namespace MpdaTest.BdModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u1723122_u1723122.TableTest")]
    public partial class TableTest
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public int IDTheme { get; set; }

        public bool necessarily { get; set; }

        public string Desp { get; set; }
    }
}
