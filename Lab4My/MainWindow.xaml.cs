using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab4My
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas canvas = new Canvas();
        Airport.UserControl_Plane plane;
        Random random = new Random();
        Airport.UserControl_Bomb bomb;

        //таймер
        private Stopwatch stopwatch = new Stopwatch();
        private double lastUpdateTime = 0;
        private double lastSpeedUpdateTime = 0;

        private const double UPDATE_INTERVAL = 1000;
        private const double SPEED_UPDATE_INTERVAL = 1000;
        private bool isRenderingSubscribed = false;

        float dx = 2000f; //изменение расстояния
        bool flStart = false; //флаг, возможно тут не нужен
        float Length = 1100f; //длинна полосы

        

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            StartGame();
            start();
        }
        void StartGame()
        {
            canvas = new Canvas();
            Grid.SetRow(canvas, 0);
            grid.Children.Add(canvas);
        }

        void start()
        {
            canvas.Children.Clear();
            plane = new Airport.UserControl_Plane(random.Next(20, 50));
            plane.SetXplane(0);      // ← Устанавливаем в модель
            plane.SetYplane(0);      //← ДО добавления на Canvas

            canvas.Children.Add(plane);

            stopwatch.Restart();
            lastUpdateTime = 0;
            lastSpeedUpdateTime = 0;


            // Подписываемся на событие рендеринга если еще не подписаны
            if (!isRenderingSubscribed)
            {
                CompositionTarget.Rendering += CompositionTarget_Rendering;
                isRenderingSubscribed = true;
            }

        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (!stopwatch.IsRunning)
                return;

            double currentTime = stopwatch.ElapsedMilliseconds;

            // Обновляем скорость (каждые 1000 ms)
            if (currentTime - lastSpeedUpdateTime >= SPEED_UPDATE_INTERVAL)
            {
                lastSpeedUpdateTime = currentTime;
                UpdateSpeed();
            }

            // Проверяем финиши (каждые 1000 ms)
            if (currentTime - lastUpdateTime >= UPDATE_INTERVAL) 
            {
                lastUpdateTime = currentTime;
                CheckFinish(); //лишнее, потом удалю при наведении красоты
            }

            // Плавная анимация КАЖДЫЙ КАДР
            UpdatePlaneVisuals();
            UpdateBomb();
        }

        private void UpdatePlaneVisuals()
        {
            if(plane != null)
            {
                double speed = plane.GetSpeed();
                if(speed > 0)
                {
                    // Умножаем на 1000 чтобы движение было видно перепробовал кучу вариантов, при меньшем значении  очень очень медленное движение
                    double pixelsPerFrame = (speed / dx * 1000.00) / 60.0;
                    plane.SetXplane(plane.GetXplane() + pixelsPerFrame);
                }
                if(plane.GetXplane() > canvas.ActualWidth)
                {
                    plane.SetXplane(0);
                }
            }
        }

        private void UpdateSpeed()
        {
            if(plane != null)
            {
                plane.UpdateSpeed(random.Next(30, 80));
            }
        }

        private void DropBomb()
        {
            if (bomb != null)
            {
                return;
            }

            bomb = new Airport.UserControl_Bomb(plane.GetXplane() + 40, plane.GetYplane() + 40, plane.GetSpeed() / 60.0);

            bomb.BombModel.X = plane.GetXplane() + 40;
            bomb.BombModel.Y = plane.GetYplane() + 40;

            canvas.Children.Add(bomb);
        }

        private void UpdateBomb()
        {
            if (bomb == null) return;

            double dt = 1.0 / 60.0;
            bomb.BombModel.Update(dt);

            if (bomb.BombModel.Y > canvas.ActualHeight - 40)
            {
                canvas.Children.Remove(bomb );
                bomb = null;
            }

        }

        private void CheckFinish()
        {
            
        }

        private void Buttondropbomb_Click(object sender, RoutedEventArgs e)
        {
            DropBomb();
            //MessageBox.Show("Бомба создана!");
        }
    }
}