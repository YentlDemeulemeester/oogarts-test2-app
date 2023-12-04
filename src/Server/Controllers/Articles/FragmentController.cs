using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Oogarts.Shared.Articles.Fragments;
using Microsoft.AspNetCore.Authorization;


using Oogarts.Persistence;
using Oogarts.Shared.Authentication;

namespace Oogarts.Server.Controllers.Articles.Fragments;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FragmentController : ControllerBase
{
    private readonly IFragmentService fragmentService;

    public FragmentController(IFragmentService fragmentService)
    {
        this.fragmentService = fragmentService;
    }

	[SwaggerOperation("Returns a list of fragments.")]
	[HttpGet, AllowAnonymous]
	public async Task<FragmentResult.Index> GetIndex([FromQuery] FragmentRequest.Index request)
	{
		return await fragmentService.GetIndexAsync(request);
	}

	[SwaggerOperation("Creates a new fragment")]
	[HttpPost, /*Authorize(Roles = Roles.Administrator)*/ AllowAnonymous]
	public async Task<IActionResult> Create(FragmentDto.Mutate model)
    {
		var creationId = await fragmentService.CreateAsync(model);
		return CreatedAtAction(nameof(Create), creationId);
	}

    [SwaggerOperation("Returns a specific fragment available in the fragments catalog.")]
    [HttpGet("{Id}"), AllowAnonymous]
    public async Task<FragmentDto.Detail> GetDetail(long Id)
    {
        return await fragmentService.GetDetailAsync(Id);
    }

    [SwaggerOperation("Edits an existing fragment.")]
    [HttpPut("{id}"), AllowAnonymous]
    public async Task<IActionResult> Edit(long id, FragmentDto.Mutate model)
    {
        await fragmentService.EditAsync(id, model);
        return NoContent();
    }

	[SwaggerOperation("Deletes an existing EyeCondition.")]
    [HttpDelete("{id}"), AllowAnonymous]
    public async Task<IActionResult> Delete(long id)
    {
        await fragmentService.DeleteAsync(id);
        return NoContent();
    }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetEyeCondition(int id)
    // {
    //     var eyeCondition = await _context.EyeConditions.FindAsync(id);
    //     if (eyeCondition == null)
    //     {
    //         return NotFound();
    //     }
    //     return Ok(eyeCondition);
    // }
}
