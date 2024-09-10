using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<Bolum> Bolumler => Set<Bolum>();
        public DbSet<Hasta> Hastalar => Set<Hasta>();
        public DbSet<Randevu> Randevular => Set<Randevu>();
		public DbSet<Doktor> Doktorlar => Set<Doktor>();

	}
}
