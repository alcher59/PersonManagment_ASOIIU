using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Причина нетрудоспособности
    /// </summary>
    public class DisablementIncapacityReason
    {
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        public bool deleted { get; set; }

        public ICollection<SickLeaves> SickLeaves { get; set; }
    }
}
