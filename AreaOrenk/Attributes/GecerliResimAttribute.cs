using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace AreaOrenk.Attributes
{
    public class GecerliResimAttribute : ValidationAttribute
    {
        public double MaxDosyaBoyutuMB { get; set; } = 5;
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value is IFormFile dosya)
                {
                    if (!DosyaResimMi(dosya))
                    {
                        return new ValidationResult($"Dosya Tipi :Yüklediğiniz Dosya Resim Dosyası Olamıdır.");

                    }
                    else if (dosya.Length > MaxDosyaBoyutuMB * 1024 * 1024)
                    {
                        return new ValidationResult($"Maksimium dosya boyutu : {MaxDosyaBoyutuMB} MB Olmalıdır");
                    }
                }
            }
            return ValidationResult.Success;
        }

        private bool DosyaResimMi(IFormFile dosya)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    dosya.CopyTo(ms);
                    Image.FromStream(ms); // Dosyanın resim olup olmadığını kontrol etmemizi sağlar.
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
