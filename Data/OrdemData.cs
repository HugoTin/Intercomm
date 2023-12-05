using Dapper;

public class OrdemData : Database, IOrdemData
{

    public List<Ordem> Read()
    {   
        string query = @"SELECT * FROM OrdensDeCarregamento O
                            INNER JOIN Contratos C ON C.IdContrato = O.CodContrato
                            INNER JOIN Locais L ON L.IdLocal = C.CodLocal
                            INNER JOIN Locais D ON D.IdLocal = O.CodDestino
                            INNER JOIN Locais T ON T.IdLocal = O.CodTransportadora
                            INNER JOIN Motoristas M ON M.IdMotorista = O.CodMotorista";

        List<Ordem> lista = connection.Query<Ordem>(query,
        new[]{
            typeof(Ordem),
            typeof(Contratos),
            typeof(Locais),
            typeof(Locais),
            typeof(Locais),
            typeof(Motoristas)
        },
        objects =>
        {
            var ordem = objects[0] as Ordem;
            var contrato = objects[1] as Contratos;
            var local = objects[2] as Locais;
            var destino = objects[3] as Locais;
            var transpor = objects[4] as Locais;
            var motorista = objects[5] as Motoristas;

            ordem.Contrato =  contrato;
            ordem.Contrato.Locais = local;
            ordem.Destino = destino;
            ordem.Transpor = transpor;
            ordem.Motorista = motorista;

            return ordem;
        },
        splitOn: "IdContrato, IdLocal, IdLocal, IdLocal, IdMotorista"
        ).ToList();

        return lista;
    }

    public Ordem Read(int IdOrdem)
    {
        string query = @"SELECT * FROM OrdensDeCarregamento O
                            INNER JOIN Contratos C ON C.IdContrato = O.CodContrato
                            INNER JOIN Locais L ON L.IdLocal = C.CodLocal
                            INNER JOIN Locais D ON D.IdLocal = O.CodDestino
                            INNER JOIN Locais T ON T.IdLocal = O.CodTransportadora
                            INNER JOIN Motoristas M ON M.IdMotorista = O.CodMotorista
                            WHERE O.IdOrdem = @IdOrdem";

        Ordem ordem = connection.Query<Ordem>(query,
        new[]{
            typeof(Ordem),
            typeof(Contratos),
            typeof(Locais),
            typeof(Locais),
            typeof(Locais),
            typeof(Motoristas)
        },
        objects =>
        {
            var ordem = objects[0] as Ordem;
            var contrato = objects[1] as Contratos;
            var local = objects[2] as Locais;
            var destino = objects[3] as Locais;
            var transpor = objects[4] as Locais;
            var motorista = objects[5] as Motoristas;

            ordem.Contrato =  contrato;
            ordem.Contrato.Locais = local;
            ordem.Destino = destino;
            ordem.Transpor = transpor;
            ordem.Motorista = motorista;

            return ordem;
        },
        splitOn: "IdContrato, IdLocal, IdLocal, IdLocal, IdMotorista",
        param: new{IdOrdem}
        ).First();

        return ordem;
    }

    public void Create(Ordem ordem)
    {
        if(ordem.PlacaA != null) ordem.PlacaA = ordem.PlacaA.Replace("-", "");
        if(ordem.PlacaB != null) ordem.PlacaB = ordem.PlacaB.Replace("-", "");
        if(ordem.PlacaC != null) ordem.PlacaC = ordem.PlacaC.Replace("-", "");

        string query = @"INSERT INTO OrdensDeCarregamento
                                (CodContrato, CodDestino, CodTransportadora, CodMotorista, TipoConjunto, PlacaA, PlacaB, PlacaC, Volume, Status)
                            VALUES
                                (@CodContrato, @CodDestino, @CodTransportadora, @CodMotorista, @TipoConjunto, @PlacaA, @PlacaB, @PlacaC, @Volume, 0)";

        connection.Execute(query, new {
            CodContrato = @ordem.Contrato.IdContrato,
            CodDestino = @ordem.Destino.IdLocal,
            CodTransportadora = @ordem.Transpor.IdLocal,
            CodMotorista = @ordem.Motorista.IdMotorista,
            ordem.TipoConjunto,
            ordem.PlacaA,
            ordem.PlacaB,
            ordem.PlacaC,
            ordem.Volume
        });
    }

    public void Delete(int IdOrdem)
    {

    }

    public void Update(Ordem ordem)
    {
        if(ordem.PlacaA != null) ordem.PlacaA = ordem.PlacaA.Replace("-", "");
        if(ordem.PlacaB != null) ordem.PlacaB = ordem.PlacaB.Replace("-", "");
        if(ordem.PlacaC != null) ordem.PlacaC = ordem.PlacaC.Replace("-", "");
        
        string query = @"UPDATE OrdensDeCarregamento SET 
                            CodContrato = @CodContrato,
                            CodDestino = @CodDestino,
                            CodTransportadora = @CodTransportadora,
                            CodMotorista = @CodMotorista,
                            TipoConjunto = @TipoConjunto,
                            PlacaA = @PlacaA,
                            PlacaB = @PlacaB,
                            PlacaC = @PlacaC,
                            Volume = @Volume,
                            Status = @Status
                        WHERE IdOrdem = @IdOrdem";

        connection.Execute(query, new {
            CodContrato = @ordem.Contrato.IdContrato,
            CodDestino = @ordem.Destino.IdLocal,
            CodTransportadora = @ordem.Transpor.IdLocal,
            CodMotorista = @ordem.Motorista.IdMotorista,
            ordem.TipoConjunto,
            ordem.PlacaA,
            ordem.PlacaB,
            ordem.PlacaC,
            ordem.Volume,
            ordem.Status,
            ordem.IdOrdem
        });
    }
}