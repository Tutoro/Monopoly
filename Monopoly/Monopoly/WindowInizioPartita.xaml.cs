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
        int Turni = 0;
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

        private void InizioPartita(object sender, MouseButtonEventArgs e)
        {
            if (checkBox_Finiti.IsChecked == false && checkBox_Infiniti.IsChecked == false)
            {
                MessageBox.Show("Devi ancora scegliere il numero di turni!");
            }
            else if(checkBox_Finiti.IsChecked == true && textBox_ScegliTurni.Text=="")
            {
                MessageBox.Show("Devi ancora inserire il numero di turni!");
            }
            else
            {
                ControllaGiocatori();
                if (checkBox_Finiti.IsChecked == true)
                    Turni = Convert.ToInt32(textBox_ScegliTurni.Text);
                else
                    Turni = 0;

                new MainWindow(Giocatori, Turni).Show();
                this.Close();
            }
        }

        private void AggiornaGiocatori(object sender, KeyboardFocusChangedEventArgs e)
        {
            ControllaGiocatori();
        }

        void ControllaGiocatori()
        {
            try
            {
                if (Convert.ToInt32(TextBox_NumeroGiocatori.Text) < 2)
                    TextBox_NumeroGiocatori.Text = "1";
                if (Convert.ToInt32(TextBox_NumeroGiocatori.Text) > 4)
                    TextBox_NumeroGiocatori.Text = "4";
            }
            catch
            {
                TextBox_NumeroGiocatori.Text = "1";
            }

            Giocatori = new Giocatore[Convert.ToInt32(TextBox_NumeroGiocatori.Text)];
            for (int i = 0; i < Giocatori.Length; i++)
            {
                Giocatori[i] = new Giocatore(Colori[i], 240000 / Giocatori.Length);
            }
        }
        private void check_TurniInfiniti(object sender, RoutedEventArgs e)
        {
            canvas_turni.Visibility = Visibility.Collapsed;
            checkBox_Finiti.IsChecked = false;
        }
        private void check_TurniFiniti(object sender, RoutedEventArgs e)
        {
            if (!(bool)checkBox_Finiti.IsChecked)
            {
                canvas_turni.Visibility = Visibility.Collapsed;
            }
            else
            {
                canvas_turni.Visibility = Visibility.Visible;
            }
            checkBox_Infiniti.IsChecked = false;
        }

        private void Cambio_Colore(object sender, MouseEventArgs e)
        {
            Button_Conferma.Background = null;
            BrushConverter B = new BrushConverter();
            Button_Conferma.Foreground = (Brush)B.ConvertFromString("#FFE41F1F");
        }
    }
}
