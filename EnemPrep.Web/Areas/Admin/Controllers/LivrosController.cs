using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Areas.Admin.ViewModels.Livros;
using EnemPrep.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnemPrep.Web.Areas.Admin.Controllers;

[Area("Admin")]
[VerificaSessaoAdmin]
public class LivrosController(ILivroApiClient livroClient, IMateriaApiClient materiaClient) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "Livros e Provas Antigas";
        var livros = await livroClient.GetAllAsync(ct: ct);
        return View(livros);
    }

    [HttpGet]
    public async Task<IActionResult> Criar(CancellationToken ct)
    {
        ViewData["Title"] = "Cadastrar Novo Livro";
        await CarregarViewBags(ct);
        return View(new CriarLivroViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(CriarLivroViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Cadastrar Novo Livro";
            await CarregarViewBags(ct);
            return View(vm);
        }

        var resultado = await livroClient.CriarAsync(vm.Titulo, vm.Descricao, vm.UrlCapa, vm.MateriaId, vm.TipoConteudo, vm.CapaArquivo, ct);
        if (resultado is null)
        {
            ModelState.AddModelError(string.Empty, "Erro ao criar livro. Tente novamente.");
            ViewData["Title"] = "Cadastrar Novo Livro";
            await CarregarViewBags(ct);
            return View(vm);
        }

        TempData["Sucesso"] = $"Livro \"{resultado.Titulo}\" cadastrado com sucesso!";
        return RedirectToAction(nameof(Gerenciar), new { id = resultado.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Editar(Guid id, CancellationToken ct)
    {
        var livro = await livroClient.GetByIdAsync(id, ct);
        if (livro is null) return NotFound();

        ViewData["Title"] = $"Editar Livro: {livro.Titulo}";
        await CarregarViewBags(ct);
        
        var vm = new CriarLivroViewModel
        {
            Titulo = livro.Titulo,
            Descricao = livro.Descricao,
            UrlCapa = livro.UrlCapa,
            MateriaId = livro.MateriaId,
            TipoConteudo = livro.TipoConteudo
        };
        
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Guid id, CriarLivroViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = $"Editar Livro: {vm.Titulo}";
            await CarregarViewBags(ct);
            return View(vm);
        }

        var ok = await livroClient.AtualizarAsync(id, vm.Titulo, vm.Descricao, vm.UrlCapa, vm.MateriaId, vm.TipoConteudo, vm.CapaArquivo, ct);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "Erro ao atualizar livro. Tente novamente.");
            ViewData["Title"] = $"Editar Livro: {vm.Titulo}";
            await CarregarViewBags(ct);
            return View(vm);
        }

        TempData["Sucesso"] = "Livro atualizado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Gerenciar(Guid id, CancellationToken ct)
    {
        var livro = await livroClient.GetByIdAsync(id, ct);
        if (livro is null) return NotFound();

        ViewData["Title"] = $"Gerenciar: {livro.Titulo}";
        return View(livro);
    }

    [HttpPost]
    public async Task<IActionResult> UploadPdf(Guid id, IFormFile? arquivoPdf, CancellationToken ct)
    {
        if (arquivoPdf == null || arquivoPdf.Length == 0)
        {
            TempData["Erro"] = "Nenhum arquivo PDF enviado.";
            return RedirectToAction(nameof(Gerenciar), new { id });
        }
        
        if (!arquivoPdf.ContentType.Contains("pdf", StringComparison.OrdinalIgnoreCase) && !arquivoPdf.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            TempData["Erro"] = "O arquivo enviado não é um PDF válido.";
            return RedirectToAction(nameof(Gerenciar), new { id });
        }

        using var stream = arquivoPdf.OpenReadStream();
        var ok = await livroClient.UploadPdfAsync(id, stream, arquivoPdf.FileName, ct);
        
        if (!ok) TempData["Erro"] = "Erro ao processar as páginas do PDF. Verifique se o PDF é válido e não está corrompido.";
        else TempData["Sucesso"] = $"As páginas do PDF foram carregadas com sucesso!";

        return RedirectToAction(nameof(Gerenciar), new { id });
    }

    [HttpPost]
    public async Task<IActionResult> CriarTema(Guid id, CriarTemaViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            TempData["Erro"] = "Dados do tema inválidos.";
            return RedirectToAction(nameof(Gerenciar), new { id });
        }

        var ok = await livroClient.CriarTemaAsync(id, vm.Nome, vm.PaginaInicial, vm.PaginaFinal, ct);
        if (!ok) TempData["Erro"] = "Erro ao criar tema.";
        else TempData["Sucesso"] = $"Tema \"{vm.Nome}\" criado!";

        return RedirectToAction(nameof(Gerenciar), new { id });
    }

    [HttpPost]
    public async Task<IActionResult> RemoverPagina(Guid id, Guid paginaId, CancellationToken ct)
    {
        var ok = await livroClient.RemoverPaginaAsync(id, paginaId, ct);
        if (!ok) TempData["Erro"] = "Erro ao remover página.";
        else TempData["Sucesso"] = "Página removida com sucesso!";

        return RedirectToAction(nameof(Gerenciar), new { id });
    }

    [HttpPost]
    public async Task<IActionResult> DeletarTema(Guid id, Guid temaId, CancellationToken ct)
    {
        await livroClient.DeletarTemaAsync(id, temaId, ct);
        TempData["Sucesso"] = "Tema removido.";
        return RedirectToAction(nameof(Gerenciar), new { id });
    }

    [HttpPost]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken ct)
    {
        await livroClient.DeletarAsync(id, ct);
        TempData["Sucesso"] = "Livro removido.";
        return RedirectToAction(nameof(Index));
    }

    private async Task CarregarViewBags(CancellationToken ct)
    {
        var materias = await materiaClient.GetAllAsync(ct);
        ViewBag.Materias = new SelectList(materias, "Id", "Nome");
    }
}
