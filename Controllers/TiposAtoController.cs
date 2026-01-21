using Microsoft.AspNetCore.Mvc;

public class TiposAtoController : Controller
{
    private readonly AppDbContext _context;

    public TiposAtoController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.TiposAto.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(TipoAto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _context.TiposAto.Add(model);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var item = _context.TiposAto.Find(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    public IActionResult Edit(TipoAto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _context.TiposAto.Update(model);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var item = _context.TiposAto.Find(id);
        if (item == null) return NotFound();

        _context.TiposAto.Remove(item);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
