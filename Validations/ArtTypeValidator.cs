using FluentValidation;
using KnowledgeApi.Models;

namespace KnowledgeApi.Validations
{
    public class ArtTypeValidator: AbstractValidator<ArtType>
    {
        public ArtTypeValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("Başlık boş geçilemez.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Açıklama boş geçilemez");
        }
    }
}
