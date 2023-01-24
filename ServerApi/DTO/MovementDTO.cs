using System.ComponentModel.DataAnnotations;

public class MovementDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Display(Name = "Montant")]
    [Required(ErrorMessage = "Un {0} doit être fourni")]
    public double A { get; set; }

    [Display(Name = "Label")]
    [Required(ErrorMessage = "Un {0} doit être fourni")]
    [MaxLength(150, ErrorMessage = "Le {0} doit avoir une longueur maximale de {1} charactères")]
    public string? L { get; set; }
    public DateTime D { get; set; }
    public Guid IdU { get; set; }
    public bool DC { get; set; }
    public Guid IdC { get; set; }

}