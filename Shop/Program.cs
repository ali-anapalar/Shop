using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using shop.business.Abstract;
using shop.business.Concrete;
using shop.data.Abstract;
using shop.data.Concrete.EfCore;
using Shop.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("Data Source=(local); Initial catalog=dataYedek2; trusted_connection=yes"));
builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options => {
    // password
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;

    // Lockout                
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    // options.User.AllowedUserNameCharacters = "";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = ".ShopApp.Security.Cookie"
    };
});

builder.Services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
builder.Services.AddScoped<IProductRepository, EfCoreProductRepository>();
builder.Services.AddScoped<ICartRepository, EfCoreCartRepository>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICartService, CartManager>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.


    if (app.Environment.IsDevelopment())
    {
        SeedDatabase.Seed();
        app.UseExceptionHandler("/Home/Error");

    }

    app.UseStaticFiles();
    app.UseAuthentication();
    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
        name: "checkout",
        pattern: "checkout",
        defaults: new { controller = "Cart", action = "Checkout" }
                );
        endpoints.MapControllerRoute(
        name: "cart",
        pattern: "cart",
        defaults: new { controller = "Cart", action = "Index" }
                );

        endpoints.MapControllerRoute(
        name: "adminusers",
        pattern: "admin/user/list",
        defaults: new { controller = "Admin", action = "UserList" }
       );
        endpoints.MapControllerRoute(
        name: "adminuseredit",
        pattern: "admin/user/{id?}",
        defaults: new { controller = "Admin", action = "UserEdit" }
                    );
        endpoints.MapControllerRoute(
        name: "adminroles",
        pattern: "admin/role/list",
        defaults: new { controller = "Admin", action = "RoleList" }
                    );

        endpoints.MapControllerRoute(
        name: "adminrolecreate",
        pattern: "admin/role/create",
        defaults: new { controller = "Admin", action = "RoleCreate" }
        );
        endpoints.MapControllerRoute(
        name: "adminroleedit",
        pattern: "admin/role/{id?}",
        defaults: new { controller = "Admin", action = "RoleEdit" }
                    );
        endpoints.MapControllerRoute(
        name: "adminproducts",
        pattern: "admin/products",
        defaults: new { controller = "Admin", action = "ProductList" }
        );

        endpoints.MapControllerRoute(
        name: "adminproductcreate",
        pattern: "admin/products/create",
        defaults: new { controller = "Admin", action = "ProductCreate" }
        );

        endpoints.MapControllerRoute(
        name: "adminproductedit",
        pattern: "admin/products/{id?}",
        defaults: new { controller = "Admin", action = "ProductEdit" }
        );

        endpoints.MapControllerRoute(
        name: "admincategories",
        pattern: "admin/categories",
        defaults: new { controller = "Admin", action = "CategoryList" }
       );

        endpoints.MapControllerRoute(
        name: "admincategorycreate",
        pattern: "admin/categories/create",
        defaults: new { controller = "Admin", action = "CategoryCreate" }
        );

        endpoints.MapControllerRoute(
        name: "admincategoryedit",
        pattern: "admin/categories/{id?}",
        defaults: new { controller = "Admin", action = "CategoryEdit" }
        );


        // localhost/search    
        endpoints.MapControllerRoute(
        name: "search",
        pattern: "search",
        defaults: new { controller = "Shop", action = "search" }
        );

        endpoints.MapControllerRoute(
        name: "products",
        pattern: "products/{category?}",
        defaults: new { controller = "Shop", action = "list" }
       );

        endpoints.MapControllerRoute(
        name: "productdetails",
        pattern: "{url}",
        defaults: new { controller = "Shop", action = "details" }
        );



        endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );
    });
    

app.Run();
