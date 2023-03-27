namespace MpdaTest.BdModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u1723122_u1723122.UserSelectTest")]
    public partial class UserSelectTest
    {
        [Required]
        public string Login { get; set; }

        public int TestID { get; set; }

        public bool BoolPassed { get; set; }

        public int ID { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
