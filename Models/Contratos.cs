public class Contratos 
{
    public int IdContrato{ get; set; }

    public Locais? Locais { get; set; }

    public Commodities Commodity { get; set; }

    public DateTime DataInicio { get; set; }

    public double Volume { get; set; }

    public double VolumeAtual { get; set; }

    public double VolumePendente{ get; set; }

    public double ValorUnitario{ get; set; }

    public Status Status { get; set; }
}