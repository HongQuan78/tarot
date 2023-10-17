﻿using System.ComponentModel.DataAnnotations;
using Tarot.Validate;

namespace Tarot.Data
{
    public partial class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập username.")]
        [MinLength(4, ErrorMessage = "Username phải nhiều hơn 4 kí tự.")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        public string? Fullname { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [CustomPhone()]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
        public string? Gender { get; set; }
        
        [Required(ErrorMessage = "Vui lòng chọn ngày sinh.")]
        public DateTime? Birthday { get; set; }

        public string? Role { get; set; }

        public virtual Reader? Reader { get; set; }

        public virtual ICollection<ReadingHistory> ReadingHistories { get; set; } = new List<ReadingHistory>();
    }
}