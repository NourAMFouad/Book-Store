using Book_store_1_.Controllers;
using Book_store_1_.Repository;
using Book_store_1_.Models;
using Book_store_1_.DTOs;
using Book_store_1_.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;


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
// registering of repositorues, meaning a new instance will be created per request. This allows for proper management of resources and dependencies
// within each request's scope.
builder.Services.AddScoped(typeof(IBaseRepository<Book, Bookdto>), typeof(BaseRepository<Book, Bookdto>));

builder.Services.AddScoped(typeof(IBaseRepository<Category, Categorydto>),typeof(BaseRepository<Category, Categorydto>));

builder.Services.AddScoped(typeof(IBaseRepository<Admin, Admindto>) , typeof(BaseRepository<Admin, Admindto>));

builder.Services.AddScoped(typeof(IBaseRepository<Member, Memberdto>) , typeof(BaseRepository<Member, Memberdto>));

builder.Services.AddScoped(typeof(IBaseRepository<BorrowedBooks,BorrowedBookdto>) , typeof(BaseRepository<BorrowedBooks,BorrowedBookdto>));


/////Identity & Authentication

// Configure JWT settings
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));


// Add Authentication service  
// to map between them 
builder.Services.AddScoped<IAuthService, AuthService>();

// adding and configure identity 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

// to make it add authorization schema by defualt before using them endpoints
string secretKey = "QD2e4b2/K6FcnxB0AYOAFyh3JAzSVEc9ZXY/YR+L//s="; 
if (string.IsNullOrEmpty(secretKey))
{
    throw new ArgumentNullException(nameof(secretKey), "JWT secret key cannot be null or empty.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "Issuer", 
            ValidAudience = "SecureApiUser", 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) 
        };
    });
  
                
                
       

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


app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();
/*
Additional notes 
// builder: An instance of WebApplicationBuilder used to configure and build the web application.
// services: The IServiceCollection provided by builder.Services, used to register application services
//           for dependency injection.
*/

 
   
