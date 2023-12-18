using Microsoft.AspNetCore.Components;
using Oogarts.Shared.EyeConditions;

namespace Client.EyeConditions;

public partial class Index
{
	[Inject] public IEyeConditionService EyeConditionService { get; set; } = default!;
    [Inject] public ISymptomService SymptomService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private IEnumerable<EyeConditionDto.Index>? eyeConditions;
    private IEnumerable<SymptomDto.Index>? symptoms;

    private bool admin = false;
    private bool open = false;
    private bool openInfo = false;

    private long deleteRequestId = default;

    [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? Page { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? PageSize { get; set; }
    [Parameter, SupplyParameterFromQuery] public long? SymptomId { get; set; }



    protected override async Task OnParametersSetAsync()
	{
		EyeConditionRequest.Index request = new()
		{
            Searchterm = Searchterm,
            Page = Page.HasValue ? Page.Value : 1,
            PageSize = PageSize.HasValue ? PageSize.Value : 25,
            SymptomId = SymptomId
        };

		var response = await EyeConditionService.GetIndexAsync(request);
        eyeConditions = response.EyeConditions.OrderBy(ec => ec.Name).ToList();

        SymptomRequest.Index symptomRequest = new()
        {

        };
        var symptomResponse = await SymptomService.GetIndexAsync(symptomRequest);
        symptoms = symptomResponse.Symptoms.OrderBy(ec => ec.Name).ToList();
    }
    private void UserChanged()
    {
        admin = !admin;
    }
    private void NavigateToCreate()
    {
        if (admin)
        {
            NavigationManager.NavigateTo($"Oogaandoeningen/nieuw");
        }
    }
    private void ToggleDeletePopUp(long id)
    {
        open = !open;
        deleteRequestId = id;
    }
    private void CloseDeletePopUp()
    {
        open = false;
        deleteRequestId = default;
    }

    private async Task ConfirmDelete()
    {
        await EyeConditionService.DeleteAsync(deleteRequestId);
        NavigationManager.NavigateTo("Oogaandoeningen",true);
    }

    private void ToglleInfoPopUp()
    {
        openInfo = !openInfo;
    }

}