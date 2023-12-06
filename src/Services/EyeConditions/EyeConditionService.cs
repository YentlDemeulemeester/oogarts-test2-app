using System.Linq;
using Oogarts.Domain.EyeConditions;
using Oogarts.Persistence;
using Oogarts.Shared.EyeConditions;
using Microsoft.EntityFrameworkCore;
using Domain.Files;
using Services.Files;

namespace Oogarts.Services.EyeConditions;
public class EyeConditionService : IEyeConditionService
{
    private readonly ApplicationDbContext dbContext;
    private readonly IStorageService storageService;


    public EyeConditionService(ApplicationDbContext context, IStorageService storageService)
    {
        dbContext = context;
        this.storageService = storageService;
    }

    public async Task<EyeConditionResult.Create> CreateAsync(EyeConditionDto.Mutate model)
    {
        if (await dbContext.EyeConditions.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(EyeCondition), nameof(EyeCondition.Name), model.Name);

/*        Image image = new Image(storageService.BasePath, model.ImageContentType!);*/

        EyeCondition eyeCondition = new(model.Name, model.Description, model.Body, /*image.FileUri.ToString()*/ "Test");

        var symptoms = await dbContext.Symptoms.Where(x => model.Symptoms.Select(s => s.Id).ToList().Contains(x.Id)).ToListAsync();

        foreach (var symptom in symptoms)
        {
            eyeCondition.Symptom(symptom);
        }

        dbContext.EyeConditions.Add(eyeCondition);

        var selectedSymptoms = await dbContext.Symptoms
                                    //.Where(symptom => model.SymptomsChosen.Contains(symptom.Id))
                                    .ToListAsync();


            //eyeCondition.EyeConditionSymptoms.Add(es);
            //dbContext.Symptoms.Add(es);

        await dbContext.SaveChangesAsync();

        // Bug
        //Uri uploadSas = storageService.GenerateImageUploadSas(image);

        EyeConditionResult.Create result = new()
        {
            EyeConditionId = eyeCondition.Id,
            Name = eyeCondition.Name,
            Description = eyeCondition.Description,
            Body = eyeCondition.Body,
            UploadUri = eyeCondition.ImageUrl
        };

        return result;
    }

    public async Task<EyeConditionResult.Index> GetIndexAsync(EyeConditionRequest.Index request)
    {
        var searchTerm = request.Searchterm != null ? request.Searchterm.ToLowerInvariant() : null;

        var query = dbContext.EyeConditions.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(x => x.Name.ToLower().Contains(searchTerm));
        }
        if (request.SymptomId is not null)
        {
            query = query.Where(x => x.Symptoms.Any(es => es.Id == request.SymptomId.Value));
        }

        int totalAmount = await query.CountAsync();

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .OrderBy(x => x.Id)
            .Select(x => new EyeConditionDto.Index
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
            })
            .ToListAsync();

        var result = new EyeConditionResult.Index
        {
            EyeConditions = items,
            TotalAmount = totalAmount
        };

        return result;
    }


    public async Task<EyeConditionDto.Detail> GetDetailAsync(long Id)
    {
        EyeConditionDto.Detail? product = await dbContext.EyeConditions.Select(x => new EyeConditionDto.Detail
        {
            Id = x.Id,
            Name = x.Name,
            Body = x.Body,
            Description = x.Description,
            ImageUrl = x.ImageUrl,

        }).SingleOrDefaultAsync(x => x.Id == Id);

        if (product is null)
            throw new EntityNotFoundException(nameof(EyeCondition), Id);

        return product;
    }

public async Task EditAsync(long id, EyeConditionDto.Mutate model)
    {
        EyeCondition? eyeCondition = await dbContext.EyeConditions.SingleOrDefaultAsync(x => x.Id == id);

        if (eyeCondition is null)
            throw new EntityNotFoundException(nameof(EyeCondition), id);

        eyeCondition.Name = model.Name;
        eyeCondition.Description = model.Description;
        //eyeCondition.ImageUrl = model.ImageUrl;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        EyeCondition? eyecondition = await dbContext.EyeConditions.SingleOrDefaultAsync(x => x.Id == id);

        if (eyecondition is null)
            throw new EntityNotFoundException(nameof(EyeCondition),id);

        dbContext.EyeConditions.Remove(eyecondition);

        await dbContext.SaveChangesAsync();
    }

    // public void UpdateEyeCondition(int id, EyeCondition updatedEyeCondition)
    // {

    //     EyeCondition? existingEyeCondition = _context.EyeConditions.FirstOrDefault(e => e.Id == id);

    //     if (existingEyeCondition != null)
    //     {

    //         existingEyeCondition.Name = updatedEyeCondition.Name;
    //         existingEyeCondition.Description = updatedEyeCondition.Description;


    //         existingEyeCondition.Updated();
    //         _context.SaveChanges();
    //     }
    // }
}
