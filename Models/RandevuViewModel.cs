using HospitalApp.Data;

namespace HospitalApp.Models
{
	public class RandevuViewModel
	{
		public List<Randevu> HastaRandevulari { get; set; }
		public Randevu YeniRandevu { get; set; }
		public Hasta Hasta { get; set; }
	}

}
