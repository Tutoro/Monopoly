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
using System.Windows.Shapes;
using Monopoly.Classi;

namespace Monopoly
{
    /// <summary>
    /// Logica di interazione per WindowScambio.xaml
    /// </summary>
    public partial class WindowScambio : Window
    {
        List<Giocatore> Altri;
        Giocatore giocatoreOriginale;
        Giocatore giocatoreSelezionato;
        List<Proprieta> selezionateUno;
        List<Proprieta> selezionateDue;
        int i;                              //inizializzo una variabile per gestire i cicli for
        bool c1 = false, c2 = false;        // variabili di stato per la conferma dell'invio del contratto
        public WindowScambio(Giocatore[] A, Giocatore G) //albi qui mi passi una lista dei giocatori e il giocatore che richiede di effettuare lo scambio
        {
            Altri = new List<Giocatore>();      //inizializzo una lista che conterrà tutti i giocatori escludendo quello che vuole scambiare
            selezionateUno = new List<Proprieta>();
            selezionateDue = new List<Proprieta>();
            giocatoreOriginale = G;
            for (i = 0; i < A.Length; i++)      //ciclo all'interno di listaGiocatori i nomi di coloro che stanno giocando
            {
                if (A[i] == G)                          //controllo se nell'array che mi arriva, si trova il giocatore che ha richiesto lo scambio
                {
                    nomeGiocatore.Content = G;          //aggiorno il contenuto di nomeGiocatore con il nome di colui che ha inizializzato la funzione scambio
                }
                else
                {
                    listaGiocatori.Items.Add(A[i]);     //aggiungo i nomi dei giocatori selezionabili alla combobox
                    Altri.Add(A[i]);                    //aggiungo i nomi dei giocatori (escludendo colui che ha richiesto lo scambio) ad una lista
                }
            }
            foreach (Proprieta P in giocatoreOriginale.Proprieta)                        // ciclo per inserire le proprietà del giocatore che invia il contratto dentro la combobox
            {
                proprietaUno.Items.Add(P.Nome);
            }
        }

        private void sceltaGiocatore()    //evento che si avvia quando viene scelto un giocatore dalla lista (nella combobox)
        {
            proprietaDue.Items.Clear();     //svuoto la combobox delle proprietà nel caso venga cambiato giocatore
            listaProprietaDue.Items.Clear();    //svuoto la listbox nel caso venga cambiato il giocatore
            giocatoreSelezionato = Altri[listaGiocatori.SelectedIndex];   //inserisco il giocatore selezionato in Giocatore
            foreach (Proprieta P in giocatoreSelezionato.Proprieta)                        // ciclo per inserire le proprietà del giocatore selezionato dentro la combobox
            {
                proprietaDue.Items.Add(P.Nome);
            }
        }

        private void aggiungiUno()  //funzione per aggiungere l'elemento selezionato dalla combobox alla listbox, controlla inoltre se già presente (in caso affermativo, non aggiunge)
        {
            String tmp = "";
            tmp = proprietaUno.Text;
            foreach (object Item in listaProprietaUno.Items)
            {
                String str = Item.ToString();
                if (str == tmp)
                {
                    MessageBox.Show("La proprietà che si tenta di aggiungere è già presente all'interno della lista delle proprietà selezionate", "Errore!");
                }
                else
                {
                    listaProprietaUno.Items.Add(tmp);
                    selezionateUno.Add((Proprieta)giocatoreOriginale.Proprieta[listaProprietaUno.Items.IndexOf(Item)]);
                }
            }
        }

        private void aggiungiDue()  //funzione per aggiungere l'elemento selezionato dalla combobox alla listbox, controlla inoltre se già presente (in caso affermativo, non aggiunge)
        {
            String tmp = "";
            tmp = proprietaDue.Text;
            foreach (object Item in listaProprietaDue.Items)
            {
                String str = Item.ToString();
                if (str == tmp)
                {
                    MessageBox.Show("La proprietà che si tenta di aggiungere è già presente all'interno della lista delle proprietà selezionate", "Errore!");
                }
                else
                {
                    listaProprietaDue.Items.Add(tmp);
                    selezionateDue.Add((Proprieta)giocatoreSelezionato.Proprieta[listaProprietaDue.Items.IndexOf(Item)]);
                }
            }
        }

