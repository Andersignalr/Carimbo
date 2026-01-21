using Microsoft.AspNetCore.Mvc;

public class FontesController : Controller
{
    private readonly AppDbContext _context;

    public FontesController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Fontes.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Fonte model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _context.Fontes.Add(model);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var item = _context.Fontes.Find(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    public IActionResult Edit(Fonte model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _context.Fontes.Update(model);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var item = _context.Fontes.Find(id);
        if (item == null) return NotFound();

        _context.Fontes.Remove(item);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
