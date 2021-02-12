using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Категория годности к военной службе
    /// </summary>
    public class MilitaryFitnessCategory
    {
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        public bool deleted { get; set; }
        public ICollection<MilitaryRegistration> MilitaryRegistration { get; set; }
    }
}