        private void rimuoviElementoUno()   //funzione per rimuovere elementi dalla lista delle proprietà selezionate del giocatore che ha inviato l'offerta
        {
            if (listaProprietaUno.SelectedItem != null) //controlla se è stato selezionata la proprietà da rimuovere
            {
                selezionateUno.RemoveAt(listaProprietaUno.SelectedIndex);
                listaProprietaUno.Items.Remove(listaProprietaUno.SelectedItem); //rimuove l'elemento dalla lista
            }
        }

        private void rimuoviElementoDue()   //funzione per rimuovere elementi dalla lista delle proprietà selezionate del giocatore che deve ricevere l'offerta
        {
            if (listaProprietaDue.SelectedItem != null) //controlla se è stato selezionata la proprietà da rimuovere
            {
                selezionateDue.RemoveAt(listaProprietaDue.SelectedIndex);
                listaProprietaDue.Items.Remove(listaProprietaDue.SelectedItem); //rimuove l'elemento dalla lista
            }
        }

        private void confermaUno()
        {
            // controllo per cambio colore bottone e aggiornamento variabili di stato
            if (sendUno.Background != Brushes.Green)
            {
                sendUno.Background = Brushes.Green;
                c1 = true;
            }
            else
            {
                sendUno.Background = Brushes.Red;
                c1 = false;
            }

            //controlla che entrambi le variabili di stato siano true, nel caso siano entrambi vere richiama la funzione per completare lo scambio
            if (c2 == true && c1 == true)
            {
                inviaContratto();
            }
        }

        private void confermaDue()
        {
            // controllo per cambio colore bottone e aggiornamento variabili di stato
            if (sendDue.Background != Brushes.Green)
            {
                sendDue.Background = Brushes.Green;
                c2 = true;
            }
            else
            {
                sendDue.Background = Brushes.Red;
                c2 = false;
            }

            //controlla che entrambi le variabili di stato siano true, nel caso siano entrambi vere richiama la funzione per completare lo scambio
            if (c1 == true && c2 == true)
            {
                inviaContratto();
            }
        }

        private void annullaScambio()
        {
            this.Close();   //annulla lo scambio e chiude la finestra
        }

        private void inviaContratto()
        {
            //ciclo che scambia le proprietà dal giocatore che ha offerto lo scambio al giocatore che ha ricevuto lo scambio
            foreach (Proprieta P in selezionateUno)    //per ogni elemento presente dentro la TextBox delle proprietà selezionate del giocatore che ha iniziato lo scambio
            {
                giocatoreSelezionato.Proprieta.Add(P);       //aggiunge la proprietà alla lista del giocatore che ha ricevuto la richiesta di scambio
                giocatoreOriginale.Proprieta.Remove(P);      //rimuove la proprietà alla lista del giocatore che ha ricevuto la richiesta di scambio 
            }

            //ciclo che scambia le proprietà dal giocatore che ha ricevuto lo scambio al giocatore che ha offerto lo scambio
            foreach (Proprieta P in selezionateUno)      //per ogni elemento presente dentro la TextBox delle proprietà selezionate del giocatore che ha ricevuto lo scambio
            {
                giocatoreOriginale.Proprieta.Add(P);         //aggiunge la proprietà alla lista del giocatore che ha effettuato la richiesta di scambio
                giocatoreSelezionato.Proprieta.Remove(P);    //rimuove la proprietà alla lista del giocatore che ha effettuato la richiesta di scambio
            }

            if (vaultUno.Text != "0" && Convert.ToInt32(vaultUno) <= giocatoreOriginale.Soldi)   //controlla se sono stati inseriti dei soldi nel contratto da parte del giocatore che richiesto lo scambio e se ci sono soldi a sufficienza per effettuare lo scambio
            {
                giocatoreSelezionato.Soldi += Convert.ToInt32(vaultUno);    //aggiunge i soldi al portafoglio del giocatore che ha ricevuto la richiesta di scambio
            }

            if (vaultDue.Text != "0" && Convert.ToInt32(vaultDue) <= giocatoreSelezionato.Soldi)   //controlla se sono stati inseriti dei soldi nel contratto da parte del giocatore che ricevuto lo scambio
            {
                giocatoreOriginale.Soldi += Convert.ToInt32(vaultDue);      //aggiunge i soldi al portafoglio del giocatore che ha inviato la richiesta di scambio e e se ci sono soldi a sufficienza per effettuare lo scambio
            }
            this.Close();
        } 
    }
}