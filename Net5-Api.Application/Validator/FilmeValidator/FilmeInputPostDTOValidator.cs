using FluentValidation;
using Net5_Api.DTOs.Filme;

namespace Net5_Api.Validator.FilmeValidator
{
    public class FilmeInputPostDTOValidator : AbstractValidator<FilmeInputPostDTO>
    {
        public FilmeInputPostDTOValidator()
        {
            RuleFor(filme => filme.Titulo).NotNull().NotEmpty();
            RuleFor(filme => filme.Titulo).Length(2, 250).WithMessage("Tamanho {TotalLength} é inválido");
            RuleFor(filme => filme.DiretorId).NotNull().NotEmpty();
        }
    }
}