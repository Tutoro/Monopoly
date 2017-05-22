using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*!
\author    Verazza Claudio
\version   0.3b
\date      15/05/2017
*/

namespace Monopoly.Classi
{
    public enum Tipo_Carta
    { SpostaCasella, SpostaNumero, Tassa, TassaGlobale, UscitaPrigione, TassaCaseAlberghi };

    //! \class Carta
    //! \brief Classe che rappresenta le carte delle probabilità e degli imprevisti nel gioco
    class Carta
    {
        public Tipo_Carta Tipo; //! \var Tipo \brief Tipo di azione della carta
        private int Pagamento; //Pagamento che deve effettuare il giocatore (nel caso la carta sia una tassa) oppure la somme che ogni giocatore deve versare (tassaGlobale)
        private int Spostamento; //Lo spostamento che deve essere effettuato dal giocatore (tipo SpostaCasella o SpostaNumero)
        private int PagaC; // Costo case
        private int PagaA; // Costo alberghi
        public string Messaggio; //! \var Messaggio \brief Il messaggio scritto sulla carta da mostrare all'utente

        public Carta(Tipo_Carta T, int P, string M)
        {
            Tipo = T;
            Pagamento = P;
            Spostamento = P;
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

        //esegue l'azione
        public bool Azione(int Turno, Giocatore[] Giocatori, ref Carta[] Mazzo)
        {
            bool Ritorno = false;
            switch (Tipo)
            {
                case Tipo_Carta.SpostaCasella:
                    Giocatori[Turno].SetPosizione(Spostamento, Turno, true);
                    Ritorno = true;
                    Scorri(ref Mazzo);
                    break;

                case Tipo_Carta.SpostaNumero:
                    Giocatori[Turno].SetPosizione(-Mazzo[0].Spostamento, Turno, false);
                    Ritorno = true;
                    Scorri(ref Mazzo);
                    break;

                case Tipo_Carta.Tassa:
                    Giocatori[Turno].Soldi += Mazzo[0].Pagamento;
                    Scorri(ref Mazzo);
                    break;

                case Tipo_Carta.TassaGlobale:
                    foreach (Giocatore G in Giocatori)
                        if (G != Giocatori[Turno])
                            G.Soldi -= Mazzo[0].Pagamento;
                        else
                            G.Soldi += Mazzo[0].Pagamento * (Giocatori.Length - 1);
                    Scorri(ref Mazzo);
                    break;

                case Tipo_Carta.UscitaPrigione:
                    Giocatori[Turno].BigliettoPrigione++;
                    RimuoviPrigione(ref Mazzo);
                    Scorri(ref Mazzo);
                    break;
                case Tipo_Carta.TassaCaseAlberghi:
                    int Costo = 0;
                    foreach (Proprieta P in Giocatori[Turno].Proprieta)
                    {
                        foreach (Struttura S in P.Strutture)
                            if (S.Tipo)
                                Costo += Mazzo[0].PagaC;
                            else
                                Costo += Mazzo[0].PagaA;
                    }
                    Giocatori[Turno].Soldi -= Costo;
                    Scorri(ref Mazzo);
                    break;
            }
            return Ritorno;
        }

        //scorre il mazzo di carte
        public static void Scorri(ref Carta[] L)
        {
            Carta T = L[0];
            for (int i = 1; i < L.Length; i++)
                L[i - 1] = L[i];

            L[L.Length - 1] = T;
        }

        //rimuove la carta della prigione dal mazzo
        public static void RimuoviPrigione(ref Carta[] L)
        {
            Carta[] T = new Carta[L.Length - 1];
            for (int i = 1; i < L.Length; i++)
                T[i - 1] = L[i];

            L = T;
        }

        //aggiunge la carta della prigione nel mazzo
        public static void AggiungiPrigione(ref Carta[] L)
        {
            Carta[] T = new Carta[L.Length + 1];
            for (int i = 0; i < L.Length; i++)
                T[i] = L[i];

            T[T.Length - 1] = new Carta(Tipo_Carta.UscitaPrigione, "Uscite gratis di prigione, se ci siete: potete conservare questo cartoncino sino al momento di servirvene(non si sa mai!) oppure venderlo");
            L = T;
        }

        //mischia il mazzo di carte
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
