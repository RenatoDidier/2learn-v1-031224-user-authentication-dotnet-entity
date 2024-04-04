using Projeto.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AdicionarConfiguracaoSegredos();
builder.AdicionarBanco();
builder.AdicionarMediator();
builder.AdicionarAutenticacao();
builder.AdicionarPolicies();

builder.Services.AddControllers();
builder.Services.AdicionarDependenciaRepositorios();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
