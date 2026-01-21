using Microsoft.AspNetCore.Mvc;

public class BancosController : Controller
{
    private readonly AppDbContext _context;

    public BancosController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
        => View(_context.Bancos.ToList());
}
