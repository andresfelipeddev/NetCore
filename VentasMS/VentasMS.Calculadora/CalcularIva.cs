using System;

namespace VentasMS.Calculadora
{
    public class CalcularIva
    {

        public int CalculaIva(int valor, int porcIva)
        {
            if (valor == 0 && porcIva == 0)
            return -1;

            if(porcIva== 0 )
                return 0;

            int iva = valor * porcIva / 100;
            return iva;
        }
    }
}
