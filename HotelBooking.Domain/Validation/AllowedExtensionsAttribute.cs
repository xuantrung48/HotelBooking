using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ShopDienThoai.Models.Validation
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Tập tin hình ảnh không hợp lệ!";
        }
    }
}
