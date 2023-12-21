using System;
namespace Shared.Articles.Fragments;

public abstract class FragmentResult
{
	public class Index
	{
		public IEnumerable<FragmentDto.Index>? Fragments { get; set; }
		public int TotalAmount { get; set; }
	}

	public class Create
	{
		public long FragmentId { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsEnabled { get; set; }
	}
	
}
