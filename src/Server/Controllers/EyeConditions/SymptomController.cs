using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oogarts.Shared.EyeConditions;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.EyeConditions;

[ApiController]
[Route("api/[controller]")]
public class SymptomController : ControllerBase
{
    private readonly ISymptomService symptomService;

    public SymptomController(ISymptomService symptomService)
    {
        this.symptomService = symptomService;
    }

    [SwaggerOperation("Returns a list of all Symptoms available in the Oogarts catalog.")]
    [HttpGet, AllowAnonymous]
    public async Task<SymptomResult.Index> GetIndex([FromQuery] SymptomRequest.Index request)
    {
        return await symptomService.GetIndexAsync(request);
    }

    [SwaggerOperation("Creates new Symptom.")]
    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Create(SymptomDto.Mutate model)
    {
        var creationId = await symptomService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), creationId);
    }
}

