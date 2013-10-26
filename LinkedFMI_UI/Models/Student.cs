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
    public class Student
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentProfileId { get; set; }

        public int UserId { get; set; }

        //[ForeignKey("UserId")]
        //public virtual UserProfile UserProfile { get; set; }

        #region Main Info

        [DataType(DataType.Upload)]
        public byte[] Picture { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name="Име")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Блог")]
        public string Blog { get; set; }

        [DataType(DataType.Url)]
        public string Facebook { get; set; }

        [DataType(DataType.Url)]
        public string LinkedIn { get; set; }

        [DataType(DataType.Url)]
        public string Twitter { get; set; }

        [DataType(DataType.Url)]
        public string GooglePlus { get; set; }

        [DataType(DataType.Url)]
        public string Github { get; set; }

        [DataType(DataType.Upload)]
        public byte[] CV { get; set; }

        #endregion

        #region Education

        public virtual HighSchoolEdu HighSchoolInfo { get; set; }

        public virtual FMIEdu FMIInfo { get; set; }

        #endregion

        #region Experience

        public virtual List<Job> Jobs { get; set; }

        public virtual List<Project> Projects { get; set; }

        [Display(Name = "Предпочитание за работно време")]
        public WorkTime WorkTimePreference { get; set; }

        [Display(Name = "Статус")]
        public WorkStatus WorkStatus { get; set; }

        #endregion

        #region Personal

        private DateTime? dateOfBirth;
        [Display(Name = "Дата на раждане")]
        public DateTime? DateOfBirth
        {
            get
            {
                return (this.dateOfBirth != null) ? this.dateOfBirth : null;
            }
            set
            {
                this.dateOfBirth = value;
            }
        }

        public string GetDateOfBirth()
        {
            if (this.dateOfBirth == null)
            {
                return null;
            }
            else
            {
                return this.dateOfBirth.Value.ToString("MM.dd.yyyy");
            }
        }
        [Display(Name = "Град")]
        public string Town { get; set; }

        public virtual List<Language> Languages { get; set; }

        public virtual List<Technology> Technologies { get; set; }

        public virtual List<string> Interests { get; set; }

        [Display(Name = "Интереси")]
        public String InterestsString
        {
            get
            {
                if (this.Interests != null)
                {
                    return String.Join(", ", this.Interests);
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    string[] interestsArr = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    this.Interests = new List<string>();
                    foreach (var interest in interestsArr)
                    {
                        this.Interests.Add(interest.Trim());
                    }
                }
                else
                {
                    this.Interests = null;
                }
            }
        }

        #endregion

        public Student()
        {
        }

        public StudentProfileViewModel ToViewModel()
        {
            var viewModel = new StudentProfileViewModel();
            viewModel.StudentProfileId = this.StudentProfileId;
            viewModel.Blog = this.Blog;
            viewModel.CV = this.CV;
            viewModel.Email = this.Email;
            viewModel.Facebook = this.Facebook;
            viewModel.FirstName = this.FirstName;
            viewModel.LastName = this.LastName;
            viewModel.LinkedIn = this.LinkedIn;
            viewModel.Picture = this.Picture;
            viewModel.Twitter = this.Twitter;

            viewModel.Education = new EducationViewModel
            {
                Jobs = this.Jobs,
                Projects = this.Projects,
                WorkTimePreference = this.WorkTimePreference
            };

            viewModel.Experience = new ExperienceViewModel
            {

            };

            return viewModel;
        }
    }
}

