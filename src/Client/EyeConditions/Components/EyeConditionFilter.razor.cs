using System;
using Microsoft.AspNetCore.Components;
using Oogarts.Shared.EyeConditions;

namespace Client.EyeConditions.Components;
public partial class EyeConditionFilter
{
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Parameter] public List<SymptomDto.Index> symptoms { get; set; } = new List<SymptomDto.Index>();
    [Parameter, EditorRequired] public string? Searchterm { get; set; } = default!;
    [Parameter, EditorRequired] public long? SymptomId { get; set; }

    private string? searchTerm;
    private long? symptomId;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        searchTerm = Searchterm;
        symptomId = SymptomId;
    }
    private void SearchTermChanged(ChangeEventArgs args)
    {
        searchTerm = args.Value?.ToString();
        FilterEyeConditions();
    }

    private void TagChanged(ChangeEventArgs args)
    {
        if (args.Value is string stringValue)
        {
            Console.WriteLine(args.Value.ToString());
            
            if (long.TryParse(stringValue, out long longValue))
            {
                symptomId = longValue;
            }
            else
            {
                symptomId = null;
            }
        }
        else
        {
            symptomId = null;
        }

        FilterEyeConditions();
    }


    private void FilterEyeConditions()
    {
        Dictionary<string, object?> parameters = new();

        parameters.Add(nameof(searchTerm), searchTerm);
        parameters.Add(nameof(symptomId), symptomId);

        var uri = NavigationManager.GetUriWithQueryParameters(parameters);

        NavigationManager.NavigateTo(uri);
    }
    
}

