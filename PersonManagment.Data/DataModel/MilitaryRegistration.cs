using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Воинский учёт
    /// </summary>
    public class MilitaryRegistration
    {
        public int id { get; set; }
        public int? employeeId { get; set; }
        [ForeignKey("employeeId")]
        public Employee Employee { get; set; }

        /// <summary>
        /// Категория запаса
        /// </summary>
        [Required]
        public int stockCategoryId { get; set; }
        [ForeignKey("stockCategoryId")]
        public StockCategory StockCategory { get; set; }

        /// <summary>
        /// Воинское звание
        /// </summary>
        [Required]
        public int militaryRankId { get; set; }
        [ForeignKey("militaryRankId")]
        public MilitaryRank MilitaryRank { get; set; }

        /// <summary>
        /// Состав (профиль)
        /// </summary>
        [Required]
        public int militaryProfileId { get; set; }
        [ForeignKey("militaryProfileId")]
        public MilitaryProfile MilitaryProfile { get; set; }

        /// <summary>
        /// Полное кодовое обозначение ВУС
        /// </summary>
        public string? VUS { get; set; }

        /// <summary>
        /// Категория годности к военной службе
        /// </summary>
        [Required]
        public int militaryFitnessCategoryId { get; set; }
        [ForeignKey("militaryFitnessCategoryId")]
        public MilitaryFitnessCategory MilitaryFitnessCategory { get; set; }

        /// <summary>
        /// Наименование военного комиссариата по месту жительства
        /// </summary>
        public string? nameOfCommissariat { get; set; }

        /// <summary>
        /// Состоит на воинском учете (общем, специальном)
        /// </summary>
        [Required]
        public int? typeMilitaryRegistrationId { get; set; }
        [ForeignKey("typeMilitaryRegistrationId")]
        public TypeMilitaryRegistration TypeMilitaryRegistration { get; set; }
        [Required]
        public bool deleted { get; set; }
    }
}
