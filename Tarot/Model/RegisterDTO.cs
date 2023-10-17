using System.ComponentModel.DataAnnotations;
using Tarot.Validate;

namespace Tarot.Model
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập username.")]
        [MinLength(4, ErrorMessage = "Username phải nhiều hơn 4 kí tự.")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải dài hơn 6 kí tự.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu.")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        public string? Fullname { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [CustomPhone()]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày sinh.")]
        public DateTime? Birthday { get; set; }
    }
}
