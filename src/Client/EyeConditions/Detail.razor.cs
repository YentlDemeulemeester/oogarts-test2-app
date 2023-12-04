using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Oogarts.Shared.EyeConditions;

namespace Client.EyeConditions;
public partial class Detail
{
    private EyeConditionDto.Detail? eyeCondition;

    [Parameter] public long Id { get; set; }
    [Inject] public IEyeConditionService EyeConditionService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;


    protected override async Task OnParametersSetAsync()
    {
        await GetEyeConditionAsync();
    }

    private async Task GetEyeConditionAsync()
    {
        eyeCondition = await EyeConditionService.GetDetailAsync(Id);
    }

}

