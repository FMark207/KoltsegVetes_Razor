using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoltsegVetes_Razor.Data;
using KoltsegVetes_Razor.Models;

namespace KoltsegVetes_Razor.Pages.TranzakcioPages
{
    public class DeleteModel : PageModel
    {
        private readonly KoltsegVetes_Razor.Data.KoltsegvetesDbContext _context;

        public DeleteModel(KoltsegVetes_Razor.Data.KoltsegvetesDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tranzakcio Tranzakcio { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tranzakcio = await _context.Tranzakciok.Include(t => t.Kategoria).FirstOrDefaultAsync(m => m.Id == id);

            if (tranzakcio == null)
            {
                return NotFound();
            }
            else
            {
                Tranzakcio = tranzakcio;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tranzakcio = await _context.Tranzakciok.FindAsync(id);
            if (tranzakcio != null)
            {
                Tranzakcio = tranzakcio;
                _context.Tranzakciok.Remove(Tranzakcio);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
