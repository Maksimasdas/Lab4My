using Airport;
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
        Airport.UserControl_Target target;
        Airport.UserControl_Exp explosion;

        //таймер
        private Stopwatch stopwatch = new Stopwatch();
        private double lastUpdateTime = 0;
        private double lastSpeedUpdateTime = 0;
        private Stopwatch explosionTimer = new Stopwatch(); //таймер для взрыва

        private const double UPDATE_INTERVAL = 1000;
        private const double SPEED_UPDATE_INTERVAL = 1000;
        private const double EXPLOSION_DURATION = 800;
        private bool isRenderingSubscribed = false;

        float dx = 2000f; //изменение расстояния
        bool flStart = false; //флаг

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            StartGame();
            //start();
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
            target = new Airport.UserControl_Target();

            plane.SetXplane(0);      // ← Устанавливаем в модель
            plane.SetYplane(0);      //← ДО добавления на Canvas
            double x = canvas.ActualWidth / 2 - target.ActualWidth / 2;
            double y = canvas.ActualHeight - target.ActualHeight - 10;
            target.SetXTarget(x);
            target.SetYTarget(y-54);
            canvas.Children.Add(plane);
            canvas.Children.Add(target);

            canvas.UpdateLayout();

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
            }

            // Плавная анимация КАЖДЫЙ КАДР
            UpdatePlaneVisuals();
            UpdateBomb();
            UpdateExplosion();
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
                if(plane.GetXplane() > canvas.ActualWidth-75)
                {
                    plane.SetXplane(0);
                }
            }
        }

        private void DropBomb()
        {
            if (bomb != null)
            {
                return;
            }

            bomb = new Airport.UserControl_Bomb(plane.GetXplane() + 40, plane.GetYplane() + 40, plane.GetSpeed()/1.7);

            bomb.BombModel.X = plane.GetXplane() + 20;
            bomb.BombModel.Y = plane.GetYplane() + 10;

            canvas.Children.Add(bomb);
        }

        private void UpdateBomb()
        {
            if (bomb == null) return;

            double dt = 1.0 / 60.0;
            bomb.BombModel.Update(dt);

            if (CheckCollision(bomb.BombModel.X, bomb.BombModel.Y))
            {
                ShowExplosion(target.GetXTarget(), target.GetYTarget());

                canvas.Children.Remove(bomb);
                bomb = null;
                return;
            }

            if (bomb.BombModel.Y > canvas.ActualHeight-59)
            {
                canvas.Children.Remove(bomb );
                bomb = null;
            }

        }

        private bool CheckCollision(double bombX, double bombY) //проверка столкновения с целью
        {
            double targetX = target.GetXTarget();
            double targetY = target.GetYTarget();

            double hitboxWidth = 200;   
            double hitboxHeight = 200;

            return bombX >= targetX - hitboxWidth / 2 &&
           bombX <= targetX + hitboxWidth / 2 &&
           bombY >= targetY - hitboxHeight / 2 &&
           bombY <= targetY + hitboxHeight / 2;
        }

        private void ShowExplosion(double x, double y) //показ взрыва
        {
            if (explosion != null)
                canvas.Children.Remove(explosion);

            explosion = new Airport.UserControl_Exp();
            explosion.SetExpX(x - 25);
            explosion.SetExpY(y - 25);
            canvas.Children.Add(explosion);
            explosionTimer.Restart();
        }

        private async void UpdateExplosion() //обновление взрыва async для работы await (подсмотрел в интернете)
        {
            if (explosion != null && explosionTimer.ElapsedMilliseconds > EXPLOSION_DURATION)
            {
                canvas.Children.Remove(explosion);
                explosion = null;
                explosionTimer.Stop();
                canvas.Children.Remove(target);
                stopwatch.Stop();

                await Task.Delay(300);

                MessageBoxResult result = MessageBox.Show(
                "Попадание по цели!!! поздравляю!!!",
                "Успех",
                MessageBoxButton.OK,
                MessageBoxImage.Information
                 );

                if (result == MessageBoxResult.OK)
                {
                    if (isRenderingSubscribed)
                    {
                        CompositionTarget.Rendering -= CompositionTarget_Rendering;
                        isRenderingSubscribed = false;
                    }
                    this.Close();
                }

            }
        }

        private void Buttondropbomb_Click(object sender, RoutedEventArgs e)
        {
            DropBomb();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            start();
            canvas.Children.Remove(bomb);
            bomb = null;
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if(flStart == false)
            {
                flStart = true;
                start();
                (sender as Button)?.Content = "Pause";
            }
            else
            {
                if (stopwatch.IsRunning)
                {
                    stopwatch.Stop();
                    (sender as Button)?.Content = "Start";
                }
                else
                {
                    stopwatch.Start();
                    (sender as Button)?.Content = "Pause";
                }
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            stopwatch.Stop();

            if (isRenderingSubscribed)
            {
                CompositionTarget.Rendering -= CompositionTarget_Rendering;
                isRenderingSubscribed = false;
            }
        }
    }
}