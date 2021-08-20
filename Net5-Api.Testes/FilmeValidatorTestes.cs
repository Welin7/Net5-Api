using System;
using Net5_Api.Controllers.Model;
using Xunit;
using FluentValidation.TestHelper;
using Net5_Api.Validator.FilmeValidator;
using Net5_Api.DTOs.Filme;

namespace Net5_Api.Testes
{
    public class FilmeValidatorTestes
    {
        [Fact]
        public void NomeDoFilmeTemQueApresentarErroSeForNull()
        {
            var validator = new FilmeInputPostDTOValidator();
            var dto = new FilmeInputPostDTO(null,2,"Ano de 1998");
            var result = validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(filme => filme.Titulo);
        }

        [Fact]
        public void IdDoDiretorTemQueApresentarErroSeForZero()
        {
            var validator = new FilmeInputPostDTOValidator();
            var dto = new FilmeInputPostDTO("Bruxa de Blair",0,"Ano de 1998");
            var result = validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(filme => filme.DiretorId);
        }

        [Theory]
        [InlineData("O Chamado")]
        [InlineData("O Misterioso Caso de Benjamin Button")]
        public void NomeDoFilmeValidoNaoDeveConterErro(string data)
        {
            var validator = new FilmeInputPostDTOValidator();
            var dto = new FilmeInputPostDTO (data,7,"Ano 2022");
            var result = validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(filme => filme.Titulo);
        }
    }
}