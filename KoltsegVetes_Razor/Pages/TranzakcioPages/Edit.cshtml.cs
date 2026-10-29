using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoltsegVetes_Razor.Data;
using KoltsegVetes_Razor.Models;

namespace KoltsegVetes_Razor.Pages.TranzakcioPages
{
    public class EditModel : PageModel
    {
        private readonly KoltsegVetes_Razor.Data.KoltsegvetesDbContext _context;

        public EditModel(KoltsegVetes_Razor.Data.KoltsegvetesDbContext context)
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

            var tranzakcio =  await _context.Tranzakciok.FirstOrDefaultAsync(m => m.Id == id);
            if (tranzakcio == null)
            {
                return NotFound();
            }
            Tranzakcio = tranzakcio;
           ViewData["KategoriaId"] = new SelectList(_context.Kategoriak, "Id", "Nev");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["KategoriaId"] = new SelectList(_context.Kategoriak, "Id", "Nev");
                return Page();
            }

            _context.Attach(Tranzakcio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranzakcioExists(Tranzakcio.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TranzakcioExists(int id)
        {
            return _context.Tranzakciok.Any(e => e.Id == id);
        }
    }
}
