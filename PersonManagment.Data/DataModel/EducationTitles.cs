using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Таблица-связка для AcademicTitles/Education
    /// </summary>
    public class EducationTitles
    {
        public int Id { get; set; }

        [Required]
        public int educationId { get; set; }
        [ForeignKey("educationId")]
        public Education Education { get; set; }

        [Required]
        public int academicTitlesId { get; set; }
        [ForeignKey("academicTitlesId")]
        public AcademicTitles AcademicTitles { get; set; }

        public bool deleted { get; set; }
    }
}
