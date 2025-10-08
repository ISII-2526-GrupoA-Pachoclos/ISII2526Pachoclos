using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {

    [Required, StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
    public string nombre { get; set; } 

    [Required, StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
    public string apellido { get; set; }

    [Required, StringLength(100, ErrorMessage = "El correo electrónico no puede tener más de 100 caracteres.")]
    public string correoElectronico { get; set; }


    public IList<Oferta> oferta { get; set; }

    public IList<Compra> Compra { get; set; }

}