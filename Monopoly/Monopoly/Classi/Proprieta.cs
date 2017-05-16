using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monopoly.Classi
{

    public enum Tipo_Speciali
    { Imprevisti, Parcheggio, Prigione, Probabilita, Tassa };

    public class Proprieta : Casella
    {
        public int Costo;
        public bool Ipotecato;
        public bool Speciale;
        public List<Struttura> Strutture;
        public Giocatore Proprietario;

        public Proprieta(Brush Col, string N, int C, bool S) : base(Col, N)
        {
            Costo = C;
            Speciale = S;
            Proprietario = null;
            Ipotecato = false;
            Strutture = new List<Struttura>();
        }

        public void Rendita(Giocatore Pagante)
        {
            if (Proprietario != null && Proprietario != Pagante)
            {
                int Quantita = 0;
                if (!Speciale)
                    Quantita = Costo / 4;

                else if (Colore == Brushes.Black)
                    foreach (Proprieta P in Proprietario.Proprieta)
                        if (P.Speciale && P.Colore == Brushes.Black)
                            Quantita += Costo / 4;

                Proprietario.Soldi += Quantita;
                Pagante.Soldi -= Quantita;
            }
        }
    }

    public class Speciali : Casella
    {
        public int Tassa_Costo;
        public Tipo_Speciali Tipo;
        public Speciali(string N, Tipo_Speciali T, int C) : base(Brushes.Black, N)
        {
            Tassa_Costo = C;
            Tipo = T;
        }
    }
}