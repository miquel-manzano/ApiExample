using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T1_APIREST.Models
{
    public enum EFilmGenre { action, terror, comedy, romantic, scrify }
    public class Film
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required][MaxLength(50)] 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EFilmGenre FilmGenre { get; set; }

        //Changed name like database column name
        [Required]
        public int DirectorID { get; set; }

        [ForeignKey("DirectorID")]
        public Director Director { get; set; } = null;
    }
}
