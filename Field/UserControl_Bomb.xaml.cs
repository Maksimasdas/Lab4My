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
    /// Логика взаимодействия для UserControl_Bomb.xaml
    /// </summary>
    public partial class UserControl_Bomb : UserControl
    {
        public Bomb BombModel { get; }

        public UserControl_Bomb(double x, double y, double vx)
        {
            InitializeComponent();

            BombModel = new Bomb(x, y, vx);
            DataContext = BombModel;

            Binding bindX = new Binding("X");
            this.SetBinding(Canvas.LeftProperty, bindX);

            Binding bindY = new Binding("Y");
            this.SetBinding(Canvas.TopProperty, bindY);
        }
    }
}
