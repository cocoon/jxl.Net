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
using System.Runtime.CompilerServices;

namespace jxlNET.Decoder
{
    public class DecoderOptions
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        public static string BaseDir = AppDomain.CurrentDomain.BaseDirectory;

        private  string outDir = System.IO.Path.Combine(BaseDir, "Out");
        public string OutDir 
        { 
            get { return outDir; }
            set { outDir = value; NotifyPropertyChanged(); }
        }

        private string decoderPath = System.IO.Path.Combine(BaseDir, "djxl.exe");
        public string DecoderPath
        {
            get { return decoderPath; }
            set { decoderPath = value; NotifyPropertyChanged(); }
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




        public DecoderOptions()
        { }

        public DecoderOptions(string OutDir, string DecoderPath, string OutFilePrefix, string WorkingDirectory)
        {
            this.OutDir = OutDir;
            this.DecoderPath = DecoderPath;
            this.OutFilePrefix = OutFilePrefix;
            this.WorkingDirectory = WorkingDirectory;
        }

    }
}
