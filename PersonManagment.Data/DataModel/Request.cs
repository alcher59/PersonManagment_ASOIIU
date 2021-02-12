using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Заявка
    /// </summary>
    public class Request
    {
        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public int employeeId { get; set; }
        [ForeignKey("employeeId")]
        public Employee Employee { get; set; }
        [Required]
        public int requestCategoryId { get; set; }
        [ForeignKey("requestCategoryId")]
        public RequestCategory RequestCategory { get; set; }
        /// <summary>
        /// Кол-во
        /// </summary>
        public int? amount { get; set; }
        /// <summary>
        /// Срок исполнения
        /// </summary>
        public int? periodOfExecution { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string? description { get; set; }


        /// <summary>
        /// Дата создания заявки
        /// </summary>
        public int createdDate { get; set; }

        /// <summary>
        /// Заявка завершена
        /// </summary>
        public bool completed { get; set; }
        public bool deleted { get; set; }
    }
}
