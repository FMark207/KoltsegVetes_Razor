using System.ComponentModel.DataAnnotations;

namespace KoltsegVetes_Razor.Models
{
    public class Tranzakcio : IValidatableObject
    {
        public int Id { get; set; }
        [Display (Name = "Kategoria")]
        public int KategoriaId { get; set; }

        public Kategoria? Kategoria { get; set; }

        [Display (Name = "Összeg")]
        public decimal Osszeg { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum")]
        public DateTime Datum { get; set; } = DateTime.Today;


        [StringLength (200, ErrorMessage = "A megjegyzés max 200 kar hosszu lehet")]
        [Display(Name = "Megjegyzes")]
        public string? Megjegyzes { get; set; }

        public IEnumerable<ValidationResult>
           Validate(ValidationContext validationContext)
        {
            if (Osszeg <= 0)
                yield return new ValidationResult("Az összegnek pozitívnak kell lennie!", new[] { nameof(Osszeg) });

            if (Datum.Date > DateTime.Today)
                yield return new ValidationResult("A dátum nem lehet jövőbeli!", new[] { nameof(Datum) });
        }
    }
    
}
