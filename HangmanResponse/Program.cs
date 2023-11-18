//using HangmanResponse.Persistence;

using HangmanResponse.Persistence;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost", "https://localhost", "http://localhost:8080", "https://localhost:7281")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<States>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowLocalhost");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
