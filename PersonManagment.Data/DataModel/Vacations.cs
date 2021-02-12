using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Отпуска
    /// </summary>
    public class Vacations
    {
        public int id { get; set; }
        public int accrualId { get; set; }
        [ForeignKey("accrualId")]
        public Accruals Accruals { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public int dateStart { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public int dateEnd { get; set; }

        /// <summary>
        /// Основание
        /// </summary>
        public int vacationEntitlementId { get; set; }
        [ForeignKey("vacationEntitlementId")]
        public VacationEntitlement VacationEntitlement { get; set; }

        /// <summary>
        /// Вид отпуска (ежегодный, учебный, без сохранения заработной платы и др.)
        /// </summary>
        public int? vacationTypeId { get; set; }
        [ForeignKey("vacationTypeId")]
        public VacationType VacationType { get; set; }

        public ICollection<StaffingTable> StaffingTable { get; set; }
        public bool deleted { get; set; }
    }
}
