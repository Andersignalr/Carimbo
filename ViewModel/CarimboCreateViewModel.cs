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

    public List<Bloco> Blocos { get; set; }
    public List<Fonte> Fontes { get; set; }
    public List<TipoAto> TiposAto { get; set; }
    public List<Banco> Bancos { get; set; }
}
