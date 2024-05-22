using Microsoft.EntityFrameworkCore;
using Book_store_1_.Controllers;
using Book_store_1_.Repository;
using Book_store_1_.Models;
using Book_store_1_.DTOs;


var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registration of services for database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// adding refrences 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


// Register repositories
// register repositories with specific model and dto  
builder.Services.AddScoped(typeof(IBaseRepository<Book, Bookdto>),typeof(BaseRepository<Book, Bookdto>));

// register mapping profile 
// Register AutoMapper with all profiles in the assembly
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
