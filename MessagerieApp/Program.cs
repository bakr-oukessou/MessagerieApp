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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Enregistre les repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<SupplierRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Récupération de la chaîne de connexion
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IUserRepository>(provider =>
	new UserRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

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

builder.Services.AddRazorPages(options =>
{
	options.Conventions.AuthorizeFolder("/"); 
	options.Conventions.AllowAnonymousToPage("/Login");
	options.Conventions.AllowAnonymousToPage("/Logout");
	options.Conventions.AllowAnonymousToPage("/RegisterSupplier"); ;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.Cookie.HttpOnly = true;
		options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
		options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
		options.LoginPath = "/Login";
		options.LogoutPath = "/Logout";
		options.AccessDeniedPath = "/AccessDenied";
	}); ;

builder.Services.AddAuthorization();

var app = builder.Build();

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
			ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
			ctx.Context.Response.Headers["Pragma"] = "no-cache";
			ctx.Context.Response.Headers["Expires"] = "-1";
		}
	});
}
else
{
	app.UseStaticFiles(); 
}

app.MapGet("/", async context =>
{
	context.Response.Redirect("/Login");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
