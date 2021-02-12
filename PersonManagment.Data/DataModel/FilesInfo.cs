using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// список всех форм документов
    /// </summary>
    public class FilesInfo
    {
        public int id { get; set; }
        /// <summary>
        /// Имя документа
        /// </summary>
        public string title { get; set; }
        public string? comment { get; set; }
        public bool deleted { get; set; }

        public ICollection<FilesInfoFilesData> FilesInfoFilesData { get; set; }
    }
}
