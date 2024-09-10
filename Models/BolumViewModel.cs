using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models
{
	public class BolumViewModel
	{
		[Key]
		public int BolumId { get; set; }
        [Required(ErrorMessage = "Bölüm başlığı zorunludur.")]
        [StringLength(100, ErrorMessage = "Bölüm başlığı en fazla 50 karakter olabilir.")]
        public string? Baslik { get; set; }
	}
}
