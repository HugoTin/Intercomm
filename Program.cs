// WebApplication
var builder = WebApplication.CreateBuilder(args);   

// middlewares (adiciono)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession();

//
builder.Services.AddTransient<IConjuntosData, ConjuntosData>();
builder.Services.AddTransient<IContratosData, ContratosData>();
builder.Services.AddTransient<ILocaisData, LocaisData>();
builder.Services.AddTransient<IMotoristasData, MotoristasData>();
builder.Services.AddTransient<IOrdemData, OrdemData>();
builder.Services.AddTransient<INotaFiscalData, NotaFiscalData>();
builder.Services.AddTransient<IUserData, UserData>();

var app = builder.Build();

// middlewares (configuro)
app.UseSession();
app.UseStaticFiles();

app.MapControllerRoute("default", "/{controller=home}/{action=index}/{id?}");

app.Run();