using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonManagment.Data.DataModel
{
    /// <summary>
    /// Образование
    /// </summary>
    public class Education
    {
        public int Id { get; set; }

        public int? employeeId { get; set; }
        [ForeignKey("employeeId")]
        public Employee Employee { get; set; }

        public int? typeOfEducationId { get; set; }
        [ForeignKey("typeOfEducationId")]
        public TypeOfEducation TypeOfEducation { get; set; }

        public int? educationalInstitutionId { get; set; }
        [ForeignKey("educationalInstitutionId")]
        public EducationalInstitution EducationalInstitution { get; set; }
       
        /// <summary>
        /// Дата начала обучения
        /// </summary>
        public int? dateStartEducation { get; set; }
        /// <summary>
        /// Дата окончания  обучения
        /// </summary>
        public int? dateEndEducation { get; set; }
       
        public int? specialtyId { get; set; }
        [ForeignKey("specialtyId")]
        public Specialty Specialty { get; set; }

        public int? qualificationId { get; set; }
        [ForeignKey("qualificationId")]
        public Qualification Qualification { get; set; }
        
        public int? documentTypeId { get; set; }
        [ForeignKey("documentTypeId")]
        public DocumentType DocumentType { get; set; }

        public int? diplomaDocumentId { get; set; }
        [ForeignKey("diplomaDocumentId")]
        public DiplomaDocument DiplomaDocument { get; set; }

        public ICollection<EducationDegrees> EducationDegrees { get; set; }
        public ICollection<EducationTitles> EducationTitles { get; set; }

        /// <summary>
        /// Наличие научных трудов
        /// </summary>
        public bool scientificWorks  { get; set; }
        /// <summary>
        /// Наличие изобретения
        /// </summary>
        public bool inventions { get; set; }

        public bool deleted { get; set; }
    }
}
