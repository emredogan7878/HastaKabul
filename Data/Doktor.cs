using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Data
{
    public class Doktor
    {
        [Key]
        public int DoktorId { get; set; }

        [Required(ErrorMessage = "Doktor adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Doktor adı en fazla 50 karakter olabilir.")]
        public string? DoktorAd { get; set; }

        [Required(ErrorMessage = "Doktor soyadı zorunludur.")]
        [StringLength(50, ErrorMessage = "Doktor soyadı en fazla 50 karakter olabilir.")]
        public string? DoktorSoyad { get; set; }

        public string DoktorAdSoyad
        {
            get { return this.DoktorAd + " " + this.DoktorSoyad; }
        }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string? DoktorEposta { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string? DoktorTelefon { get; set; }

        [Required(ErrorMessage = "Başlama tarihi zorunludur.")]
        [DataType(DataType.Date, ErrorMessage = "Geçerli bir tarih giriniz.")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime BaslamaTarihi { get; set; }

        [Required(ErrorMessage = "Lütfen geçerli bir bölüm seçiniz.")]
        public int BolumId { get; set; }

        public Bolum? Bolum { get; set; }
    }
}
