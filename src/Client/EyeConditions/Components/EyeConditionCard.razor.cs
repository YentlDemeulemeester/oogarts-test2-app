using Microsoft.AspNetCore.Components;
using Oogarts.Shared.EyeConditions;

namespace Client.EyeConditions.Components;
public partial class EyeConditionCard
{
    [Parameter] public EventCallback<long> OnDeleteRequested { get; set; }
    
	[Parameter] public EyeConditionDto.Index EyeCondition { get; set; } = default!;
    [Parameter] public bool IsAdmin { get; set; }
    [Parameter] public long Id { get; set; }
    [Inject] public IEyeConditionService EyeConditionService { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;


}

