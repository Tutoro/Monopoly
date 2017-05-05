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

namespace Monopoly
{
    //! \class MainWindow \brief Classe della finestra principale
    public partial class MainWindow : Window
    {
        Giocatore[] Giocatori; //! \var Giocatori \brief Vettore che contiene i giocatori
        Casella[] Caselle; //! \var Caselle \brief Vettore che contiene tutte le caselle del tabellone
        Random R; //! \var R \brief Variabile Random

        int Turno; //! \var Turno \brief Variabile intera che contiene di quale giocatgore è il turno

        public MainWindow(Giocatore[] G)
        {
            InitializeComponent();
            Giocatori = G;
            R = new Random();
            CreaTabellone();

            Turno = 0;
            AggiornaInterfaccia();
        }

        void AggiornaInterfaccia()
        {
            TextBox_SoldiUtenti.Text = "";
            for (int i = 0; i < Giocatori.Length; i++)
            {
                if (i == Turno)
                    TextBox_SoldiUtenti.Text += "»Giocatore " + (i + 1) + " |" + Giocatori[i].Soldi + Environment.NewLine;
                else
                    TextBox_SoldiUtenti.Text += "─Giocatore " + (i + 1) + " |" + Giocatori[i].Soldi + Environment.NewLine;
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
            Caselle[10] = new Speciali("Prigione / Transito", Tipo_Speciali.Parcheggio, 0);
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
            Caselle[30] = new Speciali("In Prigione!", Tipo_Speciali.Prigione, 0);
            Caselle[31] = new Proprieta(Brushes.Green, "Via Roma", 30000, false);
            Caselle[32] = new Proprieta(Brushes.Green, "Corso Impero", 30000, false);
            Caselle[33] = new Speciali("Probabilità", Tipo_Speciali.Probabilita, 0);
            Caselle[34] = new Proprieta(Brushes.Green, "Largo Augusto", 32000, false);
            Caselle[35] = new Proprieta(Brushes.Black, "Stazione Est", 20000, true);
            Caselle[36] = new Speciali("Imprevisti", Tipo_Speciali.Imprevisti, 0);
            Caselle[37] = new Proprieta(Brushes.Blue, "Viale Dei Giardini", 35000, false);
            Caselle[38] = new Speciali("Tassa di Lusso", Tipo_Speciali.Tassa, 10000);
            Caselle[39] = new Proprieta(Brushes.Blue, "Parco Della Vittoria", 40000, false);
        }

        private void PassaTurno(object sender, RoutedEventArgs e)
        {
            Turno++;
            if (Turno == 4)
                Turno = 0;
            AggiornaInterfaccia();
        }
    }
}
