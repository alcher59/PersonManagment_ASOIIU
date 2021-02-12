using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Трудовая деятельность (стаж)
    /// </summary>
    public class Experience
    {
        public int Id { get; set; }

        public int? employeeId { get; set; }
        [ForeignKey("employeeId")]
        public Employee Employee { get; set; }

        /// <summary>
        /// Местa работы
        /// </summary>
        public ICollection<ExperienceWork> ExperienceWork { get; set; }

        public int? positionId { get; set; }
        [ForeignKey("positionId")]
        public Position Position { get; set; }

        /// <summary>
        /// Кол ставок
        /// </summary>
        public int? countRates { get; set; }

        public bool Deleted { get; set; }
    }
}
