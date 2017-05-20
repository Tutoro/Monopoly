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
\author    Arduini Alberto
\author    Zacconi Andrea
\version   0.3
\date      19/05/2017
*/

namespace Monopoly.Classi
{
    //! \class Casella
    //! \brief Classe che rappresenta una casella del tabellone nel programma
    public abstract class Casella : Image
    {
        public Brush Colore; //! \var Colore \brief Colore della casella
        public string Nome; //! \var Nome \brief Nome della casella

        //! \fn Casella
        //! \brief Crea una casella
        //! \param Col \brief Colore della casella
        //! \param N \brief Nome della casella
        public Casella(Brush Col, string N)
        {
            Colore = Col;
            Nome = N;
        }
    }
}