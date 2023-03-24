using ImageMagick;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace jxlViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Vars
        
        static string exampleDir = AppDomain.CurrentDomain.BaseDirectory;

        const string example1OrigFileName = "b500.png";
        static string exampleOrig = Path.Combine(exampleDir, example1OrigFileName);

        const string example1ResultFileName = "b500_q1.jxl";
        static string exampleResult= Path.Combine(exampleDir, example1ResultFileName);


        private BitmapSource redImage()
        {
            int width = 128;
            int height = width;
            int stride = width / 8;
            byte[] pixels = new byte[height * stride];

            List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
            colors.Add(System.Windows.Media.Colors.Red);
            BitmapPalette tempPalette = new BitmapPalette(colors);

            BitmapSource image = BitmapSource.Create(
                width,
                height,
                96,
                96,
                PixelFormats.Indexed1,
                tempPalette,
                pixels,
                stride);

            return image;
        }



        private string _image1 = exampleOrig;
        public string Image1
        {
            get { return _image1; }
            set { _image1 = value; NotifyPropertyChanged(); NotifyPropertyChanged("ImageSource1"); }
        }

        private BitmapSource _imageSource1;

        public BitmapSource ImageSource1
        {
            get {
                if (File.Exists(Image1))
                {
                    using (var image = new MagickImage(Image1))
                    {
                        return image.ToBitmapSource();
                    }
                }
                else
                {
                    return redImage();
                }          
            }
        }


        private string _image2 = exampleResult;

        public string Image2
        {
            get { return _image2; }
            set { _image2 = value; initWatcher(value); NotifyPropertyChanged(); NotifyPropertyChanged("ImageSource2"); }
        }


        private BitmapSource _imageSource2;

        public BitmapSource ImageSource2
        {
            get
            {
                if (File.Exists(Image2))
                {
                    using (var image = new MagickImage(Image2))
                    {
                        return image.ToBitmapSource();
                    }
                }
                else
                {
                    return redImage();
                }
            }
        }

        #endregion

        #region FileSystemWatcher
        private FileSystemWatcher _watcher = new FileSystemWatcher();

        private void initWatcher(string Path)
        {
            if (_watcher != null)
            {
                _watcher.EnableRaisingEvents = false;

                var dir = new FileInfo(Image2).Directory.FullName;
                if (Directory.Exists(dir))
                {
                    Console.WriteLine("FileSystemWatcher(dir) " + dir);

                    _watcher = new FileSystemWatcher(dir);
                    _watcher.IncludeSubdirectories = false;
                    _watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size;
                    _watcher.Created += fsChangeHandler;
                    _watcher.Changed += fsChangeHandler;
                    _watcher.Renamed += fsChangeHandler;
                    _watcher.Filter = "*.*";
                    _watcher.EnableRaisingEvents = true;
                }
            }
        }

        void fsChangeHandler(object sender, FileSystemEventArgs e)
        {
            try
            {
                _watcher.EnableRaisingEvents = false;

                if (e != null && e.FullPath == new FileInfo(Image2).FullName)
                {
                    OnFsEvent(Image2);
                }
            }
            finally
            {
                _watcher.EnableRaisingEvents = true;
            }
        }

        public void OnFsEvent(string Path)
        {
            if (File.Exists(Path))
            {
                NotifyPropertyChanged("ImageSource2");
            }
        }
        #endregion


        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            initWatcher(Image2);
        }


        #region ButtonClicks
        private void BtnImage1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "JPEG files (*.jpg)|*.jpg|PNG|*.png|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = exampleDir;

            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                Image1 = openFileDialog.FileName;
            }
        }

        private void BtnImage2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "JPEG-XL files (*.jxl)|*.jxl|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = exampleDir;

            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                Image2 = openFileDialog.FileName;
            }
        }
        #endregion



    }
}
