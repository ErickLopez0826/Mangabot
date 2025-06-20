using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiMangaBot.Models
{
    [Table("Prestamos")]
    public class Prestamo
    {
        [Key]
        [Column("id_prestamo")]
        public int IdPrestamo { get; set; }

        [Required]
        [Column("name_customer")]
        public string NameCustomer { get; set; } = string.Empty;

        [Required]
        [Column("id_manga")]
        public int IdManga { get; set; }

        [Required]
        [Column("return_date")]
        public DateTime ReturnDate { get; set; }

        [Column("loan_date")]
        public DateTime LoanDate { get; set; } = DateTime.Now;
    }
}
