using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Контактные данные
    /// </summary>
    public class PersonContacts
    {
        public int Id { get; set; }
        /// <summary>
        /// Номер телфона 
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// домашний номер телфона 
        /// </summary>
        [Phone]
        public string HomePhoneNumber { get; set; }
        /// <summary>
        /// рабочий номер телфона 
        /// </summary>
        [Phone]
        public string WorkPhoneNumber { get; set; }
        /// <summary>
        /// Эл. почта
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
        public bool Deleted { get; set; }
        public PersonData PersonData { get; set; }

    }
}
