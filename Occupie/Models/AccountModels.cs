using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Occupie.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Потребител")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessage = "Това поле е задължително.")]
        [DataType(DataType.Password)]
        [Display(Name = "Стара парола")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Нова парола")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди новата парола")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Компания/Потребител")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Display(Name = "Запомни ме?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModelEmployer
    {
        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди паролата")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Име на компанията")]
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

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Email за връзка с администраторите на сайта")]
        [DataType(DataType.EmailAddress)]
        public string AdminEmail { get; set; }
    }

    public class RegisterModelStudent
    {
        [Required(ErrorMessage = "Това поле е задължително.")]
        [Display(Name = "Име на потребител ( от СУСИ)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди паролата")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
