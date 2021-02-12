using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Вид обучения
    /// </summary>
    public class TypeOfEducation
    {
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        public bool deleted { get; set; }
        public ICollection<Education> Education { get; set; }
    }
}
