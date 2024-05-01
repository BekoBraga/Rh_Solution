using RhSolution.Infra.Data.Interfaces;
using RhSolution.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Configurando o projeto para MVC
builder.Services.AddControllersWithViews();

//Capturar connectionstring
var connectionString = builder.Configuration.GetConnectionString("RhSolution");

//Enviar a connection string para a classe FuncionarioRepository
builder.Services.AddTransient<IFuncionarioRepository>(map => new FuncionarioRepository(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseRouting();
app.UseAuthorization();

// Definindo a págia inicial do projeto
app.MapControllerRoute(
        name: "default",
        pattern:"{controller=Home}/{action=Index}"
    );

app.Run();  