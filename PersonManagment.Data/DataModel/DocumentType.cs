using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Тип документа
    /// </summary>
    public class DocumentType
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public bool Deleted { get; set; }

        public ICollection<PersonData> PersonData { get; set; }
        public ICollection<Education> Education { get; set; }
        
    }
}
