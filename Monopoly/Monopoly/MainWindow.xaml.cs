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
        TextBlock[] TextBlockSoldi;
        Carta[] Probabilita, Imprevisti;

        int Turno; //! \var Turno \brief Variabile intera che contiene di quale giocatore è il turno
        bool Passato; //! \var Passato \brief Variabile booleana che controlla se un giocatore ha tirato i dadi

        public MainWindow(Giocatore[] G, int Turni)
        {
            InitializeComponent();
            Giocatori = G;
            R = new Random();
            TextBlockSoldi = new TextBlock[4];
            TextBlockSoldi[0] = TextBlock_SoldiG1;
            TextBlockSoldi[1] = TextBlock_SoldiG2;
            TextBlockSoldi[2] = TextBlock_SoldiG3;
            TextBlockSoldi[3] = TextBlock_SoldiG4;
            for (int i = 0; i < 4; i++)
            {
                if (i >= Giocatori.Length)
                    TextBlockSoldi[i].Visibility = Visibility.Collapsed;
            }
            CreaTabellone();
            Passato = true;
            Turno = 0;
            AggiornaInterfaccia();
        }

        void AggiornaInterfaccia()
        {
            for (int i = 0; i < Giocatori.Length; i++)
                TextBlockSoldi[i].Text = "";
            
            StackPanel_ProprietaUtente.Children.Clear();

            for (int i = 0; i < Giocatori.Length; i++)
            {
                if (i == Turno)
                    TextBlockSoldi[i].Text += "»Giocatore " + (i + 1) + " |" + Giocatori[i].Soldi;
                else
                    TextBlockSoldi[i].Text += "─Giocatore " + (i + 1) + " |" + Giocatori[i].Soldi;

                if (Giocatori[i].InPrigione > 0)
                    TextBlockSoldi[i].Text += " (In Prigione per " + Giocatori[i].InPrigione + " turni)";

                int Altezza_Prossima = 0;

                if (Giocatori[i].Proprieta.Count > 0)
                {
                    TextBlock R = new TextBlock();
                    R.TextAlignment = TextAlignment.Left;
                    R.HorizontalAlignment = HorizontalAlignment.Left;
                    R.VerticalAlignment = VerticalAlignment.Top;
                    R.Width = 200;
                    R.Height = 44;
                    R.Margin = new Thickness(0, Altezza_Prossima, 0, 0);
                    R.Text = Environment.NewLine + "Proprietà Giocatore " + (i + 1) + ":";
                    BrushConverter B = new BrushConverter();
                    R.Background = (Brush)B.ConvertFromString("#FFCDE7CE");
                    StackPanel_ProprietaUtente.Children.Add(R);

                    foreach (Proprieta P in Giocatori[i].Proprieta)
                    {
                        StackPanel_ProprietaUtente.Children.Add(P.GetInterfaccia(ref Altezza_Prossima));
                    }

                    for(int c = 0; c < Giocatori[i].BigliettoPrigione; c++)
                    {
                        R.Height = 22;
                        R.Margin = new Thickness(0, Altezza_Prossima, 0, 0);
                        Altezza_Prossima += 2;
                        R.Background = Brushes.Black;
                        R.Foreground = Brushes.White;
                        R.Text = "Uscita gratis di Prigione!";
                        StackPanel_ProprietaUtente.Children.Add(R);
                    }
                }
            }
            if (Passato)
            {
                Button_Scambia.IsEnabled = true;
                Button_CompraProprieta.IsEnabled = false;
                Button_VendiProprieta.IsEnabled = true;
                if (Giocatori[Turno].Proprieta.Count == 0)
                    Button_VendiProprieta.IsEnabled = false;
                Button_Passa.Content = "Avanza";
            }
            else
            {
                Button_Scambia.IsEnabled = false;
                Button_CompraProprieta.IsEnabled = false;
                Button_VendiProprieta.IsEnabled = true;
                if (Giocatori[Turno].Proprieta.Count == 0)
                    Button_VendiProprieta.IsEnabled = false;
                Button_Passa.Content = "Passa";

                if (Caselle[Giocatori[Turno].Posizione] is Proprieta)
                {
                    Proprieta ProprietaCorrente = (Proprieta)Caselle[Giocatori[Turno].Posizione];
                    if (ProprietaCorrente.Proprietario == null)
                        Button_CompraProprieta.IsEnabled = true;
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
            Caselle[11] = new Proprieta(Brushes.Purple, "Via Accademia", 14000, false);
            Caselle[12] = new Proprieta(Brushes.Yellow, "Società Elettrica", 15000, true);
            Caselle[13] = new Proprieta(Brushes.Purple, "Corso Ateneo", 14000, false);
            Caselle[14] = new Proprieta(Brushes.Purple, "Piazza Università", 16000, false);
            Caselle[15] = new Proprieta(Brushes.Black, "Stazione Ovest", 20000, true);
            Caselle[16] = new Proprieta(Brushes.Orange, "Via Verdi", 18000, false);
            Caselle[17] = new Speciali("Probabilità", Tipo_Speciali.Probabilita, 0);
            Caselle[18] = new Proprieta(Brushes.Orange, "Corso Raffaello", 18000, false);
            Caselle[19] = new Proprieta(Brushes.Orange, "Piazza Dante", 20000, false);
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
        void CreaCarte()
        {
            Probabilita = new Carta[16];
            Probabilita[0] = new Carta(Tipo_Probabilita.SpostaCasella, 0, "Vai avanti fino al Via!");
            Probabilita[1] = new Carta(Tipo_Probabilita.SpostaCasella, 30, "Vai in prigione direttamente senza passare dal Via!");
            Probabilita[2] = new Carta(Tipo_Probabilita.Tassa, -10000, "Avete perso una causa: pagate 10'000 lire");
            Probabilita[3] = new Carta(Tipo_Probabilita.Tassa, 5000, "Avete venduto delle azioni: ricavate 5'000 lire");
            Probabilita[4] = new Carta(Tipo_Probabilita.Tassa, 1000, "Avete vinto il secondo premio in un concorso di bellezza: ritirate 1'000 lire");
            Probabilita[5] = new Carta(Tipo_Probabilita.Tassa, 10000, "Avete vinto un premio di consolazione alla Lotteria di Merano: ritirate 10'000 lire");
            Probabilita[6] = new Carta(Tipo_Probabilita.TassaGlobale, 1000, "E' il vostro compleanno: ogni giocatore vi regala 1.000 lire");
            Probabilita[7] = new Carta(Tipo_Probabilita.Tassa, 2500, "E' maturata la cedola delle vostre azioni: ritirate 2'500 lire");
            Probabilita[8] = new Carta(Tipo_Probabilita.Tassa, 10000, "Ereditate da un lontano parente 10'000 lire");
            Probabilita[9] = new Carta(Tipo_Probabilita.Tassa, -5000, "Pagate il conto del Dottore: 5'000 lire");
            Probabilita[10] = new Carta(Tipo_Probabilita.Tassa, -1000, "Pagate multa di 1'000 lire");
            Probabilita[11] = new Carta(Tipo_Probabilita.Tassa, 2000, "Rimborso tassa sul reddito: ritirate 2'000 lire dalla Banca");
            Probabilita[12] = new Carta(Tipo_Probabilita.SpostaCasella, 1, "Ritornate al Vicolo Corto");
            Probabilita[13] = new Carta(Tipo_Probabilita.Tassa, -5000, "Scade il Vostro premio di assicurazione: pagate 5'000 lire");
            Probabilita[14] = new Carta(Tipo_Probabilita.Tassa, 20000, "Siete creditore verso Banca di 20'000 lire: ritiratele");
            Probabilita[15] = new Carta(Tipo_Probabilita.UscitaPrigione, "Uscite gratis di prigione, se ci siete: potete conservare questo cartoncino sino al momento di servirvene (non si sa mai!) oppure venderlo");
            // ---
            Imprevisti = new Carta[16];
            Imprevisti[0] = new Carta(Tipo_Probabilita.SpostaCasella, 25, "Andate alla Stazione NORD");
            Imprevisti[1] = new Carta(Tipo_Probabilita.SpostaCasella, 0, "Andate avanti sino al Via!");
            Imprevisti[2] = new Carta(Tipo_Probabilita.SpostaCasella, 39, "Andate fino al Parco della Vittoria");
            Imprevisti[3] = new Carta(Tipo_Probabilita.SpostaCasella, 30, "Andate in prigione senza passare dal Via!");
            Imprevisti[4] = new Carta(Tipo_Probabilita.SpostaCasella, 11, "Andate fino a Via Accademia");
            Imprevisti[5] = new Carta(Tipo_Probabilita.SpostaCasella, 24, "Andate fino al Largo Colombo");
            Imprevisti[6] = new Carta(Tipo_Probabilita.TassaCaseAlberghi, 2500, 10000, "Avete tutti i vostri stabili da riparare: pagate 2.500 lire per ogni casa e 10.000 lire per ogni albergo");
            Imprevisti[7] = new Carta(Tipo_Probabilita.Tassa, 10000, "Avete vinto un terno al lotto: ritirate 10.000 lire");
            Imprevisti[8] = new Carta(Tipo_Probabilita.TassaCaseAlberghi, 4000, 10000, "Dovete pagare per contributo di miglioria stradale, 4.000 lire per ogni casa, e 10.000 lire per ogni albergo che possedete");
            Imprevisti[9] = new Carta(Tipo_Probabilita.SpostaNumero, 3, "Fate tre passi indietro (con tanti auguri)");
            Imprevisti[10] = new Carta(Tipo_Probabilita.Tassa, 5000, "La Banca Vi paga gli interessi del vostro Conto Corrente: ritirate 5.000 lire");
            Imprevisti[11] = new Carta(Tipo_Probabilita.Tassa, -15000, "Matrimonio in famiglia: spese impreviste 15.000 lire");
            Imprevisti[12] = new Carta(Tipo_Probabilita.Tassa, 15000, "Maturano le cedole delle vostre cartelle di rendita, ritirate 15.000 lire");
            Imprevisti[13] = new Carta(Tipo_Probabilita.Tassa, -1500, "Multa di 1.500 lire per aver guidato senza patente");
            Imprevisti[14] = new Carta(Tipo_Probabilita.UscitaPrigione, "Uscite gratis di prigione, se ci siete: potete conservare questo cartoncino sino al momento di servirvene (non si sa mai!) oppure venderlo");
            Imprevisti[15] = new Carta(Tipo_Probabilita.Tassa, 2000, "Versate 2000 lire per beneficenza");
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
                    Giocatori[Turno].SetPosizione(Risultato, Turno, false);

                    if (Caselle[Giocatori[Turno].Posizione] is Speciali)
                    {
                        Speciali Casella = (Speciali)Caselle[Giocatori[Turno].Posizione];
                        switch (Casella.Tipo)
                        {
                            case Tipo_Speciali.Tassa:
                                Giocatori[Turno].Soldi -= Casella.Tassa_Costo;
                                break;

                            case Tipo_Speciali.Prigione:
                                if (Giocatori[Turno].BigliettoPrigione > 0)
                                {
                                    MessageBox.Show("Usi il biglietto per saltare la prigione");
                                    Giocatori[Turno].BigliettoPrigione--;
                                    Giocatori[Turno].SetPosizione(10, Turno, true);
                                }
                                else
                                {
                                    MessageBox.Show("Vai in Prigione!");
                                    Giocatori[Turno].InPrigione = 3;
                                    Giocatori[Turno].SetPosizione(10, Turno, true);
                                }
                                break;

                            case Tipo_Speciali.Probabilita:
                                switch(Probabilita[0].Tipo)
                                {
                                    case Tipo_Probabilita.SpostaCasella:
                                        Giocatori[Turno].SetPosizione(Probabilita[0].Spostamento, Turno, true);
                                        Carta.Scorri(ref Probabilita);
                                        break;

                                    case Tipo_Probabilita.SpostaNumero:
                                        Giocatori[Turno].SetPosizione(Probabilita[0].Spostamento, Turno, false);
                                        Carta.Scorri(ref Probabilita);
                                        break;

                                    case Tipo_Probabilita.Tassa:
                                        Giocatori[Turno].Soldi += Probabilita[0].Pagamento;
                                        Carta.Scorri(ref Probabilita);
                                        break;

                                    case Tipo_Probabilita.TassaGlobale:
                                        foreach(Giocatore G in Giocatori)
                                            if (G != Giocatori[Turno])
                                                G.Soldi -= Probabilita[0].Pagamento;
                                            else
                                                G.Soldi += Probabilita[0].Pagamento * (Giocatori.Length - 1);
                                        Carta.Scorri(ref Probabilita);
                                        break;

                                    case Tipo_Probabilita.UscitaPrigione:
                                        Giocatori[Turno].BigliettoPrigione++;
                                        Carta.RimuoviPrigione(ref Probabilita);
                                        Carta.Scorri(ref Probabilita);
                                        break;
                                }
                                break;
                        }
                    }
                    if (Caselle[Giocatori[Turno].Posizione] is Proprieta)
                    {
                        Proprieta ProprietaCorrente = (Proprieta)Caselle[Giocatori[Turno].Posizione];
                        ProprietaCorrente.Rendita(Giocatori[Turno]);
                    }
                }

                Passato = false;
            }

            else
            {
                bool ForzaVendita = false;
                if (Giocatori[Turno].Soldi < 0)
                {
                    if (Giocatori[Turno].Proprieta.Count > 0)
                    {
                        MessageBox.Show("Non puoi passare perchè andresti in bancarotta, vendi qualcosa!");
                        ForzaVendita = true;
                    }
                    else
                    {
                        MessageBox.Show("Il Giocatore " + (Turno + 1) + " è andato in bancarotta!");
                        Giocatori[Turno].InGioco = false;
                    }
                }

                if (!ForzaVendita)
                {
                    do
                    {
                        Turno++;
                        if (Turno == Giocatori.Length)
                            Turno = 0;
                    }
                    while (!Giocatori[Turno].InGioco);

                    Passato = true;
                    MessageBox.Show("E' il turno del Giocatore " + (Turno + 1));
                }
            }
            
            Menu_Azioni.Visibility = Visibility.Collapsed;
            AggiornaInterfaccia();
        }

        private void CompraProprieta(object sender, RoutedEventArgs e)
        {
            Proprieta T = (Proprieta)Caselle[Giocatori[Turno].Posizione];
            MessageBoxResult Risposta = MessageBox.Show("Comprare '" + T.Nome + "' per L." + T.Costo + "?", "Conferma", MessageBoxButton.OKCancel);

            if (Risposta.HasFlag(MessageBoxResult.OK))
            {
                if (!Giocatori[Turno].Compra(T))
                    MessageBox.Show("Non hai abbastanza soldi");
            }

            Menu_Azioni.Visibility = Visibility.Collapsed;
            AggiornaInterfaccia();
        }

        private void ApriMenu(object sender, RoutedEventArgs e)
        {
            AggiornaMenu();
        }

        void AggiornaMenu()
        {
            if (Menu_Azioni.Visibility == Visibility.Collapsed)
                Menu_Azioni.Visibility = Visibility.Visible;

            else if (Menu_Azioni.Visibility == Visibility.Visible)
                Menu_Azioni.Visibility = Visibility.Collapsed;
        }

        private void IpotecaProprieta(object sender, RoutedEventArgs e)
        {
            WindowIpoteca I = new WindowIpoteca(Giocatori[Turno], this);
            I.Show();
            this.IsEnabled = false;
            //this.Hide();
            I.Closed += ChiusuraIpoteca;
            
            Menu_Azioni.Visibility = Visibility.Collapsed;
            AggiornaInterfaccia();
        }

        private void ChiusuraIpoteca(object sender, EventArgs e)
        {
            this.IsEnabled = true;
            AggiornaInterfaccia();
        }

        private void Trucchi(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.B)
                Giocatori[Turno].Soldi = -10;
            if(e.Key == Key.P)
            {
                Carta[] T = new Carta[Probabilita.Length - 1];
                Giocatori[Turno].BigliettoPrigione++;
                bool Avanti = false; ;
                for(int i = 0; i < Probabilita.Length; i++)
                {
                    if (Avanti)
                        T[i - 1] = Probabilita[i];
                    else
                        T[i] = Probabilita[i];

                    if(Probabilita[i].Tipo == Tipo_Probabilita.UscitaPrigione)
                        Avanti = true;
                }

                Probabilita = T;
            }

            AggiornaInterfaccia();
        }
    }
}