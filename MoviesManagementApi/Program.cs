using Microsoft.EntityFrameworkCore;
using MoviesManagementApi.Database;
using MoviesManagementApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MoviesManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(MoviesManagementDbContext))));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMovieRepository, MovieRepository>();

var app = builder.Build();

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
