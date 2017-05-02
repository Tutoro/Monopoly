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

namespace Monopoly.Classi
{

    public enum Tipo_Speciali
    { Imprevisti, Parcheggio, Prigione, Probabilita, Tassa };

    //***************************************************************************//
    //Zakk dovresti mettere un bool per controllare se è una proprietà speciale  //
    //Chiamalo 'Edificabile' o 'Speciale', vedi te                               //
    //***************************************************************************//

    public class Proprieta : Casella
    {
        public int Costo;
        public bool Ipotecato;
        public List<Struttura> s = new List<Struttura>();

        public Proprieta(Brush Col, string N, int C) : base(Col, N)
        {
            Costo = C;
        }
    }

    public class Speciali : Casella
    {
        public Speciali(Brush Col, string N, Tipo_Speciali T) : base(Col, N)
        { }
    }
}