using MessagerieApp.Business;
using MessagerieApp.Business.Interfaces;
using MessagerieApp.Data;
using MessagerieApp.Models;
using MessagerieApp.Repositories;
using MessagerieApp.Repository;
using MessagerieApp.Repository.Interfaces;
using MessagerieApp.Services;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Register Repository and Service
builder.Services.AddScoped<IUserRepository>(provider =>
    new UserRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserService, UserService>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();     
builder.Services.AddScoped<IRessourceRepository>(provider =>
    new RessourceRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRessourceService, RessourceService>();
builder.Services.AddScoped<DatabaseConnection>();
builder.Services.AddScoped<RessourceRepository>();
// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Création d'un helper pour exécuter des commandes SQL
DatabaseConnection dbHelper = new DatabaseConnection(connectionString);

// Test de la connexion
dbHelper.TestConnection();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
