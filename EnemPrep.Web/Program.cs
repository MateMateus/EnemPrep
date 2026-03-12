using EnemPrep.Web.ApiClients;

var builder = WebApplication.CreateBuilder(args);

// MVC com suporte a Areas (Admin separado do aluno)
builder.Services.AddControllersWithViews();

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7001/";

// Typed HttpClients — Web só fala com a API via HTTP
builder.Services.AddHttpClient<IMateriaApiClient, MateriaApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl));

builder.Services.AddHttpClient<IAssuntoApiClient, AssuntoApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl));

builder.Services.AddHttpClient<IQuestaoApiClient, QuestaoApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl));

builder.Services.AddHttpClient<IVideoAulaApiClient, VideoAulaApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl));

builder.Services.AddHttpClient<IMaterialApiClient, MaterialApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl));

builder.Services.AddHttpClient<IAuthApiClient, AuthApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl));

builder.Services.AddHttpClient<IPerfilApiClient, PerfilApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl));

builder.Services.AddHttpClient<IDashboardApiClient, DashboardApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl));

builder.Services.AddHttpClient<EnemPrep.Web.Services.ApiClients.Interfaces.IPlanoEstudoApiClient, EnemPrep.Web.Services.ApiClients.PlanoEstudoApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl));

builder.Services.AddHttpClient<EnemPrep.Web.Services.ApiClients.Interfaces.IGamificacaoApiClient, EnemPrep.Web.Services.ApiClients.GamificacaoApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl));

builder.Services.AddHttpClient<EnemPrep.Web.Services.ApiClients.Interfaces.ISimuladoApiClient, EnemPrep.Web.Services.ApiClients.SimuladoApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl));

builder.Services.AddHttpContextAccessor();

// Sessão (simples) para armazenar estado do aluno logado
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.MapStaticAssets();

// Rota de área Admin
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
