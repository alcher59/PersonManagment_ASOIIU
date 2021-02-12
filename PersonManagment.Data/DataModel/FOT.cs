using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Фонд оплаты труда
    /// </summary>
    public class FOT
    {
        //ФОТ = Оплата труда + Стимулирующие выплаты + Компенсационные выплаты
        public int Id { get; set; }
        public decimal? salary { get; set; }
        public decimal? incentivePayments { get; set; }
        public decimal? compensationPayments { get; set; }
        public bool Deleted { get; set; }
        public ICollection<StaffingTable> StaffingTable { get; set; }
    }
}
