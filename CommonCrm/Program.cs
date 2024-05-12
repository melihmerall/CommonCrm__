using System.Globalization;
using CommonCrm.BackgroundServices;
using CommonCrm.Business.Services;
using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.AppUser;

using CommonCrm.Data.Repositories.Abstract;
using CommonCrm.Data.Repositories.Concrete;
using CommonCrm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.EnableRetryOnFailure()));

builder.Services.AddDbContext<IdentityContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.EnableRetryOnFailure()));



//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Identity ve Authorization servislerini ekleme
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
	.AddEntityFrameworkStores<IdentityContext>()
	.AddDefaultUI()
	.AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequiredLength = 8;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;

	options.Password.RequiredUniqueChars = 0;

	//options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	//options.Lockout.MaxFailedAccessAttempts = 5;
	//options.Lockout.AllowedForNewUsers = true;
	
});

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CurrencyBackgroundService>();

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<AttributeService>();
builder.Services.AddScoped<ProductUnitService>();



builder.Services.AddAuthorization(options =>
{
	options.DefaultPolicy = new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.Build();
	options.AddPolicy("AdminPolicy", policy =>
	{
		policy.RequireRole("Admin");
		policy.RequireClaim("Permission", "CreateProduct", "UpdateProduct", "DeleteProduct");
	});

	options.AddPolicy("UserPolicy", policy =>
	{
		policy.RequireRole("User");
		policy.RequireClaim("Permission", "ViewProduct");
	});
	
});
var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.HttpOnly = true;
	options.LoginPath = "/Auth/Login";
	options.AccessDeniedPath = "/Auth/AccessDenied";
	options.SlidingExpiration = true;
	options.Cookie.Name = ".Crm.Identity.Token";
	options.ExpireTimeSpan = TimeSpan.FromDays(1);
});

//cors
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAnyOriginPolicy",
		builder =>
		{
			builder
				.AllowAnyOrigin() // Tüm kaynaklardan gelen isteklere izin ver
				.AllowAnyMethod() // Tüm HTTP metotlarına izin ver (GET, POST, PUT, DELETE, vb.)
				.AllowAnyHeader(); // Tüm HTTP başlıklarına izin ver
		});
});

builder.Services.AddScoped<IAuthorizationHandler, RoleAndClaimAuthorizationHandler>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient();
builder.Services.AddHostedService<CurrencyBackgroundService>();
builder.Services.AddSession();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseCors("AllowAnyOriginPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Auth}/{action=Login}/{id?}");
app.Run();
