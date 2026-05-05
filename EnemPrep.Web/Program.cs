using EnemPrep.Web.ApiClients;

var builder = WebApplication.CreateBuilder(args);

// MVC com suporte a Areas (Admin separado do aluno)
builder.Services.AddControllersWithViews();

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7001/";

builder.Services.AddTransient<EnemPrep.Web.Handlers.AuthHeaderHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

// Typed HttpClients — Web só fala com a API via HTTP
builder.Services.AddHttpClient<IMateriaApiClient, MateriaApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<EnemPrep.Web.Handlers.AuthHeaderHandler>();

builder.Services.AddHttpClient<IAssuntoApiClient, AssuntoApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<EnemPrep.Web.Handlers.AuthHeaderHandler>();

builder.Services.AddHttpClient<IQuestaoApiClient, QuestaoApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<EnemPrep.Web.Handlers.AuthHeaderHandler>();

builder.Services.AddHttpClient<IVideoAulaApiClient, VideoAulaApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<EnemPrep.Web.Handlers.AuthHeaderHandler>();

// O AuthApiClient NÃO deve ter o handler, pois é usado para fazer login e registro (sem token)
builder.Services.AddHttpClient<IAuthApiClient, AuthApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl));

builder.Services.AddHttpClient<IPerfilApiClient, PerfilApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<EnemPrep.Web.Handlers.AuthHeaderHandler>();

builder.Services.AddHttpClient<IDashboardApiClient, DashboardApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<EnemPrep.Web.Handlers.AuthHeaderHandler>();

builder.Services.AddHttpClient<EnemPrep.Web.Services.ApiClients.Interfaces.IPlanoEstudoApiClient, EnemPrep.Web.Services.ApiClients.PlanoEstudoApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<EnemPrep.Web.Handlers.AuthHeaderHandler>();

builder.Services.AddHttpClient<EnemPrep.Web.Services.ApiClients.Interfaces.IGamificacaoApiClient, EnemPrep.Web.Services.ApiClients.GamificacaoApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<EnemPrep.Web.Handlers.AuthHeaderHandler>();

builder.Services.AddHttpClient<EnemPrep.Web.Services.ApiClients.Interfaces.ISimuladoApiClient, EnemPrep.Web.Services.ApiClients.SimuladoApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<EnemPrep.Web.Handlers.AuthHeaderHandler>();

builder.Services.AddHttpClient<ILivroApiClient, LivroApiClient>(c =>
    c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<EnemPrep.Web.Handlers.AuthHeaderHandler>();

// Antiforgery: proteção CSRF
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "XSRF-TOKEN";
    options.Cookie.HttpOnly = true;
    // Lax: protege contra CSRF e funciona com redirecionamentos normais (pós-login)
    // Strict bloquearia o cookie em navegações top-level de HTTP para HTTPS
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

// Sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    // Lax: essencial para que o cookie de sessão seja enviado após redirect de login
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // HSTS: instrui browsers a só usar HTTPS por 1 ano, incluindo subdomínios
    app.UseHsts();
}

// Security Headers em todas as respostas do Web
app.Use(async (context, next) =>
{
    var headers = context.Response.Headers;
    headers["X-Content-Type-Options"] = "nosniff";
    headers["X-Frame-Options"] = "SAMEORIGIN"; // SAMEORIGIN (não DENY) pois o site pode ter iframes internos
    headers["X-XSS-Protection"] = "1; mode=block";
    headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=(), payment=()";
    // CSP permissivo para o MVC que usa CDNs (Alpine.js, etc.) e YouTube
    headers["Content-Security-Policy"] =
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline' 'unsafe-eval' cdn.jsdelivr.net cdnjs.cloudflare.com cdn.tailwindcss.com unpkg.com www.youtube.com s.ytimg.com; " +
        "style-src 'self' 'unsafe-inline' fonts.googleapis.com cdn.jsdelivr.net cdnjs.cloudflare.com; " +
        "font-src 'self' fonts.gstatic.com data:; " +
        "img-src 'self' data: https:; " +
        "connect-src 'self' ws: wss: http: https:; " +
        "frame-src 'self' www.youtube.com youtube.com;";
    headers.Remove("Server");
    await next();
});

// Configuração para Proxy Reverso (EasyPanel/Traefik)
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | 
                       Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
});

app.UseRouting();
app.UseSession(); // deve vir antes de UseAuthorization
app.UseAuthentication();
app.UseAuthorization();
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
