
using System.ComponentModel.DataAnnotations;

public class UserDTO
{
    public Guid Id { get; set; }
    //Prénom
    [Display(Name = "Prénom")]
    [Required(ErrorMessage = "Un {0} doit être founi")]
    [MaxLength(30, ErrorMessage = "Le {0} ne peut pas faire plus de {1} caractères")]
    public string P { get; set; }
    //Nom
    [Display(Name = "Nom")]
    [Required(ErrorMessage = "Un {0} doit être founi")]
    [MaxLength(30, ErrorMessage = "Le {0} ne peut pas faire plus de {1} caractères")]
    public string N { get; set; }
    //email
    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "Un {0} doit être founi")]
    // [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "L'email ne respecte pas le bon format")]
    [EmailAddress(ErrorMessage = "Le {0} ne respecte pas le bon format")]
    public string E { get; set; }

    public IEnumerable<MovementDTO>? M { get; set; }

}