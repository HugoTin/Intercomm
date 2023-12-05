using Dapper;

public class ConjuntosData : Database, IConjuntosData
{



    /*
    ----- READ -----
    */
    public List<Conjuntos> Read()
    {
        string query = @"SELECT * 
                            FROM Conjuntos C 
                            INNER JOIN Motoristas M ON M.IdMotorista = C.CodMotorista ORDER BY M.NomeMotorista";

        List<Conjuntos> lista = connection.Query<Conjuntos, Motoristas, Conjuntos>(query, (C, M) =>
        {
            C.Motorista = M;
            return C;
        },
        splitOn: "IdMotorista"
        ).ToList();

        return lista;
    }

    public List<Conjuntos> Read(string nome)
    {
        string query = @"SELECT * 
                            FROM Conjuntos C 
                            INNER JOIN Motoristas M ON M.IdMotorista = C.CodMotorista WHERE M.NomeMotorista LIKE @nome ORDER BY M.NomeMotorista";

        List<Conjuntos> lista = connection.Query<Conjuntos, Motoristas, Conjuntos>(query, (C, M) =>
        {
            C.Motorista = M;
            return C;
        },
        splitOn: "IdMotorista",
        param: new {nome = "%" + nome + "%"}
        ).ToList();

        return lista;
    }

    public Conjuntos Read(int IdConjunto)
    {
        string query = @"SELECT * 
                            FROM Conjuntos C 
                            INNER JOIN Motoristas M ON M.IdMotorista = C.CodMotorista WHERE IdConjunto = @IdConjunto ORDER BY M.NomeMotorista";

        Conjuntos conjunto = connection.Query<Conjuntos, Motoristas, Conjuntos>(query, (C, M) =>
        {
            C.Motorista = M;
            return C;
        },
        splitOn: "IdMotorista",
        param: new {IdConjunto}
        ).First();

        return conjunto;
    }


    /*
    ----- CREATE -----
    */
    public void Create(Conjuntos conjunto)
    {
        if(conjunto.PlacaA != null) conjunto.PlacaA = conjunto.PlacaA.Replace("-", "");
        if(conjunto.PlacaB != null) conjunto.PlacaB = conjunto.PlacaB.Replace("-", "");
        if(conjunto.PlacaC != null) conjunto.PlacaC = conjunto.PlacaC.Replace("-", "");

        string query = @"INSERT INTO Conjuntos
                                (CodMotorista, TipoConjunto, PlacaA, PlacaB, PlacaC)
                            VALUES
                                (@CodMotorista, @TipoConjunto, @PlacaA, @PlacaB, @PlacaC)";

        connection.Execute(query, new{
            @CodMotorista = conjunto.Motorista.IdMotorista, 
            conjunto.TipoConjunto, 
            conjunto.PlacaA, 
            conjunto.PlacaB, 
            conjunto.PlacaC,
        });
    }



    /*
    ----- UPDATE -----
    */
    public void Update(Conjuntos conjunto)
    {
        if(conjunto.PlacaA != null) conjunto.PlacaA = conjunto.PlacaA.Replace("-", "");
        if(conjunto.PlacaB != null) conjunto.PlacaB = conjunto.PlacaB.Replace("-", "");
        if(conjunto.PlacaC != null) conjunto.PlacaC = conjunto.PlacaC.Replace("-", "");

        string query = @"UPDATE Conjuntos
                            SET CodMotorista = @CodMotorista,
                                TipoConjunto = @TipoConjunto,
                                PlacaA = @PlacaA,
                                PlacaB = @PlacaB,
                                PlacaC = @PlacaC
                            WHERE IdConjunto = @IdConjunto";

        connection.Execute(query, new{
            CodMotorista = conjunto.Motorista.IdMotorista,
            conjunto.TipoConjunto,
            conjunto.PlacaA,
            conjunto.PlacaB,
            conjunto.PlacaC,
            conjunto.IdConjunto
        });
    }



    /*
    ----- DELETE ------
    */
    public void Delete(int IdConjunto)
    {

    }



    /*
    ----- CONJUNTOS MOTORISTA
    */
    //READ
    public List<Conjuntos> ReadConjuntosMotorista(int IdMotorista)
    {
        string query = @"SELECT * FROM Conjuntos WHERE CodMotorista = @IdMotorista";

        List<Conjuntos> lista = connection.Query<Conjuntos>(query, new{ IdMotorista }).AsList();

        return lista;
    }
}