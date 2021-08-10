using System;
using Xunit;

namespace Net5_Api.Testes
{
    public class UnitTest1
    {
        [Fact]

        public void QuandoPassarDoisEDoisOResultadoTemQueSerQuatro()
        {
            Assert.Equal(4, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}
