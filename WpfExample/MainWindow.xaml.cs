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


        const string exampleFileName = "160011_architecture-air-conditioner-building.jpg";
        static string exampleDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "example");
        static string example = Path.Combine(exampleDir, exampleFileName);

        private Encoder enc;
        public Encoder Enc
        {
            get { return enc; }
            set { enc = value; NotifyPropertyChanged(); }
        }





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
                EncoderPath = @"c:\dev\jxl\cjxl.exe",
                Validate = true
            };

            // Create new encoder with previously prepared Options
            Enc = new Encoder(encOptions);

            //Try to get versions
            encOptions.TryGetEncoderVersionInfo();
            enc.Decoder.Options.TryGetDecoderVersionInfo();


            // Set example image as input file
            if (File.Exists(example))
            {
                Enc.InFile = new FileInfo(example);
            }


            // Configure Quality
            jxlNET.Encoder.Parameters.Quality q = new jxlNET.Encoder.Parameters.Quality();
            //q.Value = 101; //Throws ArgumentOutOfRangeException limit is 100
            q.Value = 50; //Throws ArgumentOutOfRangeException limit is 100
            Console.WriteLine("Encode with Quality: " + q.Value);
            Enc.AddOrReplaceParam(q);


            // Configure Speed
            jxlNET.Encoder.Parameters.Speed s = new jxlNET.Encoder.Parameters.Speed(3);
            Enc.AddOrReplaceParam(s);

            // print out all current set parameter
            foreach (var p in enc.Params)
            {
                Console.WriteLine("param: " + p.ToString());
            }

            // Dynamically build controls to add parameter
            ListBoxParameter.FillListBox(LbParam, enc);
        }


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
                jxlNET.Presets.Save("Test.preset", Enc.Params);
            }
            catch (Exception error) { Console.WriteLine(error.ToString()); }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists("Test.preset"))
                {
                    var param = jxlNET.Presets.Load("Test.preset");
                    if (param != null)
                    {
                        Enc.Params = param;
                    }
                }
            }
            catch (Exception error) { Console.WriteLine(error.ToString()); }
        }
    }
}
