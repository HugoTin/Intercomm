public interface ILocaisData
{
    public List<Locais> Read();

    public List<Locais> Read(string nome, int TipoLocal);

    public Locais Read(int LocalId);

    public List<Locais> ReadDestino();

    public List<Locais> ReadLocaisContrato();


    public void Create(Locais local);

    public void Update(Locais local);
    
    public void Delete(int LocalId);



    //RESPONSAVEIS
    public List<Responsaveis> ReadResponsaveis(int CodLocal);

    public Responsaveis CreateResponsaveis(Responsaveis responsavel);

    public int DeleteResponsaveis(int IdResponsavel);



    //EMAILS
    public List<Emails> ReadEmails(int CodLocal);

    public Emails CreateEmails(Emails Email);

    public int DeleteEmails(int IdEmail);



    //TELEFONE
    public List<Telefones> ReadTelefones(int CodLocal);

    public Telefones CreateTelefones(Telefones Telefone);

    public int DeleteTelefones(int IdTelefone);



    //LOCAIS MOTORISTA
    public List<Locais> ReadLocaisMotorista(int IdMotorista);
    public void CreateLocaisMotorista(int IdMotorista, int IdTranspor);
    public void DeleteLocaisMotorista(int IdMotorista, int IdTranspor);
}