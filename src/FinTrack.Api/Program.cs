using FinTrack.Core.Dtos;
using FinTrack.Core.Validators;
using FinTrack.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do SQLite na Infraestrutura
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=fintrack.db"));

// Registo do FluentValidation
builder.Services.AddScoped<IValidator<TransacaoCreateDto>, TransacaoCreateValidator>();

builder.Services.AddControllers();
builder.Services.AddOpenApi(); // OpenAPI nativo

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();

// Garante a criação segura do banco SQLite sem conflitos em testes paralelos
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Garante que o banco é criado apenas se não existir, evitando o erro de tabela duplicada
    db.Database.EnsureCreated();
}

app.Run();
public partial class Program { }