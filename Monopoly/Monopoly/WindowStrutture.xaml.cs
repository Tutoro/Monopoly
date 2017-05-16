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
    /// Logica di interazione per WindowStrutture.xaml
    /// </summary>
    public partial class WindowStrutture : Window
    {
        Giocatore Corrente;
        List<Proprieta> Attive, Temporanee;
        Proprieta Selezionata;
        public WindowStrutture(Giocatore G, Proprieta Pr)
        {
            InitializeComponent();
            Corrente = G;
            int i = 0;
            Attive = new List<Proprieta>();
            Temporanee = new List<Proprieta>();
            foreach (Proprieta P in Corrente.Proprieta)
            {
                if (P.Colore == Pr.Colore)
                {
                    Attive[i] = P;
                    Temporanee[i] = new Proprieta(Brushes.Black, "", 0, false);
                    CheckBox C = new CheckBox();
                    C.Content = P.Nome;
                    C.Background = P.Colore;
                    C.Width = 200;
                    C.Height = 10;
                    C.HorizontalAlignment = HorizontalAlignment.Left;
                    C.VerticalAlignment = VerticalAlignment.Top;
                    C.Checked += CambioSelezione;
                    Stack_Proprieta.Children.Add(C);
                    i++;
                }
            }
        }

        private void CambioSelezione(object sender, RoutedEventArgs e)
        {
            CheckBox C = (CheckBox)sender;
            foreach(CheckBox Ch in Stack_Proprieta.Children)
                if (Ch != C)
                    Ch.IsChecked = false;

            Proprieta P = (Proprieta)Temporanee[Stack_Proprieta.Children.IndexOf(C)];
            if ((bool)C.IsChecked)
                Selezionata = P;
        }

        private void ConfermaStrutture(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < Attive.Count; i++)
            {
                Attive[i].Strutture = Temporanee[i].Strutture;
            }
        }
    }
}
