using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Вид занятости
    /// </summary>
    public class TypeOfEmployment
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool Deleted { get; set; }
        //public ICollection<Employee> Employee { get; set; }
        public ICollection<Recruitment> Recruitment { get; set; }
    }
}
