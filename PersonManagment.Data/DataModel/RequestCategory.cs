using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Категория заявки
    /// </summary>
    public class RequestCategory
    {
        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        public bool deleted { get; set; }
        public ICollection<Request> Request { get; set; }
    }
}
