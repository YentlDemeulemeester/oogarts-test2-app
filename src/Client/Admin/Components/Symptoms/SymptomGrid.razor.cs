using Microsoft.AspNetCore.Components;
using Shared.EyeConditions;
using Radzen.Blazor;

namespace Client.Admin.Components.Symptoms
{
    public partial class SymptomGrid
    {
        RadzenDataGrid<SymptomDto.Index> symptomGrid;
        SymptomDto.Index symptomToInsert;
        SymptomDto.Mutate symptomToUpdate = new();
        List<SymptomDto.Index> symptoms;
        [Inject] public ISymptomService SymptomService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            SymptomResult.Index res = await SymptomService.GetIndexAsync(new SymptomRequest.Index());
            symptoms = res.Symptoms as List<SymptomDto.Index>;
        }
        public async Task DeleteSymptom(SymptomDto.Index data)
        {
            Reset();
            long symptomId = data.Id;
            await SymptomService.DeleteAsync(symptomId);
            symptoms.Remove(data);
            await symptomGrid.Reload();
        } 

        void Reset()
        {
            symptomToInsert = null;
            symptomToUpdate = null;
        }

        async Task EditSymptom(SymptomDto.Index symptom)
        {
            symptomToUpdate.Name = symptom.Name;
            await symptomGrid.EditRow(symptom);
        }
        async Task OnUpdateRow(SymptomDto.Index symptom)
        {
            Reset();

            await SymptomService.EditAsync(symptom.Id, symptomToUpdate);
        }
        async Task SaveSymptom(SymptomDto.Index symptom)
        {
            await symptomGrid.UpdateRow(symptom);
        }

    }
}
