using FluentValidation;
using Net5_Api.DTOs.Diretor;

namespace Net5_Api.Validator.DiretorValidator
{
    public class DiretorInputPutDTOValidator : AbstractValidator<DiretorInputPutDTO>
    {
        public DiretorInputPutDTOValidator()
        {
            RuleFor(diretor =>diretor.Nome).NotNull().NotEmpty();
            RuleFor(diretor =>diretor.Nome ).Length(2, 250).WithMessage("Tamanho {TotalLength} é inválido");
        }
    }
}