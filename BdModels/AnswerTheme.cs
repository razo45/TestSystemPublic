namespace MpdaTest.BdModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u1723122_u1723122.AnswerTheme")]
    public partial class AnswerTheme
    {
        public int ID { get; set; }

        public int IDTheme { get; set; }

        public int IDQuestion { get; set; }

        [Required]
        public string AnswerText { get; set; }
    }
}
