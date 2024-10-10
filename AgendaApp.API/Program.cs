using AgendaApp.API.Configurations;
using AgendaApp.API.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configura��o para exibir os endpoints da API em caixa baixa
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Habilitando o uso do automapper no projeto
builder.Services.AddAutoMapper(typeof(ProfileMap));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura��o para que o projeto Angular possa fazer requisi��es para a API
builder.Services.AddCors(
    config => config.AddPolicy("DefaultPolicy", builder => {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    })
);

// Configura��o para autentica��o com JWT
JwtTokenConfiguration.Configure(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); //habilitar autentica��o
app.UseAuthorization();

//registrando a pol�tica do CORS
app.UseCors("DefaultPolicy");

app.MapControllers();

app.Run();
