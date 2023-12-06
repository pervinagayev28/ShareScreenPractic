
using Segment.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
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
using System.Windows.Threading;

namespace TakeScreen
{

    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private DispatcherTimer ClearFolder;

        public string imagePath
        {
            get { return (string)GetValue(imagePathProperty); }
            set { SetValue(imagePathProperty, value); }
        }

        public string unicpng { get; private set; }

        public static readonly DependencyProperty imagePathProperty =
            DependencyProperty.Register("imagePath", typeof(string), typeof(MainWindow));


        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.000000000000000000000000000000000000000000000000000000001);
            timer.Tick += Timer_Tick;
            timer.Start();
            //ClearFolder = new DispatcherTimer();
            //ClearFolder.Interval = TimeSpan.FromSeconds(10);
            //ClearFolder.Tick += ClearFolderMethodAsync;
            //ClearFolder.Start();

            DataContext = this;
        }

        private async void ClearFolderMethodAsync(object? sender, EventArgs e)
        {
            await Task.Delay(100);
            if (Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ImagesNew" != null && Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ImagesNew" != imagePath.ToString())
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ImagesNew");
            }

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            captureScreenAsync();
        }

        private void btnTakeScreenshot_Click(object sender, RoutedEventArgs e)
        {
            captureScreenAsync();
        }
        public async Task captureScreenAsync()
        {
            using Bitmap sc = new(1920, 1080);
            using Graphics gr = Graphics.FromImage(sc);
            gr.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(1920, 1080));
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ImagesNew"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ImagesNew");
            unicpng = Guid.NewGuid().ToString();
            sc.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ImagesNew" + $"\\took{unicpng}.png");
            imagePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ImagesNew" + $"\\took{unicpng}.png";

        }

    }
}
