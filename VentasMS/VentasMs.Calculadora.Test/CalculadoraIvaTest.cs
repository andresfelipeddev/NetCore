using System;
using VentasMS.Calculadora;
using Xunit;

namespace VentasMs.Calculadora.Test
{
    public class CalculadoraIvaTest
    {            
        
        private readonly CalcularIva calcular = new CalcularIva();
        int valor = 0;
        int porciva = 0;

        [Fact]
        public void CalcularIvaConParametrosEnCeroTest()
        {
            //Arrange

             valor = 0;
             porciva = 0;

            //Act
           int valorIva = calcular.CalculaIva(valor,porciva);

            //Assert
            Assert.Equal(-1, valorIva);
        }

        [Fact]
        public void CalcularIvaConPorcentajeCeroTest()
        {
            //Arrange
       
             valor = 20000;
             porciva = 0;

            //Act

            int valorIva = calcular.CalculaIva(valor, porciva);

            //Assert

            Assert.Equal(0, valorIva);
        }

        [Fact]
        public void CalcularIva19porcientoSobre2000Test()
        {
            //Arrange

            valor = 20000;
            porciva = 19;

            //Act

            int valorIva = calcular.CalculaIva(valor, porciva);

            //Assert

            Assert.Equal(3800, valorIva);
        }
    }
}
