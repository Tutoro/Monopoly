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
\author    Poli Alessandro
\author    Verazza Claudio
\author    Zacconi Andrea
\version   1.5a
\date      02/05/2017
*/

namespace Monopoly
{
    //! \class MainWindow
    //! \brief Classe della finestra principale
    public partial class MainWindow : Window
    {
        public int Turno; //! \var Turno \brief Variabile intera che contiene di quale giocatore è il turno
        int Turni; //! \var Turni \brief Intero che conta il numero di turni massimi (0 = infiniti, > 0 finiti)
        bool Passato; //! \var Passato \brief Variabile booleana che controlla se un giocatore ha tirato i dadi

        Giocatore[] Giocatori; //! \var Giocatori \brief Vettore che contiene i giocatori
        public Casella[] Caselle; //! \var Caselle \brief Vettore che contiene tutte le caselle del tabellone
        Random R; //! \var R \brief Variabile per estrarre numeri Random
        TextBlock[] TextBlockSoldi;
        Carta[] Probabilita, Imprevisti;

        Casella CasellaCorrente { get { return Caselle[Giocatori[Turno].Posizione]; } }
        Proprieta ProprietaCorrente { get { if (CasellaCorrente is Proprieta) return (Proprieta)CasellaCorrente; else return null; } }
        Speciali SpecialeCorrente { get { if (CasellaCorrente is Speciali) return (Speciali)CasellaCorrente; else return null; } }

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
                if(i < Giocatori.Length)
                    Giocatori[i].SetPosizione(0, i, false);
                else
                    TextBlockSoldi[i].Visibility = Visibility.Collapsed;
            }
            CreaTabellone();
            CreaCarte();
            Passato = true;
            Turno = 0;
            this.Turni = Turni;
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
                    R.Text = Environment.NewLine + " Proprietà Giocatore " + (i + 1) + ":";
                    BrushConverter B = new BrushConverter();
                    R.Background = null;
                    StackPanel_ProprietaUtente.Children.Add(R);

                    foreach (Proprieta P in Giocatori[i].Proprieta)
                    {
                        StackPanel_ProprietaUtente.Children.Add(P.GetInterfaccia(ref Altezza_Prossima));
                    }

