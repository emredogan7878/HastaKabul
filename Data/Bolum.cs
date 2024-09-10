using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Data
{
    public class Bolum
    {
        [Key]
        public int BolumId { get; set; }
        public string? Baslik { get; set; }
    }
}
