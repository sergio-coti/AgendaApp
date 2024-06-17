using AgendaApp.API.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Política de CORS
builder.Services.AddCors(options => 
{
    options.AddPolicy("DefaultPolicy", builder => 
    {
        builder.WithOrigins("http://localhost:4200", "http://localhost:5104")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//configurações do AutoMapper
builder.Services.AddAutoMapper(typeof(ProfileMap));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors("DefaultPolicy"); //Política de CORS

app.MapControllers();

app.Run();

//tornando a classe Program pública..
public partial class Program { }
