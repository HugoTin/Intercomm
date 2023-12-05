using System.ComponentModel.DataAnnotations;
    
public enum TipoLocal
{
    [Display(Name = "Usina")]
    Usina,

    [Display(Name = "Base")]
    Base,

    [Display(Name = "Posto")]
    Posto,

    [Display(Name = "Transportadora")]
    Transportadora
}