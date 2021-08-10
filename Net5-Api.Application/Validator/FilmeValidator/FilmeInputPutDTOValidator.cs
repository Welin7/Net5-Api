using FluentValidation;
using Net5_Api.DTOs.Filme;

namespace Net5_Api.Validator.FilmeValidator
{
    public class FilmeInputPutDTOValidator : AbstractValidator<FilmeInputPutDTO>
    {
         public FilmeInputPutDTOValidator()
        {
            RuleFor(filme => filme.Id).NotNull().NotEmpty();
            RuleFor(filme => filme.DiretorId).NotNull().NotEmpty();
            RuleFor(filme =>filme.Titulo).NotNull().NotEmpty();
            RuleFor(filme =>filme.Titulo ).Length(2, 250).WithMessage("Tamanho {TotalLength} é inválido");
        }
    }
}