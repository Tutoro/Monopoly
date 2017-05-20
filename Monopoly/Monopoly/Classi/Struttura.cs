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
    public class Struttura : Image
    {
        public bool Tipo; //! \var Tipo \brief Bool che indica se la struttura è una casa (true) o un albergo (false)
        public Struttura(bool T)
        {
            Tipo = T;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Width = 10;
            Height = 10;
        }
    }
}