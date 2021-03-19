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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace jxlNET.Encoder
{
    public class EncoderOptions : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        public static string BaseDir = AppDomain.CurrentDomain.BaseDirectory;

        private string outDir = System.IO.Path.Combine(BaseDir, "Out");
        public string OutDir
        {
            get { return outDir; }
            set { outDir = value; NotifyPropertyChanged(); }
        }

        private string encoderPath = System.IO.Path.Combine(BaseDir, "cjxl.exe");
        public string EncoderPath
        {
            get { return encoderPath; }
            set { encoderPath = value; NotifyPropertyChanged(); }
        }

        private string outFilePrefix = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH-mm-ss-fffffffZ") + "_";
        public string OutFilePrefix
        {
            get { return outFilePrefix; }
            set { outFilePrefix = value; NotifyPropertyChanged(); }
        }

        private string workingDirectory = BaseDir;
        public string WorkingDirectory
        {
            get { return workingDirectory; }
            set { workingDirectory = value; NotifyPropertyChanged(); }
        }

        private bool validate = false;
        /// <summary>
        /// Decode the new encoded file and verify checksums (should be the same if lossless)
        /// </summary>
        public bool Validate
        {
            get { return validate; }
            set { validate = value; NotifyPropertyChanged(); }
        }


        private Version encoderVersion;

        public Version EncoderVersion
        {
            get { return encoderVersion; }
            set { encoderVersion = value; NotifyPropertyChanged(); }
        }

        public Version TryGetEncoderVersionInfo()
        {
            Version result = null;

            if (File.Exists(EncoderPath))
            {

                var version = FileVersionInfo.GetVersionInfo(EncoderPath);
                Console.WriteLine("version from FileVersionInfo: " + version);

                if (!string.IsNullOrEmpty(version.FileVersion))
                {
                    System.Version.TryParse(version.FileVersion, out result);
                    EncoderVersion = result;
                }
                else
                {
                    try
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            Arguments = new Parameters.Version().Param,
                            FileName = EncoderPath,
                            WorkingDirectory = WorkingDirectory,

                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        };

                        Process proc = new Process();
                        proc.StartInfo = startInfo;
                        proc.Start();

                        string processStandardOutput = proc.StandardOutput.ReadLine();
                        //string processStandardError = proc.StandardError.ReadToEnd();

                        if(!string.IsNullOrEmpty(processStandardOutput))
                        {
                            string searchString = "[v";
                            int index = processStandardOutput.IndexOf(searchString);
                            if (index != -1)
                            {
                                string v = processStandardOutput.Substring(index + searchString.Length, 5);
                                Console.WriteLine("version from output: " + v);
                                string[] vArray = v.Split('.');
                                if (vArray != null && vArray.Length == 3)
                                {
                                    System.Version.TryParse(v, out result);
                                    Console.WriteLine("Version parsed: " + result);
                                    EncoderVersion = result;
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    { }
                }
            }
            
            
            return result;
        }



        public EncoderOptions()
        { }

        public EncoderOptions(string OutDir, string EncoderPath, string OutFilePrefix, string WorkingDirectory)
        {
            this.OutDir = OutDir;
            this.EncoderPath = EncoderPath;
            this.OutFilePrefix = OutFilePrefix;
            this.WorkingDirectory = WorkingDirectory;
        }

    }
}
