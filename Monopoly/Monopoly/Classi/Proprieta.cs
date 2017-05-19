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

/*!
\author    Arduini Alberto
\author    Zacconi Andrea
\version   0.2
\date      19/05/2017
*/

namespace Monopoly.Classi
{
    public enum Tipo_Speciali
    { Imprevisti, Parcheggio, Prigione, Probabilita, Tassa };
    
    //! \class Proprieta
    //! \brief Classe che rappresenta una proprietà nel programma
    public class Proprieta : Casella
    {
        public int Costo; //! \var Costo \brief Costo della proprietà
        public bool Ipotecato; //! \var Ipotecato \brief Se la proprietà è ipotecata
        public bool Speciale; //! \var Speciale \brief Se è una proprietà speciale (non è possibile costruirci sopra)
        public List<Struttura> Strutture; //! \var Strutture \brief Lista che contiene le case e gli alberghi sulla proprietà
        public Giocatore Proprietario; //! \var Proprietario \brief Il Giocatore che possiede la proprietà, o 'null' se non è acquistata

        //! \fn Proprieta
        //! \brief Crea una proprietà con parametri definiti
        //! \param Col \brief Colore della proprietà
        //! \param N \brief Nome della proprietà
        //! \param C \brief Costo della proprietà
        //! \param S \brief Se la proprietà è speciale
        public Proprieta(Brush Col, string N, int C, bool S) : base(Col, N)
        {
            Costo = C;
            Speciale = S;
            Proprietario = null;
            Ipotecato = false;
            Strutture = new List<Struttura>();
        }

        //! \fn Rendita
        //! \brief Gestisce la rendita
        //! \param Pagante \brief Il giocatore che deve pagare (il prorpietario è già salvato nelle proprietà)
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

        //! \fn GetInterfaccia
        //! \brief Crea un TextBlock che mostra la proprietà nel pannello a lato
        //! \param Altezza \brief L'altezza a cui mostrare il TextBlock (verrà incrementata in automatico)
        public TextBlock GetInterfaccia(ref int Altezza)
        {
            TextBlock R = new TextBlock();
            R.TextAlignment = TextAlignment.Left;
            R.HorizontalAlignment = HorizontalAlignment.Left;
            R.VerticalAlignment = VerticalAlignment.Top;
            R.Height = 22;
            R.Margin = new Thickness(0, Altezza, 0, 0);
            Altezza += 2;
            if (!Speciale)
                R.Background = Colore;
            else
                R.Background = Brushes.DarkGray;

            R.Text = Nome;
            return R;
        }
    }

    //! \class Speciali
    //! \brief Classe che rappresenta le caselle speciali (non acquistabili) nel programma 
    public class Speciali : Casella
    {
        public int Tassa_Costo; //! \var Tassa_Costo \brief Nel caso la casella sia una tassa il valore da pagare
        public Tipo_Speciali Tipo; //! \var Tipo \brief Il tipo di casella Speciale

        //! \var Speciali
        //! \brief Crea una casella speciale con parametri definiti
        //! \param N \brief Nome della casella
        //! \param T \brief Tipo della casella
        //! \param C \brief Costo della tassa
        public Speciali(string N, Tipo_Speciali T, int C) : base(Brushes.Black, N)
        {
            Tassa_Costo = C;
            Tipo = T;
        }
    }
}