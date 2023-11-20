using System.ComponentModel.DataAnnotations;

namespace APISED.DTO
{
    public class UsuarioActualizarDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string profesion { get; set; }
        [Required]
        [Range(5, 100,
        ErrorMessage = "Valor {0} debe estar entre {1} y {2}.")]
        public string SKU { get; set; }
    }
}
