using NoSqlDatabase.Data;
using NoSqlDatabase.UseCases.WeatherForecasts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSingleton<DatabaseContextMongoDb>()
    .AddScoped<CreateOneRandonly>()
    .AddScoped<GetAll>()
    .AddScoped<GetById>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
