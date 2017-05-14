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
\version   0.1b
\date      02/05/2017
*/

namespace Monopoly.Classi
{
    //! \class Giocatore
    //! \brief Classe che rappresenta il giocatore nel programma
    public class Giocatore
    {
        public Ellipse Pedina; //! \var Pedina \brief Pedina che rappresenta il giocatore sul campo
        public List<Casella> Proprieta; //! \var Proprieta \brief Lista di Caselle che contiene le Proprietà (normali e speciali) che il giocatore possiede
        public int Soldi; //! \var Soldi \brief Soldi che il giocatore ha a disposizione
        public int Posizione; //! \var Posizione \brief Intero che salva la posizione sul tabellone del giocatore
        public int InPrigione; //! \var InPrigione \brief Intero che conta i turni rimanenti nella InPrigione

        //! \fn Costruttore \brief Crea un giocatore con colore e soldi iniziali prestabiliti
        //! \param C \brief Colore del giocatore
        //! \param S \brief Soldi iniziali del giocatore
        public Giocatore(Brush C, int S)
        {
            Pedina = new Ellipse();
            Pedina.Fill = C;
            Pedina.Stroke = Brushes.Black;
            Pedina.HorizontalAlignment = HorizontalAlignment.Left;
            Pedina.VerticalAlignment = VerticalAlignment.Top;
            Pedina.Width = 20;
            Pedina.Height = 20;
            Soldi = S;
            Proprieta = new List<Casella>();
            Posizione = 0;
            InPrigione = 0;
        }

        //! \fn Compra \brief Compra una proprietà prestabilita
        //! \param C \brief Casella da comprare
        //! \return bool \brief Ritorna falso se il giocatore non ha abbastanza soldi per acquistare o se non è una proprieta valida, altrimenti vero
        public bool Compra(Casella C)
        {
            if (C is Speciali)
                return false;

            Proprieta t = (Proprieta)C;

            if (t.Costo > Soldi)
                return false;

            Soldi -= t.Costo;
            t.Proprietario = this;
            Proprieta.Add(C);
            return true;
        }

        //! \fn Vendi \brief Ipoteca una proprietà prestabilita
        //! \param C \brief Casella da ipotecare
        //! \return bool \brief Ritorna falso se il giocatore non possiede la proprietà, altrimenti vero
        public bool Vendi(Casella C)
        {
            if (C is Speciali || !Proprieta.Contains(C))
                return false;

            Proprieta.Remove(C);

            Proprieta t = (Proprieta)C;
            Soldi += t.Costo / 2;
            t.Ipotecato = true;
            return true;
        }

        //! \fn Vendi \brief Vende una struttura su una proprietà prestabilita
        //! \param S \brief Struttura da vendere
        //! \param C \brief Casella sulla cui agire
        //! \return bool \brief Ritorna falso se il giocatore non possiede la proprietà, non ha il tipo di struttura da vendere o è una proprieta non edificabile, altrimenti vero
        public bool Vendi(Struttura S, Casella C)
        {
            if (!Proprieta.Contains(C))
                return false;

            Proprieta t = (Proprieta)C;
            if (t.Speciale==true)
                return false;

            for(int i = 0; i < t.Strutture.Count; i++)
            {
                if(t.Strutture[i].Tipo == S.Tipo)
                {
                    t.Strutture.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}
