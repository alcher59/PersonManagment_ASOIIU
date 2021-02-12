using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Должность
    /// </summary>
    public class Position
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Recruitment> Recruitment { get; set; }
        public ICollection<Experience> Experience { get; set; }

        public ICollection<StaffingTable> StaffingTable { get; set; }
    }
}
