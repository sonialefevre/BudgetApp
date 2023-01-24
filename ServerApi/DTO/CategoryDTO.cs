using System.ComponentModel.DataAnnotations;

public class CategoryDTO
{
    public Guid Id { get; set; }
    [Display(Name = "Label")]
    [Required(ErrorMessage = "Le {0} doit être fourni")]
    [MaxLength(150, ErrorMessage = "Le {0} ne peut être supérieur à {1} caractères")]
    public string L { get; set; } //Raison
}