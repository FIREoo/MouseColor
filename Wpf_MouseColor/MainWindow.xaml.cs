using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Drawing;
using Point = System.Drawing.Point;
using Color = System.Drawing.Color;
using Size = System.Drawing.Size;
using Rectangle = System.Drawing.Rectangle;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Reflection;

[assembly: AssemblyVersion("1.0")]


namespace Wpf_MouseColor
{
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        ObservableCollection<ListViewData> ListViewDataCollection = new ObservableCollection<ListViewData>();

        const int ENUM_CURRENT_SETTINGS = -1;

        List<Rectangle> screenBounds = new List<Rectangle>();//Virtual Resolution
        List<Rectangle> screenRealRect = new List<Rectangle>();//Real Resolution
        List<double> screenScale = new List<double>();
        public MainWindow()
        {
            InitializeComponent();

            lv_color.ItemsSource = ListViewDataCollection;

            //multi screen
            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                DEVMODE dm = new DEVMODE();
                dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                EnumDisplaySettings(screen.DeviceName, ENUM_CURRENT_SETTINGS, ref dm);

                int compareIndex = 0;
                for (; compareIndex < screenBounds.Count(); compareIndex++)
                    if (screen.Bounds.X < screenBounds[compareIndex].X)
                        break;

                int realX = 0;
                for (int i = 0; i < screenBounds.Count; i++)
                {
                    realX += screenRealRect[i].Width;
                }
                screenRealRect.Insert(compareIndex, new Rectangle(realX, screen.Bounds.Y, dm.dmPelsWidth, dm.dmPelsHeight));
                screenBounds.Insert(compareIndex, screen.Bounds);
                screenScale.Insert(compareIndex, (double)dm.dmPelsWidth / screen.Bounds.Width);
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Height = 200;

            DispatcherTimer _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += timerTick;
            _timer.Start();
        }
        void timerTick(object sender, EventArgs e)
        {

            Point P = GetMousePosition();

            int inScreen = 0;
            for (int i = 0; i < screenBounds.Count(); i++)
                if (P.X > screenBounds[i].X)
                    inScreen = i;

            P.X = screenBounds[inScreen].X + (int)((P.X - screenBounds[inScreen].X) * screenScale[inScreen]);
            P.Y = screenBounds[inScreen].Y + (int)((P.Y - screenBounds[inScreen].Y) * screenScale[inScreen]);


            Color color = GetPixelColor(P);
            tb_mouse_x.Text = "X = " + P.X.ToString();
            tb_mouse_y.Text = "Y = " + P.Y.ToString();
            tb_A.Text = " A = " + color.A;
            tb_R.Text = " R = " + color.R;
            tb_G.Text = " G = " + color.G;
            tb_B.Text = " B = " + color.B;

            byte[] HSV = bgr2hsv(color.B, color.G, color.R);
            tb_H.Text = " H = " + HSV[0];
            tb_S.Text = " S = " + HSV[1];
            tb_V.Text = " V = " + HSV[2];

            tb_colorName.Text = "#" + color.Name.ToUpper();

            rect_color.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
        }


        Color GetPixelColor(Point position)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(position, new Point(0, 0), new Size(1, 1));
                }
                return bitmap.GetPixel(0, 0);
            }
        }
        public static byte[] bgr2hsv(byte b, byte g, byte r)
        {
            double B = b;
            double G = g;
            double R = r;

            double Max = Math.Max(Math.Max(B, G), R);
            double Min = Math.Min(Math.Min(B, G), R);
            double V = Max;
            double S = 0;
            if (Max == 0)
                S = 0;
            else
                S = 1 - (Min / Max);
            double H = 0;
            if (Max == Min)
                H = 0;
            else if (Max == R && G >= B)
                H = 60 * ((G - B) / (Max - Min)) + 0;
            else if (Max == R && G < B)
                H = 60 * ((G - B) / (Max - Min)) + 360;
            else if (Max == G)
                H = 60 * ((B - R) / (Max - Min)) + 120;
            else if (Max == B)
                H = 60 * ((R - G) / (Max - Min)) + 240;

            H /= 2;
            S *= 255;
            byte[] rtn = new byte[3];
            rtn[0] = (byte)H;
            rtn[1] = (byte)S;
            rtn[2] = (byte)V;

            return rtn;
        }

        private void Btn_followMouse_Click(object sender, RoutedEventArgs e)
        {
            Point P = GetMousePosition();
            Color color = GetPixelColor(P);

            ListViewDataCollection.Add(new ListViewData(P, color));

            //Window_follower wf = new Window_follower();


            //wf.Show();

            //DispatcherTimer _timer = new DispatcherTimer();
            //_timer.Interval = TimeSpan.FromMilliseconds(500);
            //_timer.Tick += (object s, EventArgs ea) =>
            //{
            //    Point P = GetMousePosition();
            //    wf.Left = 10 + P.X;
            //    wf.Top = -10 + P.Y - wf.Height;
            //};

            //_timer.Start();
        }

        private void Btn_more_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Content.ToString() == "More")
            {
                button.Content = "Less";
                this.Height = 500;
            }
            else if (button.Content.ToString() == "Less")
            {
                button.Content = "More";
                this.Height = 200;
            }
            else
            {
                button.Content = "More";
            }

        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.LeftAlt)
            {
                Point P = GetMousePosition();
                Color color = GetPixelColor(P);
                ListViewDataCollection.Add(new ListViewData(P, color));
            }
        }
        private void Btn_selectall_Click(object sender, RoutedEventArgs e)
        {
            byte[] Max = new byte[3] { 0, 0, 0 };
            byte[] Min = new byte[3] { 255, 255, 255 };
            foreach (ListViewData lv in lv_color.Items)
            {
                if (Max[0] < byte.Parse(lv.H))
                    Max[0] = byte.Parse(lv.H);
                if (Max[1] < byte.Parse(lv.S))
                    Max[1] = byte.Parse(lv.S);
                if (Max[2] < byte.Parse(lv.V))
                    Max[2] = byte.Parse(lv.V);

                if (Min[0] > byte.Parse(lv.H))
                    Min[0] = byte.Parse(lv.H);
                if (Min[1] > byte.Parse(lv.S))
                    Min[1] = byte.Parse(lv.S);
                if (Min[2] > byte.Parse(lv.V))
                    Min[2] = byte.Parse(lv.V);
            }

            tb_max_h.Text = Max[0].ToString();
            tb_max_s.Text = Max[1].ToString();
            tb_max_v.Text = Max[2].ToString();
            tb_min_h.Text = Min[0].ToString();
            tb_min_s.Text = Min[1].ToString();
            tb_min_v.Text = Min[2].ToString();


        }
        private void Btn_clearList_Click(object sender, RoutedEventArgs e)
        {
            ListViewDataCollection.Clear();
        }

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;   //Angle0 = 0, Angle90 = 1, Angle180 = 2, Angle270 = 3
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        private void Lv_color_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            byte[] Max = new byte[3] { 0, 0, 0 };
            byte[] Min = new byte[3] { 255, 255, 255 };

            var selects = lv_color.SelectedItems;
            foreach (ListViewData lv in selects)
            {
                if (Max[0] < byte.Parse(lv.H))
                    Max[0] = byte.Parse(lv.H);
                if (Max[1] < byte.Parse(lv.S))
                    Max[1] = byte.Parse(lv.S);
                if (Max[2] < byte.Parse(lv.V))
                    Max[2] = byte.Parse(lv.V);

                if (Min[0] > byte.Parse(lv.H))
                    Min[0] = byte.Parse(lv.H);
                if (Min[1] > byte.Parse(lv.S))
                    Min[1] = byte.Parse(lv.S);
                if (Min[2] > byte.Parse(lv.V))
                    Min[2] = byte.Parse(lv.V);
            }

            tb_max_h.Text = Max[0].ToString();
            tb_max_s.Text = Max[1].ToString();
            tb_max_v.Text = Max[2].ToString();
            tb_min_h.Text = Min[0].ToString();
            tb_min_s.Text = Min[1].ToString();
            tb_min_v.Text = Min[2].ToString();

        }


    }

    public class ListViewData : INotifyPropertyChanged
    {
        string _h;

        public ListViewData(Point p, Color color)
        {
            X = p.X.ToString();
            Y = p.Y.ToString();

            A = color.A.ToString();
            R = color.R.ToString();
            G = color.G.ToString();
            B = color.B.ToString();

            byte[] HSV = MainWindow.bgr2hsv(color.B, color.G, color.R);
            H = HSV[0].ToString();
            S = HSV[1].ToString();
            V = HSV[2].ToString();

            BackColor = $"#{color.Name.ToUpper()}";
        }

        public string H //Binding
        {
            set
            {
                _h = value;
                NotifyPropertyChanged("H");//Binding
            }
            get { return _h; }
        }

        public string S { get; set; }
        public string V { get; set; }
        public string A { get; set; }
        public string R { get; set; }
        public string G { get; set; }
        public string B { get; set; }
        public string X { get; set; }
        public string Y { get; set; }

        public string BackColor { get; set; }
        public bool isSelect { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        }
    }
}
