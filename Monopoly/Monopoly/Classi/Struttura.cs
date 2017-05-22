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

/*!
\author    Zacconi Andrea
\version   1
\date      02/05/2017
*/

namespace Monopoly.Classi
{
    public class Struttura : Image
    {
        public bool Tipo; //! \var Tipo \brief Bool che indica se la struttura è una casa (true) o un albergo (false)

        //! \fn Struttura
        //! \brief Crea una struttura di tipo definito
        //! \param T Tipo di strutttura (true = casa, false = albergo)
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