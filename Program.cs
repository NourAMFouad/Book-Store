using Microsoft.EntityFrameworkCore;
using Book_store_1_.Controllers;
using Book_store_1_.Repository;


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
builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));


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
