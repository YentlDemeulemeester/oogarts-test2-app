namespace Shared.Articles.Fragments;

public abstract class FragmentRequest
{
	public class Index
	{
		public string? Searchterm { get; set; }
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 25;
        public int? TagId { get; set; }

    }
}