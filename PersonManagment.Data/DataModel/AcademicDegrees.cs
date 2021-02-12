using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Ученые степени
    /// </summary>
    public class AcademicDegrees
    {
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        public bool deleted { get; set; }

        public ICollection<EducationDegrees> EducationDegrees { get; set; }
    }
}
