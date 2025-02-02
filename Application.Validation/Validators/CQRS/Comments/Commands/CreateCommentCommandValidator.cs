using Application.CQRS.Comments.Commands;
using Application.Validation.Options;
using FluentValidation;

namespace Application.Validation.Validators.CQRS.Comments.Commands
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(c => c.Content)
                .NotEmpty()
                .Length(CommentValidationOptions.ContentMinLength, CommentValidationOptions.ContentMaxLength);
        }
    }
}