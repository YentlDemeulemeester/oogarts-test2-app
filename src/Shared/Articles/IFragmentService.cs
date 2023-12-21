namespace Shared.Articles.Fragments;

public interface IFragmentService
{
    Task<FragmentResult.Index> GetIndexAsync(FragmentRequest.Index request);
    Task<FragmentResult.Create> CreateAsync(FragmentDto.Mutate model);
    Task<FragmentDto.Detail> GetDetailAsync(long id);
    // Task<int> CreateAsync(ProductDto.Mutate model);
    Task EditAsync(long id, FragmentDto.Mutate edit);
    Task DeleteAsync(long id);
    // Task AddTagAsync(int productId, int tagId);
}