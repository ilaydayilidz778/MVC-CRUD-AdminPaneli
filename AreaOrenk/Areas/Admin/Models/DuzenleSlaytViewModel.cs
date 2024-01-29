using AreaOrenk.Attributes;
using AreaOrenk.Data;
using System.ComponentModel.DataAnnotations;

namespace AreaOrenk.Areas.Admin.Models
{
    public class DuzenleSlaytViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Resim Dosyası Yükleyein")]
        [GecerliResim(MaxDosyaBoyutuMB = 5)]
        public IFormFile? ResimDosyasi { get; set; } = null!;
        public string ResimYolu { get; set; } = null!;

        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "{0} Alanı Zorunludur.")]
        [MaxLength(50)]
        public string Baslik { get; set; } = null!;

        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "{0} Alanı Zorunludur.")]
        [MaxLength(255)]
        public string Aciklama { get; set; } = null!;

        [Display(Name = "Sıra")]
        [Required(ErrorMessage = "{0} Alanı Zorunludur")]
        public int Sira { get; set; }
    }
}
