using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoltsegVetes_Razor.Data;
using KoltsegVetes_Razor.Models;

namespace KoltsegVetes_Razor.Pages.TranzakcioPages
{
    public class IndexModel : PageModel
    {
        private readonly KoltsegvetesDbContext _context;

        public IndexModel(KoltsegvetesDbContext context)
        {
            _context = context;
        }

        private const int OldalMeret = 10;

        public IList<Tranzakcio> Tranzakcio { get; set; } = new List<Tranzakcio>();
        public SelectList KategoriaLista { get; set; } = default!;
        public int OsszesOldal { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Honap { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? KategoriaId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Oldal { get; set; } = 1;

        public async Task OnGetAsync()
        {
            KategoriaLista = new SelectList(await _context.Kategoriak.ToListAsync(), "Id", "Nev");

            var lekerdezes = _context.Tranzakciok.Include(t => t.Kategoria).AsQueryable();

            if (KategoriaId != null)
            {
                lekerdezes = lekerdezes.Where(t => t.KategoriaId == KategoriaId);
            }

            if (DateTime.TryParseExact(Honap, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime honapEleje))
            {
                DateTime kovetkezoHonap = honapEleje.AddMonths(1);
                lekerdezes = lekerdezes.Where(t => t.Datum >= honapEleje && t.Datum < kovetkezoHonap);
            }

            int darab = await lekerdezes.CountAsync();
            OsszesOldal = (int)Math.Ceiling(darab / (double)OldalMeret);
            if (Oldal < 1) Oldal = 1;

            Tranzakcio = await lekerdezes
                .OrderByDescending(t => t.Datum)
                .Skip((Oldal - 1) * OldalMeret)
                .Take(OldalMeret)
                .ToListAsync();
        }
    }
}
