using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoltsegVetes_Razor.Data;
using KoltsegVetes_Razor.Models;

namespace KoltsegVetes_Razor.Pages.KategoriaPages
{
    public class DeleteModel : PageModel
    {
        private readonly KoltsegVetes_Razor.Data.KoltsegvetesDbContext _context;

        public DeleteModel(KoltsegVetes_Razor.Data.KoltsegvetesDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Kategoria Kategoria { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategoriak.FirstOrDefaultAsync(m => m.Id == id);

            if (kategoria == null)
            {
                return NotFound();
            }
            else
            {
                Kategoria = kategoria;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategoriak.FindAsync(id);
            if (kategoria == null)
            {
                return NotFound();
            }

            bool vanTranzakcioja = await _context.Tranzakciok.AnyAsync(t => t.KategoriaId == id);
            if (vanTranzakcioja)
            {
                Kategoria = kategoria;
                ModelState.AddModelError(string.Empty, "Ez a kategoria nem torolheto, mert van hozza tranzakcio!");
                return Page();
            }

            _context.Kategoriak.Remove(kategoria);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
