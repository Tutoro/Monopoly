﻿using System;
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
\version   0.4
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
        public bool InGioco; //! \var InGioco \brief Bool che controlla se il giocatore è in bancarotta o no
        public int BigliettoPrigione; //! \var BigliettoPrigione \brief Intero che controlla quanti biglietti ha il giocatore per uscire di prigione

        //! \fn Giocatore
        //! \brief Crea un giocatore con colore e soldi iniziali prestabiliti
        //! \param C Colore del giocatore
        //! \param S Soldi iniziali del giocatore
        public Giocatore(Brush C, int S)
        {
            Pedina = new Ellipse();
            Pedina.Fill = C;
            Pedina.Stroke = Brushes.Black;
            Pedina.StrokeThickness = 2;
            Pedina.HorizontalAlignment = HorizontalAlignment.Left;
            Pedina.VerticalAlignment = VerticalAlignment.Top;
            Pedina.Width = 20;
            Pedina.Height = 20;
            Soldi = S;
            Proprieta = new List<Casella>();
            InPrigione = 0;
            InGioco = true;
        }

        //! \fn Compra
        //! \brief Compra una proprietà prestabilita
        //! \param C Casella da comprare

        public bool Compra(Proprieta C)
        { 
            Soldi -= C.Costo;
            C.Proprietario = this;
            Proprieta.Add(C);
            return true;
        }

        //! \fn Vendi
        //! \brief Ipoteca una proprietà prestabilita
        //! \param C Casella da ipotecare
        public bool Vendi(Casella C)
        {
            if (C is Speciali || !Proprieta.Contains(C))
                return false;

            Proprieta.Remove(C);

            Proprieta t = (Proprieta)C;
            Soldi += t.Costo / 2;
            t.Proprietario = null;
            t.Ipotecato = true;
            return true;
        }

        //! \fn Vendi
        //! \brief Vende una struttura su una proprietà prestabilita
        //! \param S Struttura da vendere
        //! \param C Casella sulla cui agire
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

        //! \fn SetPosizione
        //! \brief Muove il giocatore sul campo del numero di posizioni specificate
        //! \param P Posizione da impostare
        //! \param I Indice del giocatore sul vettore principale
        //! \param Relative Decide se il movimento va aggiunto alla posizione totale oppure se va direttamente impostato
        public void SetPosizione(int P, int I, bool Relative)
        {
            if (!Relative)
                Posizione += P;
            else
                Posizione = P;

            if (Posizione >= 40)
            {
                Posizione -= 40;
                Soldi += 20000;
            }

            if (Posizione >= 0 && Posizione < 10)
                Pedina.Margin = new Thickness(685 - (Posizione) * 62.5, 685 + (I + 1) * 22, 0, 0);

            if (Posizione >= 10 && Posizione < 20)
                Pedina.Margin = new Thickness(90 + (-I - 1) * 22, 685 - (Posizione - 10) * 62.5, 0, 0);

            if (Posizione >= 20 && Posizione < 30)
                Pedina.Margin = new Thickness(90 + (Posizione - 20) * 62.5, 90 + (-I - 1) * 22, 0, 0);

            if (Posizione >= 30 && Posizione < 40)
                Pedina.Margin = new Thickness(685 + (I + 1) * 22, 90 + (Posizione - 30) * 62.5, 0, 0);
        }

        //! \fn GetValore
        //! \brief Calcola il valore totale dei possedimenti del giocatore
        public int GetValore()
        {
            int Valore = Soldi;
            foreach (Proprieta P in Proprieta)
                Valore += P.Costo + (P.Strutture.Count * (P.Costo / 8));

            return Valore;
        }
    }
}
