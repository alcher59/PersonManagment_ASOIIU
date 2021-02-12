using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Таблица-связка для AcademicDegrees/Education
    /// </summary>
    public class EducationDegrees
    {
        public int Id { get; set; }

        [Required]
        public int educationId { get; set; }
        [ForeignKey("educationId")]
        public Education Education { get; set; }

        [Required]
        public int academicDegreesId { get; set; }
        [ForeignKey("academicDegreesId")]
        public AcademicDegrees AcademicDegrees { get; set; }

        public bool deleted { get; set; }
    }
}
