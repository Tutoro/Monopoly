using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Classi
{
    public enum Tipo_Probabilita
    { SpostaCasella, SpostaNumero, Tassa, TassaGlobale, UscitaPrigione, TassaCaseAlberghi};

    class Carta
    {
        public Tipo_Probabilita Tipo;
        public int Pagamento;
        public int Spostamento;
        //public bool Via;
        public int PagaC; // Costo case
        public int PagaA; // Costo alberghi
        public string Messaggio;

        public Carta(Tipo_Probabilita T, int P, string M)
        {
            Tipo = T;
            Pagamento = P;
            Messaggio = M;
        }
        public Carta(Tipo_Probabilita T, string M)
        {
            Tipo = T;
            Messaggio = M;
        }
        public Carta(Tipo_Probabilita T,int C, int A, string M)
        {
            Tipo = T;
            PagaC = C;
            PagaA = A;
            Messaggio = M;
        }

        public static void Scorri(ref Carta[] L)
        {
            Carta T = L[0];
            for (int i = 1; i < L.Length; i++)
                L[i - 1] = L[i];

            L[L.Length - 1] = T;
        }

        public static void RimuoviPrigione(ref Carta[] L)
        {
            Carta[] T = new Carta[L.Length - 1];
            for (int i = 1; i < L.Length; i++)
                    T[i - 1] = L[i];

            L = T;
        }

        public static void AggiungiPrigione(ref Carta[] L)
        {
            Carta[] T = new Carta[L.Length + 1];
            for (int i = 0; i < L.Length; i++)
                T[i] = L[i];

            T[T.Length - 1] = new Carta(Tipo_Probabilita.UscitaPrigione, "");
            L = T;
        }

    }
}
