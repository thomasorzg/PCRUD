using System.ComponentModel.DataAnnotations;

namespace MVCPracticaArqdSoft.Models {

    public class ContactModel {

        public int Id { get; set; }

        [Required(ErrorMessage = "El campo 'Nombre' es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo 'Número de teléfono' es obligatorio")]
        public string PhoneNumber { get; set; }

        public string? Email { get; set; }

        [Required(ErrorMessage = "El campo 'Clave' es obligatorio")]
        public string Password { get; set; }
    }

}
