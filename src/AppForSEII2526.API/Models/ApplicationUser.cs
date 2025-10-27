using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {
    public ApplicationUser()
    {
        Reparacion = new List<Reparacion>();
        oferta = new List<Oferta>();
        Compra = new List<Compra>();
        alquilar = new List<alquilar>();
    }

    public ApplicationUser(string nombre, string apellido, string correoElectronico, string numTelefono, 
        IList<Reparacion> reparacion, IList<Oferta> oferta, IList<Compra> compra, IList<alquilar> alquilar)
    {
        this.nombre = nombre;
        this.apellido = apellido;
        this.correoElectronico = correoElectronico;
        this.numTelefono = numTelefono;
        Reparacion = reparacion;
        this.oferta = oferta;
        Compra = compra;
        this.alquilar = alquilar;
    }

    [Required, StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
    public string nombre { get; set; } 

    [Required, StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
    public string apellido { get; set; }

    [Required, StringLength(100, ErrorMessage = "El correo electrónico no puede tener más de 100 caracteres.")]
    public string correoElectronico { get; set; }


    [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "El número de teléfono no es válido.")]
    public string numTelefono { get; set; }

    public IList<Reparacion> Reparacion { get; set; }

    public IList<Oferta> oferta { get; set; }

    public IList<Compra> Compra { get; set; }

    public IList<alquilar> alquilar { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is ApplicationUser user &&
               Id == user.Id &&
               UserName == user.UserName &&
               NormalizedUserName == user.NormalizedUserName &&
               Email == user.Email &&
               NormalizedEmail == user.NormalizedEmail &&
               EmailConfirmed == user.EmailConfirmed &&
               PasswordHash == user.PasswordHash &&
               SecurityStamp == user.SecurityStamp &&
               ConcurrencyStamp == user.ConcurrencyStamp &&
               PhoneNumber == user.PhoneNumber &&
               PhoneNumberConfirmed == user.PhoneNumberConfirmed &&
               TwoFactorEnabled == user.TwoFactorEnabled &&
               EqualityComparer<DateTimeOffset?>.Default.Equals(LockoutEnd, user.LockoutEnd) &&
               LockoutEnabled == user.LockoutEnabled &&
               AccessFailedCount == user.AccessFailedCount &&
               nombre == user.nombre &&
               apellido == user.apellido &&
               correoElectronico == user.correoElectronico &&
               numTelefono == user.numTelefono &&
               EqualityComparer<IList<Reparacion>>.Default.Equals(Reparacion, user.Reparacion) &&
               EqualityComparer<IList<Oferta>>.Default.Equals(oferta, user.oferta) &&
               EqualityComparer<IList<Compra>>.Default.Equals(Compra, user.Compra) &&
               EqualityComparer<IList<alquilar>>.Default.Equals(alquilar, user.alquilar);
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(Id);
        hash.Add(UserName);
        hash.Add(NormalizedUserName);
        hash.Add(Email);
        hash.Add(NormalizedEmail);
        hash.Add(EmailConfirmed);
        hash.Add(PasswordHash);
        hash.Add(SecurityStamp);
        hash.Add(ConcurrencyStamp);
        hash.Add(PhoneNumber);
        hash.Add(PhoneNumberConfirmed);
        hash.Add(TwoFactorEnabled);
        hash.Add(LockoutEnd);
        hash.Add(LockoutEnabled);
        hash.Add(AccessFailedCount);
        hash.Add(nombre);
        hash.Add(apellido);
        hash.Add(correoElectronico);
        hash.Add(numTelefono);
        hash.Add(Reparacion);
        hash.Add(oferta);
        hash.Add(Compra);
        hash.Add(alquilar);
        return hash.ToHashCode();
    }
}