public class CarimboModel
{
    public TipoAto TipoAto { get; set; }

    public string Tipo { get; set; }           // Convênio, Decreto...
    public string Fonte { get; set; }           // Municipal, Estadual...
    public string Bloco { get; set; }
    public string Numero { get; set; }
    public string Nome { get; set; }
    public string Banco { get; set; }
    public string Agencia { get; set; }
    public string Conta { get; set; }
}

public enum TipoAto
{
    Resolucao,
    Decreto,
    Portaria,
    Convenio
}
