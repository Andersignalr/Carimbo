public class CarimboCreateViewModel
{
    public int BlocoId { get; set; }
    public int FonteId { get; set; }
    public int TipoAtoId { get; set; }
    public int BancoId { get; set; }

    public string Numero { get; set; }
    public string Nome { get; set; }
    public string Agencia { get; set; }
    public string Conta { get; set; }

    public List<Bloco> Blocos { get; set; } = new();
    public List<Fonte> Fontes { get; set; } = new();
    public List<TipoAto> TiposAto { get; set; } = new();
    public List<Banco> Bancos { get; set; } = new();
}
