using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonManagment.Data.Models
{
    /// <summary>
    /// Список добавляемых/удаляемых ролей
    /// </summary>
    public class UserRoles
    {
        [JsonProperty("roles")]
        public string[] roles { get; set; }
    }
}
