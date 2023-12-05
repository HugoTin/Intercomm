public class Locais
{
    public int IdLocal { get; set; }

    public string? LocalNomeFantasia { get; set; }

    public string? LocalRazaoSocial { get; set; }

    public string? CNPJ { get; set; }

    public TipoLocal TipoLocal { get; set; }

    public string? IE { get; set; }

    public string? CEP { get; set; }

    public string? Logradouro { get; set; }

    public string? Bairro { get; set; }

    public string? Cidade { get; set; }

    public Estados Estado { get; set; }

    public string? Numero { get; set; }

    public string? Complemento { get; set; }

    public Ativo Ativo { get; set; }

    public List<Responsaveis>? Responsaveis { get; set; }

    public List<Telefones>? Telefones { get; set; }

    public List<Emails>? Emails { get; set; }
}

public class Responsaveis
{
    public int IdResponsavel { get; set; }

    public int CodLocal { get; set; }

    public string? Responsavel { get; set; }
}

public class Emails
{
    public int IdEmail { get; set; }

    public int CodLocal { get; set; }

    public string? Email { get; set; }
}

public class Telefones
{
    public int IdTelefone { get; set; }

    public int CodLocal { get; set; }

    public string? Telefone { get; set; }
}