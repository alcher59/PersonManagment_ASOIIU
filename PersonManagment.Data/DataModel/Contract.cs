using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Трудовой договор
    /// </summary>
    public class Contract
    { 
        public int Id { get; set; }
        /// <summary>
        /// Договор №
        /// </summary>
        public string? number { get; set; }
        /// <summary>
        /// Ставка
        /// </summary>
        [Required]
        public decimal rate { get; set; }
        /// <summary>
        /// Дата начала
        /// </summary>
        [Required]
        public int dateStart { get; set; }
        /// <summary>
        /// Дата окончания
        /// </summary>
        [Required]
        public int dateEnd { get; set; }
        public int? RCId { get; set; }
        [ForeignKey("RCId")]
        public ReceptionConditions ReceptionConditions { get; set; }
        /// <summary>
        /// Иные условия
        /// </summary>
        public string OtherConditions { get; set; }


        /// <summary>
        /// рабочий договор
        /// </summary>
        public string? workСontract { get; set; }


        [Required]
        public bool Deleted { get; set; }
        
        public ICollection<Recruitment> Recruitment { get; set; }
    }
}
