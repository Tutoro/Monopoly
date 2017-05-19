using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Classi
{
    public enum Tipo_Probabilita
    { SpostaCasella, SpostaNumero, Tassa, TassaGlobale, UscitaPrigione };

    class Carta
    {
        public Tipo_Probabilita Tipo;
        public int Pagamento;
        public int Spostamento;
        public bool Via;

        public Carta(Tipo_Probabilita T, int P)
        {
            Tipo = T;
            Pagamento = P;
        }
        public Carta(Tipo_Probabilita T, int S, bool V)
        {
            Tipo = T;
            Spostamento = S;
            Via = V;
        }
        public Carta(Tipo_Probabilita T)
        {
            Tipo = T;
        }

    }
}
