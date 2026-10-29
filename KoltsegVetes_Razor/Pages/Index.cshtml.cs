using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoltsegVetes_Razor.Data;
using KoltsegVetes_Razor.Models;

namespace KoltsegVetes_Razor.Pages
{
    public record KategoriaOsszeg(string Nev, KategoriaTipus Tipus, decimal Osszeg);

    public class IndexModel : PageModel
    {
        private readonly KoltsegvetesDbContext _context;

        public IndexModel(KoltsegvetesDbContext context)
        {
            _context = context;
        }

        public decimal Bevetel { get; set; }
        public decimal Kiadas { get; set; }
        public decimal Egyenleg { get; set; }
        public string HonapNeve { get; set; } = "";
        public List<KategoriaOsszeg> Osszesito { get; set; } = new();

        public async Task OnGetAsync()
        {
            DateTime honapEleje = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime kovetkezoHonap = honapEleje.AddMonths(1);
            HonapNeve = honapEleje.ToString("yyyy. MMMM");

            List<Tranzakcio> eHavi = await _context.Tranzakciok
                .Include(t => t.Kategoria)
                .Where(t => t.Datum >= honapEleje && t.Datum < kovetkezoHonap)
                .ToListAsync();

            Bevetel = eHavi.Where(t => t.Kategoria!.Tipus == KategoriaTipus.Bevetel).Sum(t => t.Osszeg);
            Kiadas = eHavi.Where(t => t.Kategoria!.Tipus == KategoriaTipus.Kiadas).Sum(t => t.Osszeg);
            Egyenleg = Bevetel - Kiadas;

            Osszesito = eHavi
                .GroupBy(t => new { t.Kategoria!.Nev, t.Kategoria.Tipus })
                .OrderBy(g => g.Key.Tipus)
                .ThenBy(g => g.Key.Nev)
                .Select(g => new KategoriaOsszeg(g.Key.Nev, g.Key.Tipus, g.Sum(t => t.Osszeg)))
                .ToList();
        }
    }
}
