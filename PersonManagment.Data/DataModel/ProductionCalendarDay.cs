using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// день производственного календаря
    /// </summary>
    public class ProductionCalendarDay
    {

        public int id { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// Выходной
        /// </summary>
        public bool isHoliday { get; set; }

        /// <summary>
        /// Сокращенный рабочий режим
        /// </summary>
        public bool isShortDay { get; set; }

        /// <summary>
        /// количество рабочих часов
        /// </summary>
        public float countWorkHours { get; set; }


    }
}
