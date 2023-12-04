using Microsoft.EntityFrameworkCore;
using Oogarts.Domain.EyeConditions;
using Oogarts.Persistence;
using Oogarts.Shared.EyeConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EyeConditions;

public class SymptomService : ISymptomService
{
    private readonly ApplicationDbContext dbContext;

    public SymptomService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<SymptomResult.Create> CreateAsync(SymptomDto.Mutate model)
    {
        if (await dbContext.Symptoms.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(EyeCondition), nameof(EyeCondition.Name), model.Name);

        Symptom symptom = new(model.Name);
        
        dbContext.Symptoms.Add(symptom);
        await dbContext.SaveChangesAsync();

        return new SymptomResult.Create
        {
            SymptomId = symptom.Id,
            Name = symptom.Name,
        };


    }

    public async Task<SymptomResult.Index> GetIndexAsync(SymptomRequest.Index request)
    {
        var query = dbContext.Symptoms.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => x.Name.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase));
        }

        int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new SymptomDto.Index
           {
               Id = x.Id,
               Name = x.Name,
           }).ToListAsync();
        
        var result = new SymptomResult.Index
        {
            Symptoms = items,
            TotalAmount = totalAmount
        };
        return result;
    }
}
