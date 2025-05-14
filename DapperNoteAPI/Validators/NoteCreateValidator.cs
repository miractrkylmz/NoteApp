using DapperNoteAPI.DTOs;
using FluentValidation;

namespace DapperNoteAPI.Validators
{
    public class NoteCreateValidator:AbstractValidator<NoteCreateDto>
    {
        public NoteCreateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık zorunludur.")
                .MaximumLength(100).WithMessage("Başlık 100 karakterden fazla olamaz.");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik zorunludur.");
        }
    }
}
