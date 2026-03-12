using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnemPrep.Web.Filters;

public class VerificaSessaoAlunoAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var usuarioId = context.HttpContext.Session.GetString("UsuarioId");

        if (string.IsNullOrEmpty(usuarioId))
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
        }

        base.OnActionExecuting(context);
    }
}
