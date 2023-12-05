using Dapper;

public class ContratosData : Database, IContratosData
{

    

    /*
    ---- READ ----
    */
    public List<Contratos> Read()
    {
        string query = @"SELECT * 
                            FROM Contratos C 
                            INNER JOIN Locais L ON L.IdLocal = C.CodLocal ORDER BY Status";

        List<Contratos> lista = connection.Query<Contratos, Locais, Contratos>(query, (C, L) =>
        {
            C.Locais = L;
            return C;
        },
        splitOn: "IdLocal"
        ).ToList();

        return lista;
    }

    public List<Contratos> Read(string nome)//Razão social do local
    {

        string query = @"SELECT * 
                            FROM Contratos C 
                            INNER JOIN Locais L ON L.IdLocal = C.CodLocal WHERE L.LocalRazaoSocial LIKE @nome ORDER BY Status";

        List<Contratos> lista = connection.Query<Contratos, Locais, Contratos>(query, (C, L) =>
        {
            C.Locais = L;
            return C;
        },
        splitOn: "IdLocal",
        param: new {nome =  "%" + nome + "%"}
        ).ToList();

        return lista;

    }

    public Contratos Read(int IdContrato)
    {
        string query = @"SELECT * 
                            FROM Contratos C 
                            INNER JOIN Locais L ON L.IdLocal = C.CodLocal WHERE C.IdContrato = @IdContrato";

        Contratos lista = connection.Query<Contratos, Locais, Contratos>(query, (C, L) =>
        {
            C.Locais = L;
            return C;
        },
        splitOn: "IdLocal",
        param: new {IdContrato = IdContrato}
        ).First();

        return lista;

    }

    public List<Contratos> ReadContratos(int IdLocal, int cancel = 0)
    {
        string query = "";

        if(cancel == 0)
        {
            query = @"SELECT * 
                            FROM Contratos C 
                            INNER JOIN Locais L ON L.IdLocal = C.CodLocal WHERE CodLocal = @IdLocal AND Status = @Andamento";
        }
        else if(cancel == 1)
        {
            query = @"SELECT * 
                            FROM Contratos C 
                            INNER JOIN Locais L ON L.IdLocal = C.CodLocal WHERE CodLocal = @IdLocal";
        }

        List<Contratos> lista = connection.Query<Contratos, Locais, Contratos>(query, (C, L) =>
        {
            C.Locais = L;
            return C;
        },
        splitOn: "IdLocal",
        param: new {
            IdLocal = IdLocal,
            Andamento = Status.Andamento}
        ).AsList();

        return lista;
    }



    /*
    ---- CREATE ----
    */
    public void Create(Contratos contrato)
    {

            Console.WriteLine(contrato.DataInicio);

            string query = @"INSERT INTO Contratos
            (CodLocal, Commodity, DataInicio, Volume, ValorUnitario)
            VALUES
            (@CodLocal, @Commodity, @DataInicio, @Volume, @ValorUnitario)";

            connection.Execute(query, new{
                CodLocal = contrato.Locais.IdLocal, 
                contrato.Commodity,
                contrato.DataInicio,
                contrato.Volume,
                contrato.ValorUnitario
            });
    }



    /*
    ---- UPDATE ----
    */
    public void Update(Contratos contrato)
    {
        string query = @"UPDATE Contratos
                            SET 
                                CodLocal = @CodLocal,
                                Commodity = @Commodity,
                                DataInicio = @DataInicio,
                                Volume = @Volume,
                                VolumeAtual = VolumeAtual,
                                VolumePendente = VolumePendente,
                                ValorUnitario = @ValorUnitario,
                                Status = @Status
                            WHERE
                                IdContrato = @IdContrato";

        connection.Execute(query, new{
            CodLocal = contrato.Locais.IdLocal, 
            contrato.Commodity,
            contrato.DataInicio,
            contrato.Volume,
            contrato.ValorUnitario,
            contrato.IdContrato,
            contrato.Status
        });
    }



    /*
    ---- DELETE ----
    */
    public void Delete(int IdContrato)
    {
        string query = @"DELETE FROM Contratos WHERE IdContrato = @IdContrato";

        connection.Execute(query, new {IdContrato});
    }
    
}