using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public enum EFilmGenre { action, terror, comedy, romantic, scrify }
    public class FilmGetDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DirectorName { get; set; }
    }
}
