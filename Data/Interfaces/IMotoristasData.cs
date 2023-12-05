public interface IMotoristasData
{
    public List<Motoristas> Read();

    public List<Motoristas> Read(String nome);

    public Motoristas Read(int MotoristaId);
    
    public List<Motoristas> ReadAtivo();

    public void Create(Motoristas motorista);

    public void Delete(int MotoristaId);

    public void Update(Motoristas motorista);

    /*
    ----- MOTORISTAS LOCAL -----
    */
    //READ
    public List<Motoristas> ReadMotoristasLocal(int IdLocal);
}