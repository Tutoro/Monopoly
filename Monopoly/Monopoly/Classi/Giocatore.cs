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
\version   0.1
\date      02/05/2017
*/

namespace Monopoly.Classi
{
    //! \class Giocatore
    //! \brief Classe che rappresenta il giocatore nel programma
    public class Giocatore
    {
        public Brush Colore; //! \var Colore \brief Colore che rappresenta il giocatore sul campo
        public List<Casella> Proprieta; //! \var Proprieta \brief Lista di Caselle che contiene le Proprietà (normali e speciali) che il giocatore possiede
        public int Soldi; //! \var Soldi \brief Soldi che il giocatore ha a disposizione

        //! \fn Costruttore \brief Crea un giocatore con colore e soldi iniziali prestabiliti
        //! \param C \brief Colore del giocatore
        //! \param S \brief Soldi iniziali del giocatore
        public Giocatore(Brush C, int S)
        {
            Colore = C;
            Soldi = S;
            Proprieta = new List<Casella>();
        }

        //! \fn Compra \brief Compra una proprietà prestabilita
        //! \param C \brief Casella da comprare
        //! \return bool \brief Ritorna falso se il giocatore non ha abbastanza soldi per acquistare, altrimenti vero
        public bool Compra(Casella C)
        {
            if (C.Costo > Soldi)
                return false;
            Proprieta.Add(C);
            return true;
        }

        //! \fn Vendi \brief Ipoteca una proprietà prestabilita
        //! \param C \brief Casella da ipotecare
        //! \return bool \brief Ritorna falso se il giocatore non possiede la proprietà, altrimenti vero
        public bool Vendi(Casella C)
        {
            if (!Proprieta.Contains(C))
                return false;
            Proprieta.Remove(C);
            Soldi += C.Costo / 2;
            C.Ipotecato = true;
            return true;
        }

        //! \fn Vendi \brief Vende una struttura su una proprietà prestabilita
        //! \param S \brief Struttura da vendere
        //! \param C \brief Casella sulla cui agire
        //! \return bool \brief Ritorna falso se il giocatore non possiede la proprietà o non ha il tipo di struttura da vendere, altrimenti vero
        public bool Vendi(Struttura S, Casella C)
        {
            if (!Proprieta.Contains(C))
                return false;
            for(int i = 0; i < C.Strutture.Count; i++)
            {
                if(C.Strutture[i].Tipo == S.Tipo)
                {
                    C.Strutture.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}
