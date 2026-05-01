using EnemPrep.Web.Filters;
using EnemPrep.Web.ApiClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EnemPrep.Web.Enums;

namespace EnemPrep.Web.Controllers;

[VerificaSessaoAluno]
public class LivrosController(ILivroApiClient livroClient, IMateriaApiClient materiaClient) : Controller
{
    public async Task<IActionResult> Index([FromQuery] string? busca, [FromQuery] Guid? materiaId, [FromQuery] TipoConteudo? tipo, CancellationToken ct)
    {
        ViewData["Title"] = "Livros e Provas Antigas";
        var livros = await livroClient.GetAllAsync(busca, materiaId, tipo, ct);
        
        var materias = await materiaClient.GetAllAsync(ct);
        ViewBag.Materias = new SelectList(materias, "Id", "Nome", materiaId);
        ViewBag.Busca = busca;
        ViewBag.Tipo = tipo;
        
        return View(livros);
    }

    [HttpGet("livros/ler/{id:guid}")]
    public async Task<IActionResult> Ler(Guid id, Guid? temaId, CancellationToken ct)
    {
        var livro = await livroClient.GetByIdAsync(id, ct);
        if (livro is null) return NotFound();

        ViewData["SelectedTemaId"] = temaId;
        ViewData["Title"] = livro.Titulo;
        
        return View(livro);
    }
}
