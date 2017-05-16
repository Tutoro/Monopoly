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
    /// Logica di interazione per WindowIpoteca.xaml
    /// </summary>
    public partial class WindowIpoteca : Window
    {
        Giocatore Corrente;
        int GuadagnoTotale;
        public WindowIpoteca(Giocatore G)
        {
            InitializeComponent();
            Corrente = G;
            foreach(Proprieta P in Corrente.Proprieta)
            {
                CheckBox C = new CheckBox();
                C.HorizontalAlignment = HorizontalAlignment.Left;
                C.VerticalAlignment = VerticalAlignment.Top;
                C.Content = P.Nome;
                C.Background = P.Colore;
                C.Width = 200;
                C.Height = 20;
                C.Checked += CambiaSelezione;
                Stack_Proprieta.Children.Add(C);
            }
        }

        private void CambiaSelezione(object sender, RoutedEventArgs e)
        {
            CheckBox C = (CheckBox)sender;
            Proprieta P = (Proprieta)Corrente.Proprieta[Stack_Proprieta.Children.IndexOf(C)];
            if ((bool)C.IsChecked)
                GuadagnoTotale += P.Costo;
        }
    }
}
