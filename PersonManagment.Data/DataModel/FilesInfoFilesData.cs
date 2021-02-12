using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// таблица связка документ (FilesData)- (Files) список всех форм документов
    /// </summary>
    public class FilesInfoFilesData
    {
        public int id { get; set; }

        /// <summary>
        /// тип формы документа
        /// </summary>
        public int filesInfoId { get; set; }
        [ForeignKey("filesInfoId")]
        public FilesInfo FilesInfo { get; set; }

        /// <summary>
        /// файл
        /// </summary>
        public int filesDataId { get; set; }
        [ForeignKey("filesDataId")]
        public FilesData FilesData { get; set; }

        [Required]
         public int date { get; set; }
        /// <summary>
        /// версия документа
        /// </summary>
        [Required]
        public int version { get; set; }

        /// <summary>
        /// является ли актуальным документом
        /// </summary>
        public bool isActual { get; set; }
        public bool deleted { get; set; }
    }
}
