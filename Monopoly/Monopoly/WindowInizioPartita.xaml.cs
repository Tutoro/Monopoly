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
    /// Logica di interazione per WindowInizioPartita.xaml
    /// </summary>
    public partial class WindowInizioPartita : Window
    {
        Giocatore[] Giocatori;
        Brush[] Colori;

        public WindowInizioPartita()
        {
            InitializeComponent();
            Giocatori = new Giocatore[1];
            Colori = new Brush[4];

            Colori[0] = Brushes.Black;
            Colori[1] = Brushes.Beige;
            Colori[2] = Brushes.Aquamarine;
            Colori[3] = Brushes.Crimson;
        }

        private void InizioPartita(object sender, RoutedEventArgs e)
        {
            new MainWindow(Giocatori);
        }

        private void AggiornaGiocatori(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(TextBox_NumeroGiocatori.Text) < 1)
                {
                    TextBox_NumeroGiocatori.Text = "1";
                }
                if (Convert.ToInt32(TextBox_NumeroGiocatori.Text) > 4)
                {
                    TextBox_NumeroGiocatori.Text = "4";
                }
            }
            catch
            {
                TextBox_NumeroGiocatori.Text = "1";
            }

            Giocatori = new Giocatore[Convert.ToInt32(TextBox_NumeroGiocatori.Text)];
            for(int i = 0; i < Giocatori.Length; i++)
            {
                Giocatori[i] = new Giocatore(Colori[i], 120000 / Giocatori.Length);
            }
        }
    }
}
