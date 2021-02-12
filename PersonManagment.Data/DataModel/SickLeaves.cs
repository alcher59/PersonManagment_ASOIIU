using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Больничные листы
    /// </summary>
    public class SickLeaves
    {
        public int id { get; set; }
        public int accrualId { get; set; }
        [ForeignKey("accrualId")]
        public Accruals Accruals { get; set; }
       
        /// <summary>
        /// Причина нетрудоспособности
        /// </summary>
        public int disablementIncapacityReasonId { get; set; }
        [ForeignKey("disablementIncapacityReasonId")]
        public DisablementIncapacityReason DisablementIncapacityReason { get; set; }
        
        /// <summary>
        /// Дата начала
        /// </summary>
        public int dateStart { get; set; }
        
        /// <summary>
        /// Дата окончания
        /// </summary>
        public int dateEnd { get; set; }
        public bool deleted { get; set; }
    }
}
