// Define o Builder da aplicação
var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container
builder.Services.AddControllersWithViews();

//Adiciona o serviço de carimbo usando injeção de dependência
builder.Services.AddScoped<CarimboImageService>();

// Instancia a aplicação de fato
var app = builder.Build();

// Configura a pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Habilita roteamento com redirecionamentos
app.UseHttpsRedirection();
app.UseRouting();

// importa autorização, mas não foi usado ainda
app.UseAuthorization();

// mapeia os arquivos estáticos
app.MapStaticAssets();

//Mapeia as rotas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

//Roda a aplicação
app.Run();
