namespace MpdaTest.BdModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u1723122_u1723122.AnswerT")]
    public partial class AnswerT
    {
        public int ID { get; set; }

        public int IDTestAnswer { get; set; }

        public bool Correct { get; set; }

        [Required]
        public string Text { get; set; }

        public int NumberOfSelected { get; set; }

        public string TextUser { get; set; }
    }
}
