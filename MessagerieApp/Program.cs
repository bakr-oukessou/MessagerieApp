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


DatabaseConnection dbHelper = new DatabaseConnection(connectionString);

// Test de la connexion
dbHelper.TestConnection();

builder.Services.AddRazorPages();

// Register Repository and Service

builder.Services.AddControllersWithViews();   
builder.Services.AddScoped<IUserRepository>(provider =>
    new UserRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDemandeRessourceRepository>(provider =>
    new DemandeRessourceRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDemandeRessourceService, DemandeRessourceService>();
builder.Services.AddScoped<IRessourceRepository>(provider =>
    new RessourceRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRessourceService, RessourceService>();
builder.Services.AddScoped<IDepartementRepository>(provider =>
    new DepartementRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDepartementService, DepartementService>();
builder.Services.AddScoped<ISupplierRepository>(provider =>
    new SupplierRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IMaintenanceDiagnosisRepository>(provider =>
    new MaintenanceDiagnosisRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMaintenanceDiagnosisService, MaintenanceDiagnosisService>();
builder.Services.AddScoped<INotificationRepository>(provider =>
    new NotificationRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IRessourceRepository, RessourceRepository>();
builder.Services.AddScoped<IRessourceService, RessourceService>();
builder.Services.AddScoped<IMaintenanceDiagnosisRepository, MaintenanceDiagnosisRepository>();
builder.Services.AddScoped<IMaintenanceDiagnosisService, MaintenanceDiagnosisService>();

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
