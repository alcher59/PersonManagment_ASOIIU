using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Статус
    /// </summary>
    public class Status
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
