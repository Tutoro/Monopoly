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
using Monopoly.Classi;

/*!
\author    Arduini Alberto
\version   0.1
\date      05/05/2017
*/

namespace Monopoly
{
    //! \class MainWindow
    //! \brief Classe della finestra principale
    public partial class MainWindow : Window
    {
        Giocatore[] Giocatori; //! \var Giocatori \brief Vettore che contiene i giocatori
        Casella[] Caselle; //! \var Caselle \brief Vettore che contiene tutte le caselle del tabellone
        Random R; //! \var R \brief Variabile Random

        int Turno; //! \var Turno \brief Variabile intera che contiene di quale giocatore è il turno
        bool Passato; //! \var Passato \brief Variabile booleana che controlla se un giocatore ha tirato i dadi

        public MainWindow(Giocatore[] G)
        {
            InitializeComponent();
            Giocatori = G;
            R = new Random();
            CreaTabellone();
            Passato = true;
            Turno = 0;
            AggiornaInterfaccia();
        }

        void AggiornaInterfaccia()
        {
            TextBox_SoldiUtenti.Text = "";
            for (int i = 0; i < Giocatori.Length; i++)
            {
                if (i == Turno)
                    TextBox_SoldiUtenti.Text += "»Giocatore " + (i + 1) + " |" + Giocatori[i].Soldi;
                else
                    TextBox_SoldiUtenti.Text += "─Giocatore " + (i + 1) + " |" + Giocatori[i].Soldi;

                if(Giocatori[i].InPrigione > 0)
                    TextBox_SoldiUtenti.Text += " (In Prigione per " + Giocatori[i].InPrigione + " turni)" + Environment.NewLine;
                else
                    TextBox_SoldiUtenti.Text += Environment.NewLine;

                if (Giocatori[i].Posizione >= 0 && Giocatori[i].Posizione < 10)
                    Giocatori[i].Pedina.Margin = new Thickness(685 - (Giocatori[i].Posizione) * 62.5, 685 + (i + 1) * 22, 0, 0);

                if (Giocatori[i].Posizione >= 10 && Giocatori[i].Posizione < 20)
                    Giocatori[i].Pedina.Margin = new Thickness(90 + (-i - 1) * 22, 685 - (Giocatori[i].Posizione - 10) * 62.5, 0, 0);

                if (Giocatori[i].Posizione >= 20 && Giocatori[i].Posizione < 30)
                    Giocatori[i].Pedina.Margin = new Thickness(90 + (Giocatori[i].Posizione - 20) * 62.5, 90 + (-i - 1) * 22, 0, 0);

                if (Giocatori[i].Posizione >= 30 && Giocatori[i].Posizione < 40)
                    Giocatori[i].Pedina.Margin = new Thickness(685 + (i + 1) * 22, 90 + (Giocatori[i].Posizione - 30) * 62.5, 0, 0);

            }
            if (Passato)
            {
                Button_Scambia.IsEnabled = true;
                Button_Compra.IsEnabled = false;
                Button_Passa.Content = "Avanza";
            }
            else
            {
                Button_Scambia.IsEnabled = false;
                Button_Compra.IsEnabled = false;
                Button_Passa.Content = "Passa";

                if (Caselle[Giocatori[Turno].Posizione] is Proprieta)
                {
                    Proprieta ProprietaCorrente = (Proprieta)Caselle[Giocatori[Turno].Posizione];
                    if (ProprietaCorrente.Proprietario == null)
                        Button_Compra.IsEnabled = true;
                    else if (ProprietaCorrente.Proprietario != Giocatori[Turno])
                    {
                        ProprietaCorrente.Proprietario.Soldi += ProprietaCorrente.Costo / 4;
                        Giocatori[Turno].Soldi -= ProprietaCorrente.Costo / 4;
                    }
                }
            }
        }

