using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Таблица-связка для Experience/WorkPlace
    /// </summary>
    public class ExperienceWork
    {
        public int Id { get; set; }

        [Required]
        public int experienceId { get; set; }
        [ForeignKey("experienceId")]
        public Experience Experience { get; set; }

        [Required]
        public int workPlaceId { get; set; }
        [ForeignKey("workPlaceId")]
        public WorkPlace WorkPlace { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public int? dateStart { get; set; }
        /// <summary>
        /// Дата окончания
        /// </summary>
        public int? dateEnd { get; set; }

        public bool deleted { get; set; }

    }
}
