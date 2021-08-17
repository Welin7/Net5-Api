using System;
using Net5_Api.Controllers.Model;
using Xunit;
using FluentValidation.TestHelper;
using Net5_Api.Validator.DiretorValidator;
using Net5_Api.DTOs.Diretor;

namespace Net5_Api.Testes
{
    public class DiretorValidatorTestes
    {
        [Fact]
        public void NomeDoDiretorTemQueApresentarErroSeForNull()
        {
            var validator = new DiretorInputPostDTOValidator();
            var dto = new DiretorInputPostDTO {Nome = null};
            var result = validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(diretor => diretor.Nome);
        }

        [Fact]
        public void NomeDoDiretorTemQueApresentarErroSeForVazio()
        {
            var validator = new DiretorInputPostDTOValidator();
            var dto = new DiretorInputPostDTO {Nome = ""};
            var result = validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(diretor => diretor.Nome);
        }

        [Theory]
        [InlineData("Teste")]
        [InlineData("Teste Teste Teste Teste")]
        public void NomeDoDiretorValidoNaoDeveConterErro(string data)
        {
            var validator = new DiretorInputPostDTOValidator();
            var dto = new DiretorInputPostDTO { Nome = data };
            var result = validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(diretor => diretor.Nome);
        }
    }
}
