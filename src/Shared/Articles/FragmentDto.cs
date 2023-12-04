
using FluentValidation;

namespace Oogarts.Shared.Articles.Fragments;
public abstract class FragmentDto
{
    public class Index
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class Detail
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class Mutate
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        /*public DateTime IsCreated { get; set; }
        public DateTime IsUpdated { get; set; }
        public bool IsEnabled { get; set; }*/

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
            }
        }
    }
}
