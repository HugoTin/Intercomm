public interface IConjuntosData
{
    public List<Conjuntos> Read();

    public List<Conjuntos> Read(String nome);

    public Conjuntos Read(int LocalId);

    public void Create(Conjuntos local);

    public void Update(Conjuntos local);
    
    public void Delete(int LocalId);


    //READ CONJUNTOS MOTORISTA
    public List<Conjuntos> ReadConjuntosMotorista(int IdMotorista);
}