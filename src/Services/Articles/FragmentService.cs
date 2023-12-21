using System.Linq;
using Domain.Articles.Fragments;
using Persistence;
using Shared.Articles.Fragments;
using Microsoft.EntityFrameworkCore;

namespace Services.Articles.Fragments;
public class FragmentService : IFragmentService
{
    private readonly ApplicationDbContext dbContext;

    public FragmentService(ApplicationDbContext context)
    {
        dbContext = context;
    }

    public async Task<FragmentResult.Create> CreateAsync(FragmentDto.Mutate model)
    {
        if (await dbContext.Fragments.AnyAsync(x => x.Title == model.Title))
            throw new EntityAlreadyExistsException(nameof(Fragment), nameof(Fragment.Title), model.Title);

        Fragment fragment = new(model.Title, model.Description);

        dbContext.Fragments.Add(fragment);
        await dbContext.SaveChangesAsync();

        FragmentResult.Create result = new()
        {
            FragmentId = fragment.Id,
            Title = fragment.Title,
            Description = fragment.Description,
        };

        return result;
    }

    public async Task<FragmentResult.Index> GetIndexAsync(FragmentRequest.Index request)
    {
        var searchTerm = request.Searchterm != null ? request.Searchterm.ToLowerInvariant() : null;

        var query = dbContext.Fragments.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(x => x.Title.ToLower().Contains(searchTerm));
        }

        int totalAmount = await query.CountAsync();

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .OrderBy(x => x.Id)
            .Select(x => new FragmentDto.Index
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
            })
            .ToListAsync();

        var result = new FragmentResult.Index
        {
            Fragments = items,
            TotalAmount = totalAmount
        };

        return result;
    }


    public async Task<FragmentDto.Detail> GetDetailAsync(long Id)
    {
        FragmentDto.Detail? product = await dbContext.Fragments.Select(x => new FragmentDto.Detail
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,

        }).SingleOrDefaultAsync(x => x.Id == Id);

        if (product is null)
            throw new EntityNotFoundException(nameof(Fragment), Id);

        return product;
    }

public async Task EditAsync(long id, FragmentDto.Mutate model)
    {
        Fragment? fragment = await dbContext.Fragments.SingleOrDefaultAsync(x => x.Id == id);

        if (fragment is null)
            throw new EntityNotFoundException(nameof(Fragment), id);

        fragment.Title = model.Title;
        fragment.Description = model.Description;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        Fragment? fragment = await dbContext.Fragments.SingleOrDefaultAsync(x => x.Id == id);

        if (fragment is null)
            throw new EntityNotFoundException(nameof(Fragment),id);

        dbContext.Fragments.Remove(fragment);

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
