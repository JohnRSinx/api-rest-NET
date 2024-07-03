using Microsoft.EntityFrameworkCore;
using dataContext;
using configs;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o suporte para documentação de API com o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona suporte para controladores (Controllers)
builder.Services.AddControllers();

// Configura o DbContext para usar o SQLite com a string de conexão definida nas configurações
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Adiciona as configurações de autenticação personalizadas
builder.AddAuthConfigs();

// Adiciona outras configurações de serviços personalizados
builder.AddServicesConfigs();

var app = builder.Build();

// Configuração para desenvolvimento: habilita o Swagger e a UI do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilita a redireção de HTTP para HTTPS
app.UseHttpsRedirection();

// Mapeia as rotas para os controladores
app.MapControllers();

// Inicia o aplicativo
app.Run();
