using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoltsegVetes_Razor.Data;
using KoltsegVetes_Razor.Models;

namespace KoltsegVetes_Razor.Pages.TranzakcioPages
{
    public class CreateModel : PageModel
    {
        private readonly KoltsegVetes_Razor.Data.KoltsegvetesDbContext _context;

        public CreateModel(KoltsegVetes_Razor.Data.KoltsegvetesDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["KategoriaId"] = new SelectList(_context.Kategoriak, "Id", "Nev");
            return Page();
        }

        [BindProperty]
        public Tranzakcio Tranzakcio { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["KategoriaId"] = new SelectList(_context.Kategoriak, "Id", "Nev");
                return Page();
            }

            _context.Tranzakciok.Add(Tranzakcio);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
