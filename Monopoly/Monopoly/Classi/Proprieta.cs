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

    enum Tipo_Speciali
    { Imprevisti, Parcheggio, Prigione, Probabilita, Tassa };

    class Proprieta : Casella
    {
        public int Costo;
        public bool Ipotecato;
        public List<Struttura> s = new List<Struttura>();
        public Proprieta(string N, int C, Brush Col) : base(Col, N)
        {
            Costo = C;
        }
    }

    class Speciali : Casella
    {

    }

    class Proprieta_Speciali : Casella
    {
        public int Costo;
        public bool Ipotecato;
        public Proprieta_Speciali(string N, int C, Brush Col) : base(Col, N)
        {
            Costo = C;
        }
    }
}