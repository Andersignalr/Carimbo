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
        var tipoAto = _context.TiposAto.Find(vm.TipoAtoId);
        var fonte = _context.Fontes.Find(vm.FonteId);

        if (tipoAto == null || fonte == null)
            return BadRequest();

        // 🔒 REGRA DE NEGÓCIO
        //if (tipoAto.Nome == "Resolução" && fonte.Nome != "ESTADUAL")
        //{
        //    ModelState.AddModelError(
        //        "",
        //        "Resolução só pode ser da fonte Estadual."
        //    );
        //}

        //if (tipoAto.Nome == "Portaria" && fonte.Nome != "FEDERAL")
        //{
        //    ModelState.AddModelError(
        //        "",
        //        "Portaria só pode ser da fonte Federal."
        //    );
        //}
        //if (vm.EhEmenda)
        //{
        //    if (string.IsNullOrWhiteSpace(vm.NumeroProposta))
        //        ModelState.AddModelError(nameof(vm.NumeroProposta), "Informe o Nº da Proposta.");

        //    if (string.IsNullOrWhiteSpace(vm.NumeroEmenda))
        //        ModelState.AddModelError(nameof(vm.NumeroEmenda), "Informe o Nº da Emenda.");

        //    if (string.IsNullOrWhiteSpace(vm.NomeParlamentar))
        //        ModelState.AddModelError(nameof(vm.NomeParlamentar), "Informe o Parlamentar.");
        //}




        if (!ModelState.IsValid)
        {
            //vm.Blocos = _context.Blocos.ToList();
            vm.Fontes = _context.Fontes.ToList();
            vm.TiposAto = _context.TiposAto.ToList();
            vm.Bancos = _context.Bancos.ToList();

            return View("~/Views/Home/Index.cshtml", vm);
        }

        // 🔎 Busca nomes reais no banco
        var bloco = _context.Blocos.Find(vm.BlocoId);
        //var fonte = _context.Fontes.Find(vm.FonteId);
        //var tipoAto = _context.TiposAto.Find(vm.TipoAtoId);
        var banco = _context.Bancos.Find(vm.BancoId);

        if (fonte == null || tipoAto == null || banco == null)
            return BadRequest("Dados inválidos.");

        // 🧱 Modelo FINAL para imagem
        var carimbo = new CarimboModel
        {
            Bloco = vm.Bloco,
            Fonte = fonte.Nome,
            TipoAto = tipoAto.Nome,
            Numero = vm.Numero,
            Nome = vm.Nome,
            Banco = banco.Nome,
            Agencia = vm.Agencia,
            Conta = vm.Conta,
            EhEmenda = vm.EhEmenda,
            NumeroProposta = vm.NumeroProposta,
            NumeroEmenda = vm.NumeroEmenda,
            NomeParlamentar = vm.NomeParlamentar,
            Partido = vm.Partido,
            TipoEmenda = vm.TipoEmenda,
        };
        Console.WriteLine("-------------------------EhEmenda = " + vm.EhEmenda);


        var imagem = _imageService.GerarCarimbo(carimbo);

        return File(imagem, "image/png", "carimbo.png");
        //return File(imagem, "image/png");
    }
}
