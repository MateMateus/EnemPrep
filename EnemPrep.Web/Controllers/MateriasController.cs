using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Controllers;

[VerificaSessaoAluno]
public class MateriasController(IMateriaApiClient materiaClient) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var materias = await materiaClient.GetAllAsync(ct);
        return View(materias);
    }

    public async Task<IActionResult> Detalhes(Guid id, CancellationToken ct)
    {
        var materia = await materiaClient.GetByIdAsync(id, ct);
        if (materia is null) return NotFound();

        return View(materia);
    }
}
