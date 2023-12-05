public interface IOrdemData
{
    public List<Ordem> Read();

    public Ordem Read(int OrdemId);

    public void Create(Ordem ordem);

    public void Delete(int OrdemId);

    public void Update(Ordem ordem);
}