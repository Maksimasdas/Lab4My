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

namespace Airport
{
    /// <summary>
    /// Логика взаимодействия для UserControl_Exp.xaml
    /// </summary>
    public partial class UserControl_Exp : UserControl
    {
        Exp exp;
        public UserControl_Exp()
        {
            InitializeComponent();
            exp = new Exp(0, 0);
            this.DataContext = exp;
        }

        public void SetExpX(double x)
        {
            exp.X = x;
            Canvas.SetLeft(this, x);
        }

        public void SetExpY(double y)
        {
            exp.Y = y;
            Canvas.SetTop(this, y);
        }

        public double GetExpX()
        {
            return exp?.X ?? 0; //проверка на случай нула
        }

        public double GetExpY()
        {
            return exp?.Y ?? 0;
        }
    }
}
