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
        public Canvas GetInterfaccia(ref int Altezza)
        {
            Canvas C = new Canvas();

            C.HorizontalAlignment = HorizontalAlignment.Center;
            C.VerticalAlignment = VerticalAlignment.Top;
            C.Width = 272;
            C.Height = 22;
            C.Margin = new Thickness(0, Altezza, 0, 0);
            Altezza += 2;

            TextBlock R = new TextBlock();
            R.HorizontalAlignment = HorizontalAlignment.Center;
            R.VerticalAlignment = VerticalAlignment.Top;
            R.TextAlignment = TextAlignment.Center;
            R.Width = 174;
            R.Height = 22;
            R.Text = Nome;
            R.Margin = new Thickness(98, 0, 0, 0);
            if (!Speciale)
                R.Background = Colore;
            else
            {
                R.Background = Brushes.Black;
                R.Foreground = Brushes.White;
            }
            C.Children.Add(R);

            if(Strutture.Count > 0)
            {
                for(int i = 0; i < Strutture.Count; i++)
                {
                    R = new TextBlock();
                    R.HorizontalAlignment = HorizontalAlignment.Left;
                    R.VerticalAlignment = VerticalAlignment.Top;
                    R.TextAlignment = TextAlignment.Center;
                    R.Width = 18;
                    R.Height = 18;
                    R.Margin = new Thickness(22 * i + 2 * i, 2, 0, 0);
                    R.Text = "";
                    if (Strutture[i].Tipo)
                        R.Background = Brushes.ForestGreen;
                    else
                        R.Background = Brushes.IndianRed;

                    C.Children.Add(R);
                }
            }

            return C;
        }

        //! \fn AddStruttura
        //! \brief Aggiunge una struttura sulla proprieta
        public void AddStruttura()
        {
            if (Strutture.Count == 4)
            {
                Strutture.Clear();
                Strutture.Add(new Struttura(false));
            }
            else
                Strutture.Add(new Struttura(true));
        }

        //! \fn RemoveStruttura
        //! \brief Rimuove una struttura sulla proprieta
        public void RemoveStruttura()
        {
            if (!Strutture[0].Tipo)
                for (int i = 0; i < 4; i++)
                    Strutture.Add(new Struttura(true));
            Strutture.RemoveAt(0);
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