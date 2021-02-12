using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// документ (файл)
    /// </summary>
    public class FilesData
    {
        public int id { get; set; }
        public byte[] data { get; set; }
        public int size { get; set; }
        public bool deleted { get; set; }


        public ICollection<FilesInfoFilesData> FilesInfoFilesData { get; set; }
    }
}
