using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Occupie.Enums;
using Occupie.ViewModels;
using System.Web.Mvc;

namespace Occupie.Models
{
    public class Employer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int EmployerProfileId { get; set; }

        public int UserId { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Picture { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Име")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "ЕИК")]
        [StringLength(9)]
        public string EIK { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Адрес")]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Сайт")]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Display(Name = "LinkedIn")]
        [DataType(DataType.Url)]
        public string LinkedIn { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Email за връзка с администраторите на сайта")]
        [DataType(DataType.EmailAddress)]
        public string AdminEmail { get; set; }

        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Дата на създаване")]
        public int YearFounded { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [StringLength(600)]
        public string Description { get; set; }

        [Display(Name = "Брой на служителите")]
        public int NumberOfEmployees { get; set; }

        [Display(Name = "Отдели")]
        public virtual List<string> Departments { get; set; }

        [Display(Name = "Отдели, разделени със запетая")]
        // TODO: add custom validation - every word < 20 chars
        public string DepartmentsString
        {
            get
            {
                if (this.Departments != null)
                {
                    return String.Join(", ", this.Departments);
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    string[] departmentsArr = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    this.Departments = new List<string>();
                    foreach (var t in departmentsArr)
                    {
                        this.Departments.Add(t.Trim());
                    }
                }
                else
                {
                    this.Departments = null;
                }
            }
        }

        [Display(Name = "Обяви")]
        public virtual List<Offer> Offers { get; set; }
    }
}