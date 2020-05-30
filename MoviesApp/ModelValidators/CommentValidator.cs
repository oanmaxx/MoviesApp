using FluentValidation;
using MoviesApp.Models;


namespace MoviesApp.ModelValidators
{
    public class CommentValidator : AbstractValidator<Comment>
    {
       public CommentValidator()
        {
            RuleFor(x => x.Text).NotNull();
            RuleFor(x => x.Movie).NotNull();
        }
    }
}
