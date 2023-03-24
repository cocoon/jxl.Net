// Copyright (c) 2021 github.com/cocoon
// 
// The copyright notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Windows;
using jxlNET.Encoder;
using System.IO;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using ImageMagick;
using System.Collections.Generic;
using System.Windows.Media;
using jxlNET.Decoder;
using System.Linq;

namespace WpfExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Vars

        public static string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
        const string exampleFileName = "160011_architecture-air-conditioner-building.jpg";
        static string exampleDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "example");
        static string example = Path.Combine(exampleDir, exampleFileName);

        private Encoder enc;
        public Encoder Enc
        {
            get { return enc; }
            set
            {
                enc = value; NotifyPropertyChanged();
                if (value.OutFile != null) initWatcher(value.OutFile.FullName);
            }
        }


        #region Presets

        private string PresetsDirectory = Path.Combine(BaseDir, "Presets");

        private List<FileInfo> _loadedPresets = new List<FileInfo>();
        public List<FileInfo> LoadedPresets
        {
            get { return _loadedPresets; }
            set { _loadedPresets = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region Images
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


        private string _image1 = String.Empty;
        public string Image1
        {
            get { return _image1; }
            set { 
                _image1 = value;
                if (Enc != null)
                {
                    Enc.InFilePath = value;
                    Image2 = Enc.OutFilePath;
                }
                NotifyPropertyChanged(); NotifyPropertyChanged("ImageSource1"); 
            }
        }


        private BitmapSource _imageSource1;

        public BitmapSource ImageSource1
        {
            get
            {
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


        private string _image2 = String.Empty;

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

        #endregion

        #region FileSystemWatcher
        private FileSystemWatcher _watcher = new FileSystemWatcher();

        private void initWatcher(string Path)
        {
            if (!string.IsNullOrEmpty(Path))
            {
                if (_watcher != null)
                {
                    _watcher.EnableRaisingEvents = false;

                    var dir = new FileInfo(Path).Directory.FullName;
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

                        if (File.Exists(Path)) OnFsEvent(Image2);
                    }
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






        /// <summary>
        /// MAIN
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Init();
        }






        private void Init()
        {
            // Create Options for the encoder and activate validation
            EncoderOptions encOptions = new EncoderOptions
            {
                EncoderPath = Path.Combine(BaseDir, "cjxl.exe"),
                Validate = false
            };

            // Create new encoder with previously prepared Options
            Enc = new Encoder(encOptions);

            // Create Options for the encoder and activate validation
            DecoderOptions decOptions = new DecoderOptions
            {
                DecoderPath = Path.Combine(BaseDir, "djxl.exe"),
            };


            //Try to get versions
            encOptions.TryGetEncoderVersionInfo();
            enc.Decoder.Options.TryGetDecoderVersionInfo();

            // listen to propertychange events of the encoder to get notified
            // if the images changes to update the FileSystem watcher
            // and it updates the image in the viewer component
            enc.PropertyChanged += enc_PropertyChanged;

            initWatcher(Image2);


            // Set example image as input file
            if (File.Exists(example))
            {
                Enc.InFile = new FileInfo(example);
                Image1 = example;
            }


            // Configure Quality
            jxlNET.Encoder.Parameters.Quality q = new jxlNET.Encoder.Parameters.Quality();
            //q.Value = 101; //Throws ArgumentOutOfRangeException limit is 100
            q.Value = 50; //Throws ArgumentOutOfRangeException limit is 100
            Console.WriteLine("Encode with Quality: " + q.Value);
            Enc.AddOrReplaceParam(q);

            // Configure Speed (deprecated in v0.5.0)
            //jxlNET.Encoder.Parameters.Speed s = new jxlNET.Encoder.Parameters.Speed(3);
            //Enc.AddOrReplaceParam(s);

            // Configure Effort
            jxlNET.Encoder.Parameters.Effort e = new jxlNET.Encoder.Parameters.Effort(3);
            Enc.AddOrReplaceParam(e);

            //Enc.CmdLine += (" --lossless_jpeg=0 ");


            // print out all current set parameter
            foreach (var p in enc.Params)
            {
                Console.WriteLine("param: " + p.ToString());
            }

            //Load saved Presets from files
            LoadPresets();

            // Dynamically build controls to add parameter
            ListBoxParameter.FillListBox(LbParam, enc);
        }



        private void LoadPresets()
        {
            if (!Directory.Exists(PresetsDirectory))
            {
                try
                {
                    Directory.CreateDirectory(PresetsDirectory);
                }
                catch (Exception ex)
                {
                    string m = "Presets Directory could not be created: " + PresetsDirectory;
                    MessageBox.Show(m);
                }
            }
            if (Directory.Exists(PresetsDirectory))
            {
                try
                {
                    var presets = Directory.EnumerateFiles(PresetsDirectory, "*.preset");
                    if (presets != null && presets.Count() > 0)
                    {
                        LoadedPresets.Clear();

                        foreach (var p in presets)
                        {
                            LoadedPresets.Add(new FileInfo(p));
                        }
                    }
                }
                catch (Exception ex)
                {
                    string m = "Presets Directory could not be created: " + PresetsDirectory;
                    MessageBox.Show(m);
                }
            }
        }


        void enc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "InFile":
                    NotifyPropertyChanged("ImageSource1");
                    break;
                case "OutFile":
                    initWatcher(enc.OutFile.FullName);
                    break;
            }
        }

        #region Theme
        void ApplyTheme(bool Clear)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            if (!Clear) Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("DarkTheme.xaml", UriKind.Relative) });
        }

        private void ChkDarkMode_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (ChkDarkMode.IsChecked == true) ApplyTheme(false);
            else ApplyTheme(true);
        }
        #endregion

        #region Button Clicks

        private void BtnInput_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = exampleDir;

            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                if (Enc != null) Enc.InFilePath = openFileDialog.FileName;
            }
        }

        private void BtnOutput_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = new DirectoryInfo(Enc.OutFilePath).Root.FullName;

                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == true)
                {
                    if (Enc != null) Enc.OutFilePath = openFileDialog.FileName;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEncode_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxInput.Text) || string.IsNullOrEmpty(txtBoxOutput.Text)) return;
            
            // Start Encoding
            Enc.Encode(); // synchronous
        }


        private async void btnEncodeAsync_Click(object sender, RoutedEventArgs e)
        {
            txtBoxStatus.Text = "encoding started ...";

            // run a encoding in other thread
            var result = await Enc.EncodeAsync(); //async Task
            // encoding is finished here

            // modify UI object in UI thread
            Console.WriteLine(result);
            txtBoxStatus.Text = "encoding done. ";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = PresetsDirectory;
                saveFileDialog.Filter = "Preset file (*.preset)|*.preset|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == true)
                {
                    jxlNET.Presets.Save(saveFileDialog.FileName, Enc.Params);
                    LoadPresets();
                }
            }
            catch (Exception error) { Console.WriteLine(error.ToString()); }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileInfo preset = (FileInfo)Presets.SelectedItem;

                if (preset != null)
                {
                    if (File.Exists(preset.FullName))
                    {
                        var param = jxlNET.Presets.Load(preset.FullName);
                        if (param != null)
                        {
                            //Enc.Params = param;
                            Enc.Params.Clear();
                            if(param.Count > 0)
                            {
                                foreach (var p in param)
                                {
                                    Enc.AddOrReplaceParam(p);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Preset not found: " + preset);
                    }
                }
            }
            catch (Exception error) { Console.WriteLine(error.ToString()); }
        }

        private void BtnEncoder_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "JPEG-XL Encoder (cjxl.exe)|cjxl.exe|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = BaseDir;

            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                if (Enc != null) Enc.Options.EncoderPath = openFileDialog.FileName;
                Enc.Options.TryGetEncoderVersionInfo();
            }
        }

        private void BtnDecoder_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "JPEG-XL Decoder (djxl.exe)|djxl.exe|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = BaseDir;

            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                if (Enc != null) Enc.Options.EncoderPath = openFileDialog.FileName;
                Enc.Decoder.Options.TryGetDecoderVersionInfo();
            }
        }

        #endregion


        #region DragnDrop

        private void Drop_Input(object sender, DragEventArgs e)
        {
            HandleDrop(e, "Input");
        }

        private void Drop_Output(object sender, DragEventArgs e)
        {
            HandleDrop(e, "Output");
        }

        public void HandleDrop(DragEventArgs e, string Code)
        {
            DragDropEffects effects = DragDropEffects.None;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileDrops = (string[])e.Data.GetData(DataFormats.FileDrop);

                //if (fileDrops != null && fileDrops.Length == 1)
                if (fileDrops != null && fileDrops.Length > 0)
                {
                    foreach (var fileDrop in fileDrops)
                    {
                        FileInfo fInfo = new FileInfo(fileDrop);

                        if (File.Exists(fInfo.FullName))
                        {
                            if (Code == "Input")
                            {
                                if (Encoder.AllowedExtensions.Contains(fInfo.Extension))
                                {
                                    Enc.InFile = fInfo;
                                }
                                else
                                {
                                    MessageBox.Show("Extension not allowed: " + fInfo.Extension);
                                }
                            }
                            if (Code == "Output") Enc.OutFile = fInfo;

                        }
                        else if (Directory.Exists(fileDrop))
                        {
                            HandleFolderDrop(fileDrop);
                        }
                    }
                }
                else if (fileDrops != null && fileDrops.Length > 1)
                {
                    foreach (var fileDrop in fileDrops)
                    {
                        //handle multiple images
                    }
                }

            }
        }


        void HandleFolderDrop(string path)
        {
            throw new NotImplementedException();
        }

        private void txtBoxInput_PreviewDragOver(object sender, DragEventArgs e)
        {
            //needed to enable drag drop for other types besides strings
            e.Handled = true;
        }

        private void txtBoxOutput_PreviewDragOver(object sender, DragEventArgs e)
        {
            //needed to enable drag drop for other types besides strings
            e.Handled = true;
        }



        #endregion


    }
}
