using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Оплата труда
    /// </summary>
    public class Salary
    {
        public int Id { get; set; }
        [Required]
        public decimal salary { get; set; }
        
        public string title { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Recruitment> Recruitment { get; set; }

    }
}
