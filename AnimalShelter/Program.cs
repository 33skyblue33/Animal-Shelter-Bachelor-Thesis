using DatabaseAdapters.Configuration;
using Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqliteDatabase(builder.Configuration);
builder.Services.AddScoped<BreedService, BreedService>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

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
