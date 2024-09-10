using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Data
{
    public class Hasta
    {
        [Key]
        public int HastaId { get; set; }

        [Required(ErrorMessage = "Hasta adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Hasta adı en fazla 100 karakter olabilir.")]
        public string? HastaAd { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]
        public string? Telefon { get; set; }

        [Required(ErrorMessage = "TC Kimlik numarası zorunludur.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik numarası 11 haneli olmalıdır.")]
        public string? TcKimlik { get; set; }

        [Required(ErrorMessage = "Adres zorunludur.")]
        [StringLength(250, ErrorMessage = "Adres en fazla 250 karakter olabilir.")]
        public string? Adres { get; set; }
    }
}
