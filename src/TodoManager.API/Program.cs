using Microsoft.AspNetCore.Mvc;
using TodoManager.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços à DI
builder.Services.AddServices();

// Configuração de controladores com JSON como formato padrão e com indentação para legibilidade
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserva o nome original das propriedades
        options.JsonSerializerOptions.WriteIndented = true; // Para depuração, desabilite em produção para melhorar a performance
    });

// Configuração do Swagger para documentação da API
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

// Configuração de rotas com URLs em minúsculas
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configuração do CORS (ajuste as políticas conforme necessário)
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configuração do pipeline de middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts(); // Adiciona cabeçalho de segurança em ambientes de produção
}

app.UseHttpsRedirection(); // Redireciona para HTTPS
app.UseCors(); // Habilita CORS
app.UseAuthorization(); // Configura autorização

app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers()
        .WithMetadata(new ProducesAttribute("application/json"));
});

app.Run();
