public class CarimboModel
{
    public string Fonte { get; set; }
    public string? Bloco { get; set; }
    public string TipoAto { get; set; }
    public string Numero { get; set; }
    public string Nome { get; set; }
    public string Banco { get; set; }
    public string Agencia { get; set; }
    public string Conta { get; set; }

    public string? NumeroProposta { get; set; }
    public string? NumeroEmenda { get; set; }
    public string? NomeParlamentar { get; set; }
    public string? Partido { get; set; }
    public string? TipoEmenda { get; set; }

    public bool EhEmenda {  get; set; }
}
