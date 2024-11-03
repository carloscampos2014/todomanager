using Microsoft.AspNetCore.Mvc;
using TodoManager.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os � DI
builder.Services.AddServices();

// Configura��o de controladores com JSON como formato padr�o e com indenta��o para legibilidade
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserva o nome original das propriedades
        options.JsonSerializerOptions.WriteIndented = true; // Para depura��o, desabilite em produ��o para melhorar a performance
    });

// Configura��o do Swagger para documenta��o da API
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

// Configura��o de rotas com URLs em min�sculas
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configura��o do CORS (ajuste as pol�ticas conforme necess�rio)
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configura��o do pipeline de middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts(); // Adiciona cabe�alho de seguran�a em ambientes de produ��o
}

app.UseHttpsRedirection(); // Redireciona para HTTPS
app.UseCors(); // Habilita CORS
app.UseAuthorization(); // Configura autoriza��o

app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers()
        .WithMetadata(new ProducesAttribute("application/json"));
});

app.Run();
