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
    public class DetailsModel : PageModel
    {
        private readonly KoltsegVetes_Razor.Data.KoltsegvetesDbContext _context;

        public DetailsModel(KoltsegVetes_Razor.Data.KoltsegvetesDbContext context)
        {
            _context = context;
        }

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
    }
}
