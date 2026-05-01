using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnemPrep.Web.Filters;

public class VerificaSessaoAdminAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var usuarioId = context.HttpContext.Session.GetString("UsuarioId");
        var tipoPerfil = context.HttpContext.Session.GetString("TipoPerfil");

        if (string.IsNullOrEmpty(usuarioId))
        {
            context.Result = new RedirectToActionResult("Login", "Auth", new { area = "" });
        }
        else if (tipoPerfil != "Administrador")
        {
            context.Result = new RedirectToActionResult("Index", "Dashboard", new { area = "" });
        }

        base.OnActionExecuting(context);
    }
}
