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

using jxlNET.Encoder.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace jxlNET.Decoder
{
    public class Decoder : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public static List<string> AllowedExtensions = new List<string>
        {
            ".jxl",           
        };

        private DecoderOptions _options = new DecoderOptions();
        public DecoderOptions Options
        {
            get { return _options; }
            set { _options = value; NotifyPropertyChanged(); }
        }

        // Constructor
        #region Constructor
        public Decoder()
        {
            DecoderArgumentBuilder();
        }

        public Decoder(DecoderOptions Options) : this()
        {
            this.Options = Options;
        }

        public Decoder(List<Parameter> Params) : this()
        {
            this.Params = Params;
        }

        public Decoder(DecoderOptions Options, List<Parameter> Params) : this()
        {
            this.Options = Options;
            this.Params = Params;
        }

        public Decoder(FileInfo InFile, FileInfo OutFile) : this()
        {
            this.InFile = InFile;
            this.OutFile = OutFile;
        }

        public Decoder(DecoderOptions Options, List<Parameter> Params, FileInfo InFile, FileInfo OutFile) : this()
        {
            this.Options = Options;
            this.Params = Params;
            this.InFile = InFile;
            this.OutFile = OutFile;
        }
        #endregion

        
        // InputFile and OutputFile
        #region InputOutput

        private string _inFilePath;
        public string InFilePath
        {
            get { return _inFilePath; }
            set
            {
                _inFilePath = value; NotifyPropertyChanged();
                _inFile = new FileInfo(value); NotifyPropertyChanged("InFile");
                generateOutFilePath();
                DecoderArgumentBuilder();
            }
        }

        private string _outFilePath;
        public string OutFilePath
        {
            get { return _outFilePath; }
            set
            {
                _outFilePath = value; NotifyPropertyChanged();
                _outFile = new FileInfo(value); NotifyPropertyChanged("OutFile");
                DecoderArgumentBuilder();
            }
        }

        private void generateOutFilePath()
        {
            OutFilePath = Path.Combine(Options.OutDir, Options.OutFilePrefix + InFile.Name + ".jxl");
        }

        private FileInfo _inFile;
        public FileInfo InFile
        {
            get { return _inFile; }
            set
            {
                _inFile = value; NotifyPropertyChanged();
                _inFilePath = value.FullName; NotifyPropertyChanged("InFilePath");
                generateOutFilePath();
                DecoderArgumentBuilder();
            }
        }

        private FileInfo _outFile;
        public FileInfo OutFile
        {
            get { return _outFile; }
            set
            {
                _outFile = value; NotifyPropertyChanged();
                _outFilePath = value.FullName; NotifyPropertyChanged("OutFilePath");
                DecoderArgumentBuilder();
            }
        }
        #endregion

        private List<Parameter> _params = new List<Parameter>
        {
        };
        public List<Parameter> Params
        {
            get { return _params; }
            set { _params = value; NotifyPropertyChanged(); DecoderArgumentBuilder(); }
        }


        public void AddOrReplaceParam(Parameter Param)
        {
            Type t = Param.GetType(); //example: jxlNET.Encoder.Parameters.JPEG
            //Console.WriteLine("AddOrReplaceParam: " + t);
            Params.RemoveAll(p => p.GetType().Equals(t)); //valid is only one instance of a parameter so we can remove all existing
            Params.Add(Param);
        }

        private string cmdLine = string.Empty;

        public string CmdLine
        {
            get { return cmdLine; }
            set { cmdLine = value; NotifyPropertyChanged();}
        }

        private BindableStringBuilder _processText = new BindableStringBuilder();

        public BindableStringBuilder ProcessText
        {
            get { return _processText; }
            set { _processText = (value); NotifyPropertyChanged(); }
        }

        private BindableStringBuilder _messages = new BindableStringBuilder();
        public BindableStringBuilder Messages
        {
            get { return _messages; }
            set { _messages = (value); NotifyPropertyChanged(); }
        }


        public bool DecoderExecutableExists
        {
            get
            {
                return File.Exists(Options.DecoderPath);
            }
        }



        public string DecoderArgumentBuilder()
        {
            StringBuilder args = new StringBuilder();

            if (InFile != null)
            {
                //Input
                args.Append("\"" + InFile.FullName + "\"");
            }

            if (OutFile != null)
            {
                //SPACER
                args.Append(" ");
                //Output
                args.Append("\"" + OutFile.FullName + "\"");
            }


            if (Params != null && Params.Count > 0)
            {
                foreach (var param in Params)
                {
                    //SPACER
                    args.Append(" ");
                    //Param
                    //args.Append(param.Param);
                    args.Append(param.ToString());
                }
            }

            Messages.AppendLine("argsDecoder: " + args.ToString());

            CmdLine = args.ToString();

            return args.ToString();
        }




        public void Decode()
        {
            decode();
        }

        public void Decode(FileInfo InputFile)
        {
            this.InFile = InputFile;
            decode();
        }

        public void Decode(FileInfo InputFile, FileInfo OutputFile)
        {
            this.InFile = InputFile;
            this.OutFile = OutputFile;
            decode();
        }

        public async Task<string> DecodeAsync()
        {
            return await Task.Run(() => decode());
        }

        public async Task<string> DecodeAsync(FileInfo InputFile, FileInfo OutputFile)
        {
            this.InFile = InputFile;
            this.OutFile = OutputFile;
            return await Task.Run(() => decode());
        }



        private string decode()
        {
            //Checks
            if (!File.Exists(InFile.FullName))
            {
                Messages.AppendLine("InputFile not found: " + InFile.FullName);
                return "error";
            }
            if (!File.Exists(Options.DecoderPath))
            {
                Messages.AppendLine("Decoder not found: " + Options.DecoderPath);
                return "error";
            }
            if (!Directory.Exists(OutFile.Directory.FullName))
            {
                Messages.AppendLine("OutDirectory not found, try to create: " + OutFile.Directory.FullName);

                try
                {
                    Directory.CreateDirectory(OutFile.Directory.FullName);
                }
                catch(Exception ex)
                {
                    Messages.AppendLine("OutDirectory could not be created: " + OutFile.Directory.FullName);
                    return "error";
                }
            }



            //djxl --jpeg OutFile
            Messages.AppendLine("OutFile.Extension.ToLower(): " + OutFile.Extension.ToLower());

            if (OutFile.Extension.ToLower() == ".jpg" || OutFile.Extension.ToLower() == ".jpeg")
            {
                //argsDecoder.Append("--jpeg ");
                Params.Add(new jxlNET.Decoder.Parameters.JPEG());
            }
            else if (OutFile.Extension.ToLower() == ".png")
            {

            }

            string args = CmdLine;
            Messages.AppendLine("Decoder Parameter: " + args);

            ProcessStartInfo startInfoDecoder = new ProcessStartInfo
            {
                Arguments = args,
                FileName = Options.DecoderPath,
                WorkingDirectory = Options.WorkingDirectory,

                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };


            //SAVE Decoded FILE
            Messages.AppendLine("Decode File: " + InFile.FullName + " to: " + OutFile.FullName);

            Process procDecode = new Process();
            procDecode.StartInfo = startInfoDecoder;
            procDecode.Start();

            string processDecodeStandardOutput = procDecode.StandardOutput.ReadToEnd();
            string processDecodeStandardError = procDecode.StandardError.ReadToEnd();

            ProcessText.Append(Environment.NewLine + processDecodeStandardOutput + Environment.NewLine + processDecodeStandardError);

            //Console.WriteLine("processDecodeStandardOutput: " + processDecodeStandardOutput);
            //Console.WriteLine("processDecodeStandardError: " + processDecodeStandardError);

            Messages.AppendLine("Decoding DONE: " + OutFile.FullName);
            return ProcessText.Text + Environment.NewLine + Messages.Text;
        }




    }
}
