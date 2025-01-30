using MessagerieApp.Business;
using MessagerieApp.Business.Interfaces;
using MessagerieApp.Data;
using MessagerieApp.Repositories;
using MessagerieApp.Repository.Interfaces;
using MessagerieApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Récupération de la chaîne de connexion
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Enregistrement des services pour l'injection de dépendances
builder.Services.AddRazorPages();

// Enregistrement de la connexion comme service
builder.Services.AddSingleton(connectionString);

// Enregistrement des repositories avec injection manuelle de la chaîne de connexion
builder.Services.AddScoped<IUserRepository>(provider =>
    new UserRepository(provider.GetRequiredService<string>()));
builder.Services.AddScoped<IDemandeRessourceRepository>(provider =>
    new DemandeRessourceRepository(provider.GetRequiredService<string>()));
builder.Services.AddScoped<IRessourceRepository>(provider =>
    new RessourceRepository(provider.GetRequiredService<string>()));
builder.Services.AddScoped<IDepartementRepository>(provider =>
    new DepartementRepository(provider.GetRequiredService<string>()));
builder.Services.AddScoped<ISupplierRepository>(provider =>
    new SupplierRepository(provider.GetRequiredService<string>()));
builder.Services.AddScoped<IMaintenanceDiagnosisRepository>(provider =>
    new MaintenanceDiagnosisRepository(provider.GetRequiredService<string>()));
builder.Services.AddScoped<INotificationRepository>(provider =>
    new NotificationRepository(provider.GetRequiredService<string>()));

// Enregistrement des services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDemandeRessourceService, DemandeRessourceService>();
builder.Services.AddScoped<IRessourceService, RessourceService>();
builder.Services.AddScoped<IDepartementService, DepartementService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IMaintenanceDiagnosisService, MaintenanceDiagnosisService>();
builder.Services.AddScoped<INotificationService, NotificationService>();


var app = builder.Build();

// Configuration du pipeline de requêtes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
