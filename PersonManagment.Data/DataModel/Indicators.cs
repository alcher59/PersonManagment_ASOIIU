using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Показатели
    /// </summary>
    public class Indicators
    {
        public int id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Код
        /// </summary>
        public string code { get; set; }
        public ICollection<TimeSheet> TimeSheet { get; set; }
        public bool deleted { get; set; }
    }
}
