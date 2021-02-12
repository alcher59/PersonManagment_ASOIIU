using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Справочник стран (мест рождений)
    /// </summary>
    public class Country
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public ICollection<DocumentPassportData> DocumentPassportData { get; set; }
    }
}
