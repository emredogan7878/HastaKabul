using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Data
{
    public class Bolum
    {
        [Key]
        public int BolumId { get; set; }
        [Display(Name = "Bölüm")]
        public string? Baslik { get; set; }
    }
}
