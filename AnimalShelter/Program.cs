using DatabaseAdapters.Configuration;
using ExternalAdapters;
using Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqliteDatabase(builder.Configuration);
builder.Services.AddExternalAdapters();

builder.Services.AddScoped<IBreedService, BreedService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IAdoptionRequestService, AdoptionRequestService>();
builder.Services.AddScoped<IDotationService, DotationService>();
builder.Services.AddScoped<IConversationService, ConversationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3001") 
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials(); 
        });
});


var app = builder.Build();


app.UseCors("AllowReactApp");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
