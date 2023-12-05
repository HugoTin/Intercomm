using Dapper;

public class MotoristasData : Database, IMotoristasData 
{


    
    /* 
    ---- READ ----
    */
    public List<Motoristas> Read()
    {
        string query = @"SELECT * FROM Motoristas ORDER BY Ativo ASC, NomeMotorista";

        List<Motoristas> motoristas = (List<Motoristas>)connection.Query<Motoristas>(query);

        return motoristas;
    }

    public List<Motoristas> Read(String nome)
    {
        string query = @"SELECT * FROM Motoristas WHERE NomeMotorista LIKE @nome ORDER BY NomeMotorista";

        List<Motoristas> motoristas = connection.Query<Motoristas>(query, new{nome = "%" + nome + "%"}).ToList();

        return motoristas;
    }

    public Motoristas Read(int IdMotorista)
    {
        string query = @"SELECT * FROM Motoristas WHERE IdMotorista = @IdMotorista ORDER BY NomeMotorista";

        Motoristas motorista = connection.Query<Motoristas>(query, new{ IdMotorista}).FirstOrDefault();

        return motorista;
    }

    public List<Motoristas> ReadAtivo()
    {
        string query = @"SELECT * FROM Motoristas WHERE Ativo = @Sim ORDER BY NomeMotorista";

        List<Motoristas> motoristas = (List<Motoristas>)connection.Query<Motoristas>(query, new {Ativo.Sim});

        return motoristas;
    }



    /*
    ----- CREATE -----
    */
    public void Create(Motoristas motorista)
    {
        if (motorista.CPF != null )motorista.CPF = motorista.CPF.Replace(".", "").Replace("/", "").Replace("-", "");

        string query = @"INSERT INTO Motoristas
            (NomeMotorista, CPF, CNH)
            VALUES (@NomeMotorista, @CPF, @CNH)
        ";

        connection.Execute(query, motorista);
    }



    /*
    ----- DELETE -----
    */
    public void Delete(int IdMotorista)
    {
        string query = @"DELETE FROM Motoristas Where IdMotorista = @IdMotorista";

        connection.Execute(query, IdMotorista);
    }



    /*
    ----- UPDATE -----
    */
    public void Update(Motoristas motorista)
    {
        if (motorista.CPF != null )motorista.CPF = motorista.CPF.Replace(".", "").Replace("/", "").Replace("-", "");

        string query = @" UPDATE Motoristas 
            SET NomeMotorista = @NomeMotorista,
                CPF = @CPF,
                CNH = @CNH,
                Ativo = @Ativo
            WHERE IdMotorista = @IdMotorista
        ";

        connection.Execute(query, motorista);
    }



    /*
    ----- MOTORISTAS LOCAL-----
    */
    //READ
    public List<Motoristas> ReadMotoristasLocal(int IdLocal)
    {
        string query = @"SELECT * FROM Motoristas M
                            INNER JOIN Locais_Motoristas LM ON LM.CodLocal = @IdLocal 
                            AND LM.CodMotorista = M.IdMotorista
                            AND Ativo = @Sim";

        List<Motoristas> lista = connection.Query<Motoristas>(query, new {IdLocal, Ativo.Sim}).AsList();

        return lista;
    }
}