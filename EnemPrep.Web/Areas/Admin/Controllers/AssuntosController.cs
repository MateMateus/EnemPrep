using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Areas.Admin.ViewModels.Assuntos;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class AssuntosController(IAssuntoApiClient assuntoClient, IMateriaApiClient materiaClient) : Controller
{
    public async Task<IActionResult> Index(Guid materiaId, CancellationToken ct)
    {
        var materia = await materiaClient.GetByIdAsync(materiaId, ct);
        if (materia is null) return NotFound();

        ViewData["Title"] = $"Assuntos — {materia.Nome}";
        ViewBag.MateriaId = materiaId;
        ViewBag.MateriaNome = materia.Nome;

        var assuntos = await assuntoClient.GetByMateriaAsync(materiaId, ct);
        return View(assuntos);
    }

    [HttpGet]
    public async Task<IActionResult> Criar(Guid materiaId, CancellationToken ct)
    {
        var materia = await materiaClient.GetByIdAsync(materiaId, ct);
        if (materia is null) return NotFound();

        ViewData["Title"] = "Novo Assunto";
        return View(new CriarAssuntoViewModel { MateriaId = materiaId, MateriaNome = materia.Nome });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(CriarAssuntoViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Novo Assunto";
            return View(vm);
        }

        var resultado = await assuntoClient.CriarAsync(vm.Nome, vm.Descricao, vm.MateriaId, ct);
        if (resultado is null)
        {
            ModelState.AddModelError(string.Empty, "Erro ao criar assunto.");
            ViewData["Title"] = "Novo Assunto";
            return View(vm);
        }

        TempData["Sucesso"] = $"Assunto \"{resultado.Nome}\" criado!";
        return RedirectToAction(nameof(Index), new { materiaId = vm.MateriaId });
    }

    [HttpGet]
    public async Task<IActionResult> Editar(Guid id, Guid materiaId, CancellationToken ct)
    {
        var assuntos = await assuntoClient.GetByMateriaAsync(materiaId, ct);
        var assunto = assuntos.FirstOrDefault(a => a.Id == id);
        if (assunto is null) return NotFound();

        var materia = await materiaClient.GetByIdAsync(materiaId, ct);
        ViewData["Title"] = $"Editar: {assunto.Nome}";
        return View(new CriarAssuntoViewModel
        {
            MateriaId = materiaId,
            Nome = assunto.Nome,
            Descricao = assunto.Descricao ?? string.Empty,
            MateriaNome = materia?.Nome ?? string.Empty
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Guid id, CriarAssuntoViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Editar Assunto";
            return View(vm);
        }

        var ok = await assuntoClient.AtualizarAsync(id, vm.Nome, vm.Descricao, vm.MateriaId, ct);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "Erro ao atualizar assunto.");
            ViewData["Title"] = "Editar Assunto";
            return View(vm);
        }

        TempData["Sucesso"] = "Assunto atualizado!";
        return RedirectToAction(nameof(Index), new { materiaId = vm.MateriaId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deletar(Guid id, Guid materiaId, CancellationToken ct)
    {
        await assuntoClient.DeletarAsync(id, ct);
        TempData["Sucesso"] = "Assunto removido.";
        return RedirectToAction(nameof(Index), new { materiaId });
    }
}
