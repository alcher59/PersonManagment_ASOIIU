using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Условия приема
    /// </summary>
    public class ReceptionConditions
    {
        public int Id { get; set; }
        [Required]
        public string placeWork { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Contract> Contract { get; set; }
    }
}
