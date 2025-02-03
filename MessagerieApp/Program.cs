using MessagerieApp.Business;
using MessagerieApp.Business.Interfaces.MasterData;
using MessagerieApp.Business.Interfaces.TransactionData;
using MessagerieApp.Business.Interfaces.TransversalData;
using MessagerieApp.Data;
using MessagerieApp.Repositories;
using MessagerieApp.Repository.Interfaces.MasterData;
using MessagerieApp.Repository.Interfaces.TransactionData;
using MessagerieApp.Repository.Interfaces.TransversalData;
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

if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            // Disable caching for static files in development
            ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
            ctx.Context.Response.Headers["Pragma"] = "no-cache";
            ctx.Context.Response.Headers["Expires"] = "-1";
        }
    });
}
else
{
    app.UseStaticFiles(); // Use default caching in production
}

app.MapGet("/", async context =>
{
    context.Response.Redirect("/Dashboard");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();