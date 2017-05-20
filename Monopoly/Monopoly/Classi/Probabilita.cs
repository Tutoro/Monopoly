using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Classi
{
    public enum Tipo_Carta
    { SpostaCasella, SpostaNumero, Tassa, TassaGlobale, UscitaPrigione, TassaCaseAlberghi };

    class Carta
    {
        public Tipo_Carta Tipo;
        public int Pagamento;
        public int Spostamento;
        //public bool Via;
        public int PagaC; // Costo case
        public int PagaA; // Costo alberghi
        public string Messaggio;

        public Carta(Tipo_Carta T, int P, string M)
        {
            Tipo = T;
            Pagamento = P;
            Messaggio = M;
        }
        public Carta(Tipo_Carta T, string M)
        {
            Tipo = T;
            Messaggio = M;
        }
        public Carta(Tipo_Carta T, int C, int A, string M)
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

            T[T.Length - 1] = new Carta(Tipo_Carta.UscitaPrigione, "Uscite gratis di prigione, se ci siete: potete conservare questo cartoncino sino al momento di servirvene(non si sa mai!) oppure venderlo");
            L = T;
        }

        public static Proprieta GetProprieta(Casella C)
        {
            return (Proprieta)C;
        }

        public static void Mischia(ref Carta[] M)
        {
            Random R = new Random();
            for(int i = 0; i < M.Length / 2; i++)
            {
                int Indice = R.Next(M.Length / 2, M.Length);
                Carta Temp = M[i];
                M[i] = M[Indice];
                M[Indice] = Temp;
            }
        }

    }
}