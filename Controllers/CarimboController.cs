using Microsoft.AspNetCore.Mvc;

public class CarimboController : Controller
{
    private readonly AppDbContext _context;
    private readonly CarimboImageService _imageService;

    public CarimboController(
        AppDbContext context,
        CarimboImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }

    // =========================
    // GET
    // =========================
    [HttpGet]
    public IActionResult Criar()
    {
        var vm = new CarimboCreateViewModel
        {
            Blocos = _context.Blocos.ToList(),
            Fontes = _context.Fontes.ToList(),
            TiposAto = _context.TiposAto.ToList(),
            Bancos = _context.Bancos.ToList()
        };

        return View(vm);
    }

    // =========================
    // POST
    // =========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Gerar(CarimboCreateViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Blocos = _context.Blocos.ToList();
            vm.Fontes = _context.Fontes.ToList();
            vm.TiposAto = _context.TiposAto.ToList();
            vm.Bancos = _context.Bancos.ToList();

            return View("Criar", vm);
        }

        // 🔎 Busca nomes reais no banco
        var bloco = _context.Blocos.Find(vm.BlocoId);
        var fonte = _context.Fontes.Find(vm.FonteId);
        var tipoAto = _context.TiposAto.Find(vm.TipoAtoId);
        var banco = _context.Bancos.Find(vm.BancoId);

        if (fonte == null || tipoAto == null || banco == null)
            return BadRequest("Dados inválidos.");

        // 🧱 Modelo FINAL para imagem
        var carimbo = new CarimboModel
        {
            Bloco = bloco.Nome,
            Fonte = fonte.Nome,
            TipoAto = tipoAto.Nome,
            Numero = vm.Numero,
            Nome = vm.Nome,
            Banco = banco.Nome,
            Agencia = vm.Agencia,
            Conta = vm.Conta
        };

        var imagem = _imageService.GerarCarimbo(carimbo);

        return File(imagem, "image/png");
    }
}
