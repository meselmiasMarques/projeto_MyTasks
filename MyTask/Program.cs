using MyTask.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DbContextImpacta>();

var app = builder.Build();


app.MapControllers();
app.Run();
