using System.ComponentModel.DataAnnotations;

namespace MiMangaBot.Models
{
    public class Manga
    {
        public int id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Autor { get; set; } = string.Empty;

        public DateTime? Fecha_Emision { get; set; }

        
        public DateTime? Fecha_Registro { get; set; }

    }
}
