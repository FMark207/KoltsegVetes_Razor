using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;

namespace KoltsegVetes_Razor.Models
{
    public enum KategoriaTipus
    {
        [Display(Name = "Bevétel")] Bevetel,
        [Display(Name = "Kiadas")] Kiadas
    }
    public class Kategoria
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "A nev megadasa kotelezo")]
        [StringLength(60, ErrorMessage = "A név maximum 60 kar lehet")]

        [Display (Name = "Nev")]
        public string Nev { get; set; }

        [Display(Name = "Típus")]
        public KategoriaTipus Tipus { get; set; }

        public List<Tranzakcio> Tranzakciok { get; set; } = new();



    }
}
