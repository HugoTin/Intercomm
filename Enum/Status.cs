using System.ComponentModel.DataAnnotations;

public enum Status
{
    [Display(Name = "Pendente")]
    Pendente,

    [Display(Name = "Andamento")]
    Andamento,

    [Display(Name = "Concluido")]
    Concluido,

    [Display(Name = "Cancelado")]
    Cancelado
}