namespace MpdaTest.BdModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u1723122_u1723122.Theme")]
    public partial class Theme
    {
        public int ID { get; set; }

        public int IDTable { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
