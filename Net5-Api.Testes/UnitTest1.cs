using System;
using Net5_Api.Controllers.Model;
using Xunit;

namespace Net5_Api.Testes
{
    public class UnitTest1
    {
        [Fact]
        public void CriaUmDiretor()
        {
            var diretor = new Diretor("Spike Lee");
            Assert.Equal("Spike Lee", diretor.Nome);
        }
    }
}