        void CreaTabellone()
        {
            Caselle = new Casella[40];
            Caselle[0] = new Speciali("Via!", Tipo_Speciali.Tassa, -20000);
            Caselle[1] = new Proprieta(Brushes.Brown, "Vicolo Corto", 6000, false);
            Caselle[2] = new Speciali("Probabilità", Tipo_Speciali.Probabilita, 0);
            Caselle[3] = new Proprieta(Brushes.Brown, "Vicolo Stretto", 6000, false);
            Caselle[4] = new Speciali("Tassa Patrimoniale", Tipo_Speciali.Tassa, 20000);
            Caselle[5] = new Proprieta(Brushes.Black, "Stazione Sud", 20000, true);
            Caselle[6] = new Proprieta(Brushes.LightBlue, "Bastioni Gran Sasso", 10000, false);
            Caselle[7] = new Speciali("Imprevisti", Tipo_Speciali.Imprevisti, 0);
            Caselle[8] = new Proprieta(Brushes.LightBlue, "Viale Monterosa", 10000, false);
            Caselle[9] = new Proprieta(Brushes.LightBlue, "Viale Vesuvio", 12000, false);
            Caselle[10] = new Speciali("InPrigione / Transito", Tipo_Speciali.Parcheggio, 0);
            Caselle[11] = new Proprieta(Brushes.Orange, "Via Accademia", 14000, false);
            Caselle[12] = new Proprieta(Brushes.Yellow, "Società Elettrica", 15000, true);
            Caselle[13] = new Proprieta(Brushes.Orange, "Corso Ateneo", 14000, false);
            Caselle[14] = new Proprieta(Brushes.Orange, "Piazza Università", 16000, false);
            Caselle[15] = new Proprieta(Brushes.Black, "Stazione Ovest", 20000, true);
            Caselle[16] = new Proprieta(Brushes.Brown, "Via Verdi", 18000, false);
            Caselle[17] = new Speciali("Probabilità", Tipo_Speciali.Probabilita, 0);
            Caselle[18] = new Proprieta(Brushes.Brown, "Corso Raffaello", 18000, false);
            Caselle[19] = new Proprieta(Brushes.Brown, "Piazza Dante", 20000, false);
            Caselle[20] = new Speciali("Parcheggio", Tipo_Speciali.Parcheggio, 0);
            Caselle[21] = new Proprieta(Brushes.Red, "Via Marco Polo", 22000, false);
            Caselle[22] = new Speciali("Imprevisti", Tipo_Speciali.Imprevisti, 0);
            Caselle[23] = new Proprieta(Brushes.Red, "Corso Magellano", 22000, false);
            Caselle[24] = new Proprieta(Brushes.Red, "Largo Colombo", 24000, false);
            Caselle[25] = new Proprieta(Brushes.Black, "Stazione Nord", 20000, true);
            Caselle[26] = new Proprieta(Brushes.Yellow, "Viale Costantino", 26000, false);
            Caselle[27] = new Proprieta(Brushes.Yellow, "Viale Traiano", 26000, false);
            Caselle[28] = new Proprieta(Brushes.Yellow, "Società Acqua Potabile", 15000, true);
            Caselle[29] = new Proprieta(Brushes.Yellow, "Piazza Giulio Cesare", 28000, false);
            Caselle[30] = new Speciali("In InPrigione!", Tipo_Speciali.Prigione, 0);
            Caselle[31] = new Proprieta(Brushes.Green, "Via Roma", 30000, false);
            Caselle[32] = new Proprieta(Brushes.Green, "Corso Impero", 30000, false);
            Caselle[33] = new Speciali("Probabilità", Tipo_Speciali.Probabilita, 0);
            Caselle[34] = new Proprieta(Brushes.Green, "Largo Augusto", 32000, false);
            Caselle[35] = new Proprieta(Brushes.Black, "Stazione Est", 20000, true);
            Caselle[36] = new Speciali("Imprevisti", Tipo_Speciali.Imprevisti, 0);
            Caselle[37] = new Proprieta(Brushes.Blue, "Viale Dei Giardini", 35000, false);
            Caselle[38] = new Speciali("Tassa di Lusso", Tipo_Speciali.Tassa, 10000);
            Caselle[39] = new Proprieta(Brushes.Blue, "Parco Della Vittoria", 40000, false);

            for (int i = 0; i < Giocatori.Length; i++)
            {
                Ellipse Pedina = Giocatori[i].Pedina;
                Griglia_Principale.Children.Add(Pedina);
            }
        }

        private void PassaTurno(object sender, RoutedEventArgs e)
        {
            if (Passato)
            {
                int Risultato = R.Next(1, 7) + R.Next(1, 7);

                MessageBox.Show(Risultato.ToString());

                if (Giocatori[Turno].InPrigione > 0 && Risultato < 12)
                {
                    if (Giocatori[Turno].InPrigione == 1)
                        Giocatori[Turno].Soldi -= 5000;
                    Giocatori[Turno].InPrigione--;
                }
                else
                {
                    Giocatori[Turno].Posizione += Risultato;
                    if (Giocatori[Turno].Posizione >= Caselle.Length)
                    {
                        Giocatori[Turno].Soldi += 20000;
                        Giocatori[Turno].Posizione -= 40;
                    }
                    if (Caselle[Giocatori[Turno].Posizione] is Speciali)
                    {
                        Speciali Casella = (Speciali)Caselle[Giocatori[Turno].Posizione];
                        switch (Casella.Tipo)
                        {
                            case Tipo_Speciali.Tassa:
                                Giocatori[Turno].Soldi -= Casella.Tassa_Costo;
                                break;
                            case Tipo_Speciali.Prigione:
                                MessageBox.Show("Vai in Prigione!");
                                Giocatori[Turno].InPrigione = 3;
                                Giocatori[Turno].Posizione = 10;
                                break;
                        }
                    }
                }
                Passato = false;
            }
            else
            {
                Turno++;
                if (Turno == Giocatori.Length)
                    Turno = 0;
                Passato = true;

                MessageBox.Show("E' il turno del Giocatore " + (Turno + 1));
            }

            AggiornaInterfaccia();
        }

        private void CompraProprieta(object sender, RoutedEventArgs e)
        {
            if (Giocatori[Turno].Compra(Caselle[Giocatori[Turno].Posizione]))
                MessageBox.Show("Non hai abbastanza soldi");

            AggiornaInterfaccia();
        }
    }
}