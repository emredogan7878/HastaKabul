using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Data
{
    public class Randevu
    {
        [Key]
        public int RandevuId { get; set; }
        public int HastaId { get; set; }
        public Hasta Hasta { get; set; } = null!;
        public int DoktorId { get; set; }
        public Doktor Doktor { get; set; } = null!;
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime KayitTarihi { get; set; }

    }
}
