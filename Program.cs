using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// CONFIGURAÇÃO ENTITY FRAMEWORK COM ORACLE
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");

builder.Services.AddDbContext<SpaceCare.Infra.Data.AppDbContext>(options =>
    options.UseOracle(connectionString));

builder.Services.AddScoped<SpaceCare.Services.TuristaService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ENDPOINT DE TESTE DE CONEXÃO COM BANCO FIAP
app.MapGet("/api/teste-conexao", (IConfiguration configuration) =>
{
    var connectionString = configuration.GetConnectionString("OracleConnection");
    
    using var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(connectionString);
    
    try
    {
        connection.Open();
        return Results.Ok(new { status = "Sucesso", mensagem = "Conexão com o Oracle da FIAP realizada com êxito!" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Falha ao conectar no Oracle: {ex.Message}");
    }
    finally
    {
        connection.Close();
    }
});

app.Run();
