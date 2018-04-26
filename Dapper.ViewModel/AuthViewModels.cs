using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.ViewModel
{
    public class RegisterBindingModel
    {
        [Required]
        [MaxLength(300)]
        public string DisplayName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(100)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Mật khẩu xác nhận không trùng")]
        [MaxLength(100)]
        public string ConfirmPassword { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class LoginBindingModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        public string Remember { get; set; }
    }

    public class ResetPasswordBindingModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgetPasswordBindingModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Current Password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [DisplayName("Re-type New Password")]
        public string CofirmNewPassword { get; set; }
    }
}
