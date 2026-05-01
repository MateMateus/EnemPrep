using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Areas.Admin.ViewModels.Materias;
using EnemPrep.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Areas.Admin.Controllers;

[Area("Admin")]
[VerificaSessaoAdmin]
public class MateriasController(IMateriaApiClient materiaClient) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "Matérias";
        var materias = await materiaClient.GetAllAsync(ct);
        return View(materias);
    }

    [HttpGet]
    public IActionResult Criar()
    {
        ViewData["Title"] = "Nova Matéria";
        return View(new CriarMateriaViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(CriarMateriaViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Nova Matéria";
            return View(vm);
        }

        var resultado = await materiaClient.CriarAsync(vm.Nome, vm.Descricao, ct);
        if (resultado is null)
        {
            ModelState.AddModelError(string.Empty, "Erro ao criar matéria. Verifique os dados e tente novamente.");
            ViewData["Title"] = "Nova Matéria";
            return View(vm);
        }

        TempData["Sucesso"] = $"Matéria \"{resultado.Nome}\" criada com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Editar(Guid id, CancellationToken ct)
    {
        var materia = await materiaClient.GetByIdAsync(id, ct);
        if (materia is null) return NotFound();

        ViewData["Title"] = $"Editar: {materia.Nome}";
        return View(new CriarMateriaViewModel { Nome = materia.Nome, Descricao = materia.Descricao });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Guid id, CriarMateriaViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Editar Matéria";
            return View(vm);
        }

        var ok = await materiaClient.AtualizarAsync(id, vm.Nome, vm.Descricao, ct);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "Erro ao atualizar matéria.");
            ViewData["Title"] = "Editar Matéria";
            return View(vm);
        }

        TempData["Sucesso"] = "Matéria atualizada com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken ct)
    {
        var sucesso = await materiaClient.DeletarAsync(id, ct);
        if (sucesso)
        {
            TempData["Sucesso"] = "Matéria removida.";
        }
        else
        {
            TempData["Erro"] = "Não foi possível remover a matéria. Verifique se existem livros vinculados a ela.";
        }
        return RedirectToAction(nameof(Index));
    }
}
