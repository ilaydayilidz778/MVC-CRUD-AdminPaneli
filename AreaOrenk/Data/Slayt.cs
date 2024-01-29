using AreaOrenk.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AreaOrenk.Data
{
    public class Slayt
    {
        public int Id { get; set; }

        [Display(Name = "Resim Dosyası Yükleyein")]
        [Required(ErrorMessage ="Resim Dosyası Yüklemek Zorunludur.")]
        public string ResimYolu { get; set; } = null!;

        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "Başlık Alanı Zorunludur.")]
        [MaxLength(50)]
        public string Baslik { get; set; } = null!;

        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Başlık Alanı Zorunludur.")]
        [MaxLength(255)]
        public string Aciklama { get; set; } = null!;

        [Display(Name = "Sıra")]
        public int Sira { get; set; }
    }
}
