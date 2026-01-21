using Microsoft.AspNetCore.Mvc;

public class CarimboController : Controller
{
    private readonly CarimboImageService _service;

    public CarimboController(CarimboImageService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Gerar(CarimboModel model)
    {
        var imagemBytes = _service.GerarCarimbo(model);

        return File(imagemBytes, "image/jpeg", "carimbo.jpg");
    }
}
