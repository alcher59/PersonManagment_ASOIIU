using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Право на отпуск
    /// </summary>
    public class VacationEntitlement
    {
        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Recruitment> Recruitment { get; set; }

        public ICollection<VacationShedule> VacationShedule { get; set; }
        public ICollection<Vacations> Vacations { get; set; }
        public ICollection<StaffingTable> StaffingTable { get; set; }
    }
}
