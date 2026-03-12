using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Controllers;

[VerificaSessaoAluno]
public class MateriaisController(IMaterialApiClient materialClient) : Controller
{
    public async Task<IActionResult> ListarPorAssunto(Guid assuntoId, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId")))
            return RedirectToAction("Login", "Auth");

        var materiais = await materialClient.GetByAssuntoAsync(assuntoId, ct);

        ViewBag.AssuntoId = assuntoId;
        return View(materiais);
    }
}
