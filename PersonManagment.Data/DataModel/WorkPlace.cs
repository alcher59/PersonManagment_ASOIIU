using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// места работы
    /// </summary>
    public class WorkPlace
    {
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        public bool deleted { get; set; }

       public ICollection<ExperienceWork> ExperienceWork { get; set; }
    }
}
