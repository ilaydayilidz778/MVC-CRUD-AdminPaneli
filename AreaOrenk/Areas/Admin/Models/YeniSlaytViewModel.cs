using AreaOrenk.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AreaOrenk.Areas.Admin.Models
{
    public class YeniSlaytViewModel
    {

        [Display(Name = "Resim Dosyası")]
        [Required(ErrorMessage = "{0} Yüklemek Zorunludur.")]
        [GecerliResim(MaxDosyaBoyutuMB = 5)]
        public IFormFile ResimDosyasi { get; set; } = null!;

        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "{0} Alanı Zorunludur.")]
        [MaxLength(50)]
        public string Baslik { get; set; } = null!;

        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "{0} Alanı Zorunludur.")]
        [MaxLength(255)]
        public string Aciklama { get; set; } = null!;

        [Display(Name = "Sıra")]
        [Required(ErrorMessage = "{0} Alanı Zorunludur..")]
        public int Sira { get; set; }
    }
}
