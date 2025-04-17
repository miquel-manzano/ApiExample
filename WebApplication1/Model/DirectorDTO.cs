using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class DirectorDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; } = string.Empty;

        public List<FilmGetDTO>? Films { get; set; }

    }
}
