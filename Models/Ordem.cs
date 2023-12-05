public class Ordem 
{
    public int IdOrdem { get; set; }

    public Contratos? Contrato { get; set; }

    public Motoristas? Motorista { get; set; }

    public Locais? Transpor { get; set; }

    public Locais? Destino { get; set; }

    public TipoConjunto TipoConjunto{ get; set; }

    public string? PlacaA { get; set; }

    public string? PlacaB { get; set; }

    public string? PlacaC { get; set; }
    
    public double? Volume { get; set; }

    public Status Status { get; set; }
}