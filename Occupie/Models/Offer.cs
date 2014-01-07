using Occupie.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Occupie.ViewModels;
using System.Web.Mvc;

namespace Occupie.Models
{
    public class Offer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OfferId { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Видима ли е?")]
        public bool IsVisible { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Име")]
        [StringLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Референтен номер")]
        [StringLength(10)]
        public string ReferenceNumber { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Работно време")]
        public WorkTime DailyWorkTime { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Описание")]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Description { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Тип на офертата")]
        public OfferType OfferType { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Ниво на офертата")]
        public Level OfferLevel { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Основни технологии")]
        public virtual List<string> MainTechnologies { get; set; }

        [Display(Name = "Основни технологии, разделени със запетая")]
        [DataType(DataType.MultilineText)]
        // TODO: add custom validation - every word < 20 chars
        public String MainTechString
        {
            get
            {
                if (this.MainTechnologies != null)
                {
                    return String.Join(", ", this.MainTechnologies);
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    string[] technologiesArr = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    this.MainTechnologies = new List<string>();
                    foreach (var t in technologiesArr)
                    {
                        this.MainTechnologies.Add(t.Trim());
                    }
                }
                else
                {
                    this.MainTechnologies = null;
                }
            }
        }

        [Display(Name = "Допълнителни технологии")]
        public virtual List<string> AdditionalTechnologies { get; set; }

        [Display(Name = "Допълнителни технологии, разделени със запетая")]
        [DataType(DataType.MultilineText)]
        // TODO: add custom validation - every word < 20 chars
        public String AdditionalTechString
        {
            get
            {
                if (this.AdditionalTechnologies != null)
                {
                    return String.Join(", ", this.AdditionalTechnologies);
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    string[] technologiesArr = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    this.AdditionalTechnologies = new List<string>();
                    foreach (var t in technologiesArr)
                    {
                        this.AdditionalTechnologies.Add(t.Trim());
                    }
                }
                else
                {
                    this.AdditionalTechnologies = null;
                }
            }
        }

        [Display(Name = "Заплата")]
        public int? Salary { get; set; }

        public string EmployerId { get; set; }
        public virtual Employer Employer { get; set; }
    }
}