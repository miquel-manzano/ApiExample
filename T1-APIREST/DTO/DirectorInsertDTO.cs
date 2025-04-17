using System.ComponentModel.DataAnnotations;

namespace T1_APIREST.DTO
{
    public class DirectorInsertDTO
    {

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;
    }
}
