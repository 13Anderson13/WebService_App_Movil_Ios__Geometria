using WebServiceGeometria.Contexto;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Hacer que la cadena de conexión sea accesible
builder.Services.AddApplicationServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Urls.Add("http://0.0.0.0:5267"); // puerto HTTP para el emulador
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
