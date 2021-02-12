using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// График работы
    /// </summary>
    public class Shedule
    {
        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        public bool Deleted { get; set; }
        public int? StaffingTableId { get; set; }
        [ForeignKey("StaffingTableId")]
        public StaffingTable StaffingTable { get; set; }
        public ICollection<Recruitment> Recruitment { get; set; }


       // public ICollection<StaffingTable> StaffingTable { get; set; }

    }
}
