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
        Proprieta Selezionata;
        MainWindow Principale;
        List<Proprieta> Temp;

        public WindowStrutture(Giocatore G, MainWindow Pr)
        {
            IsVisibleChanged += CreaLista;
            InitializeComponent();
            Corrente = G;
            Principale = Pr;
            Button_Aggiungi.IsEnabled = false;
            Button_Rimuovi.IsEnabled = false;
        }

        private void CreaLista(object sender, DependencyPropertyChangedEventArgs e)
        {
            Casella CC = Principale.Caselle[Corrente.Posizione];
            Temp = new List<Proprieta>();
            for (int i = 0; i < Corrente.Proprieta.Count; i++)
            {
                if (Corrente.Proprieta[i].Colore == CC.Colore)
                    Temp.Add((Proprieta)Corrente.Proprieta[i]);
            }

            if (Temp.Count == 3 || (Temp.Count == 2 && (CC.Colore == Brushes.Brown || CC.Colore == Brushes.Blue)))
            {
                foreach (Proprieta P in Temp)
                {
                    CheckBox C = new CheckBox();
                    C.HorizontalAlignment = HorizontalAlignment.Left;
                    C.VerticalAlignment = VerticalAlignment.Top;
                    C.Width = 200;
                    C.Height = 20;
                    C.Checked += CambiaSelezione;
                    C.Unchecked += CambiaSelezione;
                    C.Content = P.Nome + " (" + P.Strutture.Count + ")";
                    C.Background = P.Colore;
                    Stack_Proprieta.Children.Add(C);
                }
            }
            else if(Visibility == Visibility.Visible)
            {
                MessageBox.Show("Non hai proprietà adatte alla costruzione di strutture!");
                this.Visibility = Visibility.Collapsed;
                this.Close();
            }
        }

        private void CambiaSelezione(object sender, RoutedEventArgs e)
        {
            CheckBox S = (CheckBox)sender;
            foreach (CheckBox C in Stack_Proprieta.Children)
                if (C != S && (bool)S.IsChecked)
                    C.IsChecked = false;

            if ((bool)S.IsChecked)
            {
                Button_Aggiungi.IsEnabled = true;
                Button_Rimuovi.IsEnabled = true;
            }
            else
            {
                Button_Aggiungi.IsEnabled = false;
                Button_Rimuovi.IsEnabled = false;
            }

            Selezionata = Temp[Stack_Proprieta.Children.IndexOf((UIElement)sender)];
            if (Selezionata.Strutture.Count < 1)
                Button_Rimuovi.IsEnabled = false;
            else if (!Selezionata.Strutture[0].Tipo)
                Button_Aggiungi.IsEnabled = false;
        }

        void AggiornaInterfaccia()
        {
            for(int i = 0; i < Temp.Count; i++)
            {
                CheckBox C = (CheckBox)Stack_Proprieta.Children[i];
                Proprieta P = Temp[i];
                C.Content = P.Nome + " (" + P.Strutture.Count + ")";
            }
            Button_Aggiungi.IsEnabled = true;
            Button_Rimuovi.IsEnabled = true;

            if (Selezionata.Strutture.Count < 1)
                Button_Rimuovi.IsEnabled = false;
            else if (!Selezionata.Strutture[0].Tipo)
                Button_Aggiungi.IsEnabled = false;

            Principale.AggiornaDaStrutture();
        }

        private void ConfermaStrutture(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Risposta = MessageBox.Show("Sei sicuro di voler costruire su questa proprietà per L." + Selezionata.Costo / 8 + "?", "Conferma", MessageBoxButton.OKCancel);

            if (Risposta.HasFlag(MessageBoxResult.OK))
            {
                Corrente.Soldi -= Selezionata.Costo / 8;
                Selezionata.AddStruttura();

                AggiornaInterfaccia();
            }
        }

        private void RimuoviProprieta(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Risposta = MessageBox.Show("Sei sicuro di voler rimuovere una struttura su questa proprietà per L." + Selezionata.Costo / 16 + "?", "Conferma", MessageBoxButton.OKCancel);

            if (Risposta.HasFlag(MessageBoxResult.OK))
            {
                Corrente.Soldi += Selezionata.Costo / 16;
                Selezionata.RemoveStruttura();

                AggiornaInterfaccia();
            }
        }
    }
}
