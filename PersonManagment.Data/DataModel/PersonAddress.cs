using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Адреса
     /// </summary>
    public class PersonAddress
    {
        public int Id { get; set; }
        /// <summary>
        /// адрес по прописке 
        /// </summary>
        public string RegistrationAddress { get; set; }
        /// <summary>
        /// дата регистрации 
        /// </summary>
        public int RegistrationDate { get; set; }
        /// <summary>
        /// адрес места проживания
        /// </summary>
        public string ResidenceAddress { get; set; }
        /// <summary>
        /// адрес за пределами рф
        /// </summary>
        public string OutsideAddress { get; set; }
        /// <summary>
        /// адрес для информирования
        /// </summary>
        public string InformationAddress { get; set; }
        public bool Deleted { get; set; }
        public PersonData PersonData { get; set; }
    }
}