                    for(int c = 0; c < Giocatori[i].BigliettoPrigione; c++)
                    {
                        R = new TextBlock();
                        R.HorizontalAlignment = HorizontalAlignment.Center;
                        R.TextAlignment = TextAlignment.Center;
                        R.Width = 268;
                        R.Height = 22;
                        R.Margin = new Thickness(0, Altezza_Prossima, 0, 0);
                        Altezza_Prossima += 2;
                        R.Background = Brushes.DarkGray;
                        R.Foreground = Brushes.White;
                        R.Text = "Uscita gratis di Prigione!";
                        StackPanel_ProprietaUtente.Children.Add(R);
                    }
                }
            }
            if (Passato)
            {
                Button_CompraStrutture.IsEnabled = false;
                Button_Scambia.IsEnabled = true;
                Button_CompraProprieta.IsEnabled = false;
                Button_VendiProprieta.IsEnabled = true;
                if (Giocatori[Turno].Proprieta.Count == 0)
                    Button_VendiProprieta.IsEnabled = false;

                Button_Passa.Content = "Avanza";
            }
            else
            {
                Button_CompraStrutture.IsEnabled = true;
                if (CasellaCorrente is Proprieta && ProprietaCorrente.Proprietario != Giocatori[Turno])
                    Button_CompraStrutture.IsEnabled = false;

                Button_Scambia.IsEnabled = false;
                Button_CompraProprieta.IsEnabled = false;
                if (CasellaCorrente is Proprieta && Giocatori[Turno].Soldi >= ProprietaCorrente.Costo && ProprietaCorrente.Proprietario == null)
                    Button_CompraProprieta.IsEnabled = true;

                Button_VendiProprieta.IsEnabled = true;
                if (Giocatori[Turno].Proprieta.Count == 0)
                    Button_VendiProprieta.IsEnabled = false;

                Button_Passa.Content = "Passa";
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
            Probabilita[0] = new Carta(Tipo_Carta.SpostaCasella, 0, "Vai avanti fino al Via!");
            Probabilita[1] = new Carta(Tipo_Carta.SpostaCasella, 30, "Vai in prigione direttamente senza passare dal Via!");
            Probabilita[2] = new Carta(Tipo_Carta.Tassa, -10000, "Avete perso una causa: pagate 10'000 lire");
            Probabilita[3] = new Carta(Tipo_Carta.Tassa, 5000, "Avete venduto delle azioni: ricavate 5'000 lire");
            Probabilita[4] = new Carta(Tipo_Carta.Tassa, 1000, "Avete vinto il secondo premio in un concorso di bellezza: ritirate 1'000 lire");
            Probabilita[5] = new Carta(Tipo_Carta.Tassa, 10000, "Avete vinto un premio di consolazione alla Lotteria di Merano: ritirate 10'000 lire");
            Probabilita[6] = new Carta(Tipo_Carta.TassaGlobale, 1000, "E' il vostro compleanno: ogni giocatore vi regala 1.000 lire");
            Probabilita[7] = new Carta(Tipo_Carta.Tassa, 2500, "E' maturata la cedola delle vostre azioni: ritirate 2'500 lire");
            Probabilita[8] = new Carta(Tipo_Carta.Tassa, 10000, "Ereditate da un lontano parente 10'000 lire");
            Probabilita[9] = new Carta(Tipo_Carta.Tassa, -5000, "Pagate il conto del Dottore: 5'000 lire");
            Probabilita[10] = new Carta(Tipo_Carta.Tassa, -1000, "Pagate multa di 1'000 lire");
            Probabilita[11] = new Carta(Tipo_Carta.Tassa, 2000, "Rimborso tassa sul reddito: ritirate 2'000 lire dalla Banca");
            Probabilita[12] = new Carta(Tipo_Carta.SpostaCasella, 1, "Ritornate al Vicolo Corto");
            Probabilita[13] = new Carta(Tipo_Carta.Tassa, -5000, "Scade il Vostro premio di assicurazione: pagate 5'000 lire");
            Probabilita[14] = new Carta(Tipo_Carta.Tassa, 20000, "Siete creditore verso Banca di 20'000 lire: ritiratele");
            Probabilita[15] = new Carta(Tipo_Carta.UscitaPrigione, "Uscite gratis di prigione, se ci siete: potete conservare questo cartoncino sino al momento di servirvene (non si sa mai!) oppure venderlo");
            Carta.Mischia(ref Probabilita);

            Imprevisti = new Carta[16];
            Imprevisti[0] = new Carta(Tipo_Carta.SpostaCasella, 25, "Andate alla Stazione NORD");
            Imprevisti[1] = new Carta(Tipo_Carta.SpostaCasella, 0, "Andate avanti sino al Via!");
            Imprevisti[2] = new Carta(Tipo_Carta.SpostaCasella, 39, "Andate fino al Parco della Vittoria");
            Imprevisti[3] = new Carta(Tipo_Carta.SpostaCasella, 30, "Andate in prigione senza passare dal Via!");
            Imprevisti[4] = new Carta(Tipo_Carta.SpostaCasella, 11, "Andate fino a Via Accademia");
            Imprevisti[5] = new Carta(Tipo_Carta.SpostaCasella, 24, "Andate fino al Largo Colombo");
            Imprevisti[6] = new Carta(Tipo_Carta.TassaCaseAlberghi, 2500, 10000, "Avete tutti i vostri stabili da riparare: pagate 2.500 lire per ogni casa e 10.000 lire per ogni albergo");
            Imprevisti[7] = new Carta(Tipo_Carta.Tassa, 10000, "Avete vinto un terno al lotto: ritirate 10.000 lire");
            Imprevisti[8] = new Carta(Tipo_Carta.TassaCaseAlberghi, 4000, 10000, "Dovete pagare per contributo di miglioria stradale, 4.000 lire per ogni casa, e 10.000 lire per ogni albergo che possedete");
            Imprevisti[9] = new Carta(Tipo_Carta.SpostaNumero, 3, "Fate tre passi indietro (con tanti auguri)");
            Imprevisti[10] = new Carta(Tipo_Carta.Tassa, 5000, "La Banca Vi paga gli interessi del vostro Conto Corrente: ritirate 5.000 lire");
            Imprevisti[11] = new Carta(Tipo_Carta.Tassa, -15000, "Matrimonio in famiglia: spese impreviste 15.000 lire");
            Imprevisti[12] = new Carta(Tipo_Carta.Tassa, 15000, "Maturano le cedole delle vostre cartelle di rendita, ritirate 15.000 lire");
            Imprevisti[13] = new Carta(Tipo_Carta.Tassa, -1500, "Multa di 1.500 lire per aver guidato senza patente");
            Imprevisti[14] = new Carta(Tipo_Carta.UscitaPrigione, "Uscite gratis di prigione, se ci siete: potete conservare questo cartoncino sino al momento di servirvene (non si sa mai!) oppure venderlo");
            Imprevisti[15] = new Carta(Tipo_Carta.Tassa, 2000, "Versate 2000 lire per beneficenza");
            Carta.Mischia(ref Imprevisti);
        }

        void ControllaCella()
        {
            if (CasellaCorrente is Speciali)
            {
                switch (SpecialeCorrente.Tipo)
                {
                    case Tipo_Speciali.Tassa:
                        Giocatori[Turno].Soldi -= SpecialeCorrente.Tassa_Costo;
                        break;

                    case Tipo_Speciali.Prigione:
                        if (Giocatori[Turno].BigliettoPrigione > 0)
                        {
                            MessageBox.Show("Usi il biglietto per saltare la prigione");
                            Giocatori[Turno].BigliettoPrigione--;
                            Giocatori[Turno].SetPosizione(10, Turno, true);
                            ControllaCella();
                        }
                        else
                        {
                            MessageBox.Show("Vai in Prigione!");
                            Giocatori[Turno].InPrigione = 3;
                            Giocatori[Turno].SetPosizione(10, Turno, true);
                            ControllaCella();
                        }
                        break;

                    case Tipo_Speciali.Probabilita:
                        {
                            MessageBox.Show(Probabilita[0].Messaggio);
                            Probabilita[0].Azione(Turno, Giocatori, ref Probabilita);
                            ControllaCella();
                        }
                        break;

                    case Tipo_Speciali.Imprevisti:
                        MessageBox.Show(Imprevisti[0].Messaggio);
                        Imprevisti[0].Azione(Turno, Giocatori, ref Imprevisti);
                        ControllaCella();
                        break;
                }
            }

            if (CasellaCorrente is Proprieta)
                ProprietaCorrente.Rendita(Giocatori[Turno]);
        }

        private void PassaTurno(object sender, RoutedEventArgs e)
        {
            if (Passato)
            {
                int Risultato = R.Next(1, 7) + R.Next(1, 7);
                MessageBox.Show("Hai estratto " + Risultato.ToString());

                if (Giocatori[Turno].InPrigione > 0 && Risultato < 12)
                {
                    if (Giocatori[Turno].InPrigione == 1)
                        Giocatori[Turno].Soldi -= 5000;

                    Giocatori[Turno].InPrigione--;
                }
                else
                {
                    Giocatori[Turno].SetPosizione(Risultato, Turno, false);
                    ControllaCella();
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
                    int Giocanti = 0;
                    foreach(Giocatore G in Giocatori)
                    {
                        if (G.InGioco)
                            Giocanti++;
                    }

                    if(Turni == 1 || Giocanti == 1)
                    {
                        int G = 0;
                        for(int i = 1; i < Giocatori.Length; i++)
                        {
                            if (Giocatori[G].GetValore() < Giocatori[i].GetValore())
                                G = i;
                        }
                        MessageBox.Show("Partita terminata. Il vincitore è il Giocatore " + G + " con un valore totale di " + Giocatori[G].GetValore() + "L");
                        MessageBoxResult Risposta = MessageBox.Show("Vuoi Rigiocare?", "Avviso", MessageBoxButton.YesNo);

                        if (Risposta == MessageBoxResult.Yes)
                            new WindowInizioPartita().Show();

                        this.Close();
                    }
                    else if (Turni > 0)
                    {
                        Turni--;
                        MessageBox.Show("E' il turno del Giocatore " + (Turno + 1) + ", Turni rimanenti: " + Turni);
                    }
                    else
                        MessageBox.Show("E' il turno del Giocatore " + (Turno + 1));
                }
            }
            
            Menu_Azioni.Visibility = Visibility.Collapsed;
            AggiornaInterfaccia();
        }

        private void CompraProprieta(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Risposta = MessageBox.Show("Comprare '" + ProprietaCorrente.Nome + "' per L." + ProprietaCorrente.Costo + "?", "Conferma", MessageBoxButton.OKCancel);

            if (Risposta.HasFlag(MessageBoxResult.OK))
            {
                if (!Giocatori[Turno].Compra(ProprietaCorrente))
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
            I.Closed += ChiusuraFinestra;
            Menu_Azioni.Visibility = Visibility.Collapsed;
            AggiornaInterfaccia();
        }

        private void CompraStrutture(object sender, RoutedEventArgs e)
        {
            WindowStrutture W = new WindowStrutture(Giocatori[Turno], this);
            W.Visibility = Visibility.Visible;
            this.IsEnabled = false;
            W.Closed += ChiusuraFinestra;
        }
        public void AggiornaDaStrutture()
        {
            AggiornaInterfaccia();
        }

        private void Scambio(object sender, RoutedEventArgs e)
        {
            WindowScambio W = new WindowScambio(Giocatori, Giocatori[Turno]);
            W.Closed += ChiusuraFinestra;
            this.IsEnabled = false;
            W.Show();
        }

        private void ChiusuraFinestra(object sender, EventArgs e)
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

                    if(Probabilita[i].Tipo == Tipo_Carta.UscitaPrigione)
                        Avanti = true;
                }

                Probabilita = T;
            }
            if (e.Key == Key.U)
                Giocatori[Turno].SetPosizione(1, Turno, false);
            if (e.Key == Key.I)
                Passato = false;

            AggiornaInterfaccia();
        }
    }
}