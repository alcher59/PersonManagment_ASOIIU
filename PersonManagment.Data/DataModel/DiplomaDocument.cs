using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Документ об образовании
    /// </summary>
    public class DiplomaDocument
    {
        //Диплом
        public int id { get; set; }
        [Required]
        public int serial { get; set; }
        [Required]
        public int number { get; set; }
        public bool deleted { get; set; }

        public ICollection<Education> Education { get; set; }

    }
}
