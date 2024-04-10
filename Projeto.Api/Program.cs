using Projeto.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AdicionarConfiguracaoSegredos();
builder.AdicionarBanco();
builder.AdicionarMediator();
builder.AdicionarAutenticacao();
builder.AdicionarPolicies();
builder.AdicionarConfiguracaoCors();

builder.Services.AddControllers();
builder.Services.AdicionarDependenciaRepositorios();
builder.Services.AdicionarDependenciaServices();
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
app.UseCors();

app.MapControllers();

app.Run();
