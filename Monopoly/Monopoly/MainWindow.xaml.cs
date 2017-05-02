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
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Giocatore[] Giocatori;
        Casella[] Caselle;
        Random R;

        public MainWindow(Giocatore[] G)
        {
            InitializeComponent();
            Giocatori = G;
            R = new Random();
            CreaTabellone();
        }

        void CreaTabellone()
        {
            Caselle = new Casella[36];
            Caselle[0] = new Speciali();
            Caselle[1] = new Proprieta("Vicolo Corto", 6000, Brushes.Brown);
            Caselle[2] = new Speciali();
            Caselle[3] = new Proprieta("Vicolo Stretto", 6000, Brushes.Brown);
            Caselle[4] = new Speciali();
            Caselle[5] = new Proprieta_Speciali("Stazione Sud", 20000, Brushes.Black);
            Caselle[6] = new Proprieta("Bastioni Gran Sasso", 10000, Brushes.LightBlue);
            Caselle[7] = new Speciali();
            Caselle[8] = new Proprieta("Viale Monterosa", 10000, Brushes.LightBlue);
            Caselle[9] = new Proprieta("Viale Vesuvio", 12000, Brushes.LightBlue);
            Caselle[10] = new Speciali();
            Caselle[11] = new Proprieta("Via Accademia", 14000, Brushes.Orange);
            Caselle[12] = new Proprieta_Speciali("Società Elettrica", 15000, Brushes.Yellow);
            Caselle[13] = new Proprieta("Corso Ateneo", 14000, Brushes.Orange);
            Caselle[14] = new Proprieta("Piazza Università", 16000, Brushes.Orange);
            Caselle[15] = new Proprieta_Speciali("Stazione Ovest", 20000, Brushes.Black);
            Caselle[16] = new Proprieta("Via Verdi", 18000, Brushes.Brown);
            Caselle[17] = new Speciali();
            Caselle[18] = new Proprieta("Corso Raffaello", 18000, Brushes.Brown);
            Caselle[19] = new Proprieta("Piazza Dante", 20000, Brushes.Brown);
            Caselle[20] = new Speciali();
            Caselle[21] = new Proprieta("Via Marco Polo", 22000, Brushes.Red);
            Caselle[22] = new Speciali();
            Caselle[23] = new Proprieta("Corso Magellano", 22000, Brushes.Red);
            Caselle[24] = new Proprieta("Largo Colombo", 24000, Brushes.Red);
            Caselle[25] = new Proprieta_Speciali("Stazione Nord", 20000, Brushes.Black);
            Caselle[26] = new Proprieta("Viale Costantino", 26000, Brushes.Yellow);
            Caselle[27] = new Proprieta("Viale Traiano", 26000, Brushes.Yellow);
            Caselle[28] = new Proprieta_Speciali("Società Acqua Potabile", 15000, Brushes.Yellow);
            Caselle[29] = new Proprieta("Piazza Giulio Cesare", 28000, Brushes.Yellow);
            Caselle[30] = new Speciali();
            Caselle[31] = new Proprieta("Via Roma", 30000, Brushes.Green);
            Caselle[32] = new Proprieta("Corso Impero", 30000, Brushes.Green);
            Caselle[33] = new Speciali();
            Caselle[34] = new Proprieta("Largo Augusto", 32000, Brushes.Green);
            Caselle[35] = new Proprieta_Speciali("Stazione Est", 20000, Brushes.Black);
            Caselle[36] = new Speciali();
            Caselle[37] = new Proprieta("Viale Dei Giardini", 35000, Brushes.Blue);
            Caselle[38] = new Speciali();
            Caselle[39] = new Proprieta("Parco Della Vittoria", 40000, Brushes.Blue);
        }
    }
}
