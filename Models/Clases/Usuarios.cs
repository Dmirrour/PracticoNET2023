using System.ComponentModel.DataAnnotations;

namespace PractNET_2023.Models.Clases
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Nickname { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaDeNacimiento { get; set; }

        public string Descripcion { get; set; }
    }
}
