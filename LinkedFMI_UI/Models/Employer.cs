using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using LinkedFMI_UI.Enums;
using LinkedFMI_UI.ViewModels;
using System.Web.Mvc;

namespace LinkedFMI_UI.Models
{
    public class Employer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int EmployerProfileId { get; set; }

        public int UserId { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Picture { get; set; }

        [Required]
        [Display(Name = "Име")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Адрес")]
        //[StringLength(256)]
        public string Adress { get; set; }

        [Required]
        [Display(Name = "Сайт")]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Display(Name = "LinkedIn")]
        [DataType(DataType.Url)]
        public string LinkedIn { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public int YearFounded { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Брой на работниците")]
        public int NumberOfEmployees { get; set; }

        [Display(Name = "Отдели")]
        public virtual List<string> Departments { get; set; }
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