using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

public class CarimboImageService
{
    private readonly Font _fonteA;
    private readonly Font _fonteB;
    private readonly Font _fonteC;


    private const int Padding = 10;
    private const int ColunaTitulo = 160;
    private const int LarguraImagem = 500;
    private const int AlturaMinLinha = 16;

    public CarimboImageService()
    {
        var family = SystemFonts.Collection.TryGet("Arial", out var arial)
            ? arial
            : SystemFonts.Collection.Families.First();

        _fonteA = family.CreateFont(14, FontStyle.Bold);
        _fonteB = family.CreateFont(12, FontStyle.Bold);
        _fonteC = family.CreateFont(10, FontStyle.Bold);

    }

    public byte[] GerarCarimbo(CarimboModel dto)
    {
        using var image = new Image<Rgba32>(LarguraImagem, 1200);
        image.Mutate(x => x.BackgroundColor(Color.White));

        float y = Padding;

        y = Linha(image, y, "FONTE:", dto.Fonte);
        y = Linha(image, y, "BLOCO:", dto.Bloco);

        // 🔥 PULO DO GATO
        y = Linha(image, y, ObterTituloNumero(dto.TipoAto), dto.Numero);

        y = Linha(image, y, "NOME:", dto.Nome);
        y = Linha(image, y, "BANCO:", dto.Banco);
        y = Linha(image, y, "AGÊNCIA:", dto.Agencia);
        y = Linha(image, y, "Nº CONTA:", dto.Conta);

        // Borda externa
        image.Mutate(ctx =>
            ctx.Draw(Color.Black, 2,
                new Rectangle(
                    0,
                    0,
                    LarguraImagem-1,
                    (int)(y+4)
                ))
        );

        image.Mutate(ctx =>
            ctx.Crop(new Rectangle(0, 0, LarguraImagem, (int)(y + Padding / 2)))
        );

        using var ms = new MemoryStream();
        image.SaveAsPng(ms);
        return ms.ToArray();
    }

    private float Linha(Image<Rgba32> image, float y, string titulo, string valor)
    {
        float xTitulo = Padding;
        float xValor = Padding + ColunaTitulo;
        float larguraValor = LarguraImagem - xValor - Padding;

        // 1️⃣ Começa tentando com fonte 16
        var fonteAtual = _fonteA;
        var valorOptions = CriarValorOptions(fonteAtual, xValor, y, larguraValor);

        var bounds = TextMeasurer.MeasureBounds(valor.ToUpper(), valorOptions);

        // 2️⃣ Se passar de 1 linha → fonte 14
        if (bounds.Height > AlturaMinLinha * 1.2f)
        {
            fonteAtual = _fonteB;
            valorOptions = CriarValorOptions(fonteAtual, xValor, y, larguraValor);
            bounds = TextMeasurer.MeasureBounds(valor.ToUpper(), valorOptions);
        }

        // 3️⃣ Se passar de 2 linhas → fonte 12
        if (bounds.Height > AlturaMinLinha * 2.2f)
        {
            fonteAtual = _fonteC;
            valorOptions = CriarValorOptions(fonteAtual, xValor, y, larguraValor);
            bounds = TextMeasurer.MeasureBounds(valor.ToUpper(), valorOptions);
        }

        float alturaLinha = Math.Max(AlturaMinLinha, bounds.Height);

        var tituloOptions = new RichTextOptions(_fonteA)
        {
            Origin = new PointF(xTitulo, y)
        };

        image.Mutate(ctx =>
        {
            ctx.DrawText(tituloOptions, titulo, Color.Black);
            ctx.DrawText(valorOptions, valor.ToUpper(), Color.Black);
        });

        return y + alturaLinha + 2;
    }

    private RichTextOptions CriarValorOptions(
    Font fonte,
    float x,
    float y,
    float largura)
    {
        return new RichTextOptions(fonte)
        {
            Origin = new PointF(x, y),
            WrappingLength = largura
        };
    }


    private string ObterTituloNumero(TipoAto tipo)
    {
        return tipo switch
        {
            TipoAto.Resolucao => "Nº RESOLUÇÃO:",
            TipoAto.Decreto => "Nº DECRETO:",
            TipoAto.Portaria => "Nº PORTARIA:",
            TipoAto.Convenio => "Nº CONVÊNIO:",
            _ => "Nº:"
        };
    }
}
