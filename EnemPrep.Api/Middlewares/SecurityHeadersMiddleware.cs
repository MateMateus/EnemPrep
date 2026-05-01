namespace EnemPrep.Api.Middlewares;

/// <summary>
/// Centraliza a injeção de Security Headers HTTP em todas as respostas da API.
/// </summary>
public class SecurityHeadersMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var headers = context.Response.Headers;

        // Impede MIME-type sniffing — evita ataques onde o browser "adivinha" o tipo do conteúdo
        headers["X-Content-Type-Options"] = "nosniff";

        // Bloqueia carregamento da API dentro de iframes — previne clickjacking
        headers["X-Frame-Options"] = "DENY";

        // Ativa filtro XSS legado de browsers mais antigos
        headers["X-XSS-Protection"] = "1; mode=block";

        // Controla de onde o browser envia o Referer — não vaza URL interna
        headers["Referrer-Policy"] = "strict-origin-when-cross-origin";

        // Desativa features sensíveis do browser que a API não precisa
        headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=(), payment=()";

        // CSP para API REST: só permite respostas sem recursos visuais
        // 'none' em tudo pois APIs não servem HTML, imagens ou scripts ao browser diretamente
        headers["Content-Security-Policy"] = "default-src 'none'; frame-ancestors 'none'";

        // Remove header que expõe a tecnologia do servidor
        headers.Remove("Server");
        headers.Remove("X-Powered-By");

        await next(context);
    }
}

public static class SecurityHeadersMiddlewareExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        => app.UseMiddleware<SecurityHeadersMiddleware>();
}
