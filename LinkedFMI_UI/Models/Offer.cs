using LinkedFMI_UI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using LinkedFMI_UI.ViewModels;

namespace LinkedFMI_UI.Models
{
    public class Offer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OfferId { get; set; }

        [Required]
        [Display(Name = "Име")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Референтен номер")]
        public string ReferenceNumber { get; set; }

        [Required]
        [Display(Name = "Работно време")]
        public WorkTime DailyWorkTime { get; set; }

        [Required]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Тип на офертата")]

        public LinkedFMI_UI.Enums.Type OfferType { get; set; }

        [Required]
        [Display(Name = "Ниво на офертата")]
        public Level OfferLevel { get; set; }

        [Required]
        [Display(Name = "Основни технологии")]
        public virtual List<string> MainTechnologies { get; set; }
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

        public virtual List<string> AdditionalTechnologies { get; set; }
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

        public int? Salary { get; set; }

        public string EmployerId { get; set; }
        public virtual Employer Employer { get; set; }

        public Offer()
        {
        }
    }
}