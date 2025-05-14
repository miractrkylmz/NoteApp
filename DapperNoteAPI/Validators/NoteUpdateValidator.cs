using DapperNoteAPI.DTOs;
using FluentValidation;

namespace DapperNoteAPI.Validators
{
    public class NoteUpdateValidator:AbstractValidator<NoteUpdateDto>
    {
        public NoteUpdateValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id 0'dan büyük olmalıdır");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık zorunludur.")
                .MaximumLength(100).WithMessage("Başlık 100 karakterden az olmalıdır.");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");
        }
    }
}
