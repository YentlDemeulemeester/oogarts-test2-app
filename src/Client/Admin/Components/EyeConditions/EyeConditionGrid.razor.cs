using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.EyeConditions;
using Radzen.Blazor;

namespace Client.Admin.Components.EyeConditions
{
    public partial class EyeConditionGrid
    {
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; } = default!;
        RadzenDataGrid<EyeConditionDto.Index> eyeConditionGrid;
        List<EyeConditionDto.Index>? eyeConditions;
        [Inject] public IEyeConditionService EyeConditionService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            EyeConditionResult.Index res = await EyeConditionService.GetIndexAsync(new EyeConditionRequest.Index());
            eyeConditions = res.EyeConditions.ToList();
        }

        async Task EditRow(EyeConditionDto.Index eyeCondition)
        {
            long eyeConditionId = eyeCondition.Id;
            // await JSRuntime.InvokeVoidAsync("openInNewTab", $"/Oogaandoeningen/edit/{eyeConditionId}");
            NavigationManager.NavigateTo($"/Oogaandoeningen/edit/{eyeConditionId}", true);
        }

        async Task DeleteRow(EyeConditionDto.Index eyeCondition)
        {
            long eyeConditionId = eyeCondition.Id;
            await EyeConditionService.DeleteAsync(eyeConditionId);
            eyeConditions.Remove(eyeCondition);
            await eyeConditionGrid.Reload();
        }

        async Task CreateEyeCondition()
        {
            NavigationManager.NavigateTo($"/Oogaandoeningen/nieuw", true);
        }
    }
}
