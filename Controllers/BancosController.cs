using Microsoft.AspNetCore.Mvc;

public class BancosController : Controller
{
    private readonly AppDbContext _context;

    public BancosController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Bancos.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Banco model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _context.Bancos.Add(model);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var item = _context.Bancos.Find(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    public IActionResult Edit(Banco model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _context.Bancos.Update(model);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var item = _context.Bancos.Find(id);
        if (item == null) return NotFound();

        _context.Bancos.Remove(item);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
