using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Application.Abstraction.Services.Document;

namespace PokemonGameAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController(IDocumentService _documentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _documentService.GetAllAsync();
        return Ok(response);
    }
}