using Microsoft.EntityFrameworkCore;
using KoltsegVetes_Razor.Models;

namespace KoltsegVetes_Razor.Data
{
    public class KoltsegvetesDbContext : DbContext
    {
        public KoltsegvetesDbContext(DbContextOptions<KoltsegvetesDbContext> options) : base(options)
        {
        }

        public DbSet<Kategoria> Kategoriak { get; set; }
        public DbSet<Tranzakcio> Tranzakciok { get; set; }
    }
}