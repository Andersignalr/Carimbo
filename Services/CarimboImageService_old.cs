using System.Drawing;
using System.Drawing.Imaging;

public class CarimboImageService_old
{
    public byte[] GerarImagem(CarimboModel m)
    {
        int largura = 400;
        int altura = 250;

        using var bmp = new Bitmap(largura, altura);
        using var g = Graphics.FromImage(bmp);

        g.Clear(Color.White);

        var fonteTitulo = new Font("Arial", 20, FontStyle.Bold);
        var fonteTexto = new Font("Arial", 14);
        var pincel = Brushes.Black;

        int y = 20;

        g.DrawRectangle(Pens.Black, 10, 10, largura - 20, altura - 20);

        g.DrawString($"{m.Tipo} - {m.Fonte}", fonteTitulo, pincel, 20, y);
        y += 40;

        g.DrawString($"Bloco: {m.Bloco}", fonteTexto, pincel, 20, y);
        y += 30;

        g.DrawString($"Número: {m.Numero}", fonteTexto, pincel, 20, y);
        y += 30;

        g.DrawString($"Nome: {m.Nome}", fonteTexto, pincel, 20, y);
        y += 30;

        g.DrawString($"Banco: {m.Banco}", fonteTexto, pincel, 20, y);
        y += 30;

        g.DrawString($"Agência: {m.Agencia}", fonteTexto, pincel, 20, y);
        y += 30;

        g.DrawString($"Conta: {m.Conta}", fonteTexto, pincel, 20, y);

        using var ms = new MemoryStream();
        bmp.Save(ms, ImageFormat.Jpeg);

        return ms.ToArray();
    }
}
