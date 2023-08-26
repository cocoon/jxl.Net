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

namespace jxlNET.Encoder
{
    public class Encoder: INotifyPropertyChanged
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
            ".png",
            ".apng",
            ".jpeg",
            ".jpg",
            ".ppm",
            ".pfm",
            ".pgx"
        };

        private EncoderOptions _options = new EncoderOptions();
        public EncoderOptions Options
        {
            get { return _options; }
            set { _options = value; NotifyPropertyChanged(); }
        }

        private jxlNET.Decoder.Decoder _decoder = new jxlNET.Decoder.Decoder();
        public jxlNET.Decoder.Decoder Decoder
        {
            get { return _decoder; }
            set { _decoder = value; NotifyPropertyChanged(); NotifyPropertyChanged("ProcessResultText"); }
        }


        // Constructor
        #region Constructor

        public Encoder()
        {
            //Decoder.PropertyChanged += Decoder_PropertyChanged;
            EncoderArgumentBuilder();
        }

        void Decoder_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("Decoder_PropertyChanged e.PropertyName: " + e.PropertyName);

            if (e.PropertyName == "ProcessText")
            {
                Console.WriteLine("Decoder_PropertyChanged NOTIFY NOW");
                NotifyPropertyChanged("ProcessText");
                NotifyPropertyChanged("ProcessResultText"); 
            }
            else if (e.PropertyName == "Messages")
            {
                Console.WriteLine("Decoder_PropertyChanged NOTIFY NOW");
                NotifyPropertyChanged("Messages");
            }
        }

        public Encoder(EncoderOptions Options) : this()
        {
            this.Options = Options;
        }

        public Encoder(List<Parameter> Params) : this()
        {
            this.Params = Params;
        }

        public Encoder(EncoderOptions Options, List<Parameter> Params) : this()
        {
            this.Options = Options;
            this.Params = Params;
        }

        public Encoder(FileInfo InFile, FileInfo OutFile) : this()
        {
            this.InFile = InFile;
            this.OutFile = OutFile;
        }

        public Encoder(EncoderOptions Options, List<Parameter> Params, FileInfo InFile, FileInfo OutFile) : this()
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
                string relPath = PathNetCore.GetRelativePath(new FileInfo(Options.EncoderPath).DirectoryName, value);
                _inFilePath = relPath; NotifyPropertyChanged(); 
                _inFile = new FileInfo(value); NotifyPropertyChanged("InFile"); 
                generateOutFilePath();
                EncoderArgumentBuilder();
            }
        }

        private string _outFilePath;
        public string OutFilePath
        {
            get { return _outFilePath; }
            set 
            {
                string relPath = PathNetCore.GetRelativePath(new FileInfo(Options.EncoderPath).DirectoryName, value);
                _outFilePath = relPath; NotifyPropertyChanged(); 
                _outFile = new FileInfo(value); NotifyPropertyChanged("OutFile");
                EncoderArgumentBuilder();
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
                string relPath = PathNetCore.GetRelativePath(new FileInfo(Options.EncoderPath).DirectoryName, value.FullName);
                _inFile = value; NotifyPropertyChanged(); 
                _inFilePath = relPath; NotifyPropertyChanged("InFilePath");
                generateOutFilePath();
                EncoderArgumentBuilder();
            }
        }

        private FileInfo _outFile;
        public FileInfo OutFile
        {
            get { return _outFile; }
            set 
            {
                string relPath = PathNetCore.GetRelativePath(new FileInfo(Options.EncoderPath).DirectoryName, value.FullName);
                _outFile = value; NotifyPropertyChanged(); 
                _outFilePath = relPath; NotifyPropertyChanged("OutFilePath"); 
                EncoderArgumentBuilder(); 
            }
        }
        #endregion

        // Parameter
        #region Parameter
        private List<Parameter> _params = new List<Parameter>
        {
            //new Modular(), //-m -s X -q 100 = crash with example jpeg
            new Quality(100),
            //new Speed(9) // in v0.8.0 -s is not available anylonger, it is now -e for EFFORT
            new Effort(9),
        };
        public List<Parameter> Params
        {
            get { return _params; }
            set { _params = value; NotifyPropertyChanged(); EncoderArgumentBuilder(); }
        }

        public void AddOrReplaceParam(Parameter Param)
        {
            Type t = Param.GetType(); //example: jxlNET.Encoder.Parameters.Quality
            Console.WriteLine("AddOrReplaceParam: " + t);
            Params.RemoveAll(p => p.GetType().Equals(t)); //valid is only one instance of a parameter so we can remove all existing
            Params.Add(Param);
            EncoderArgumentBuilder();
        }

        public void RemoveParam(Parameter Param)
        {
            Type t = Param.GetType(); //example: jxlNET.Encoder.Parameters.Quality
            Console.WriteLine("RemoveParam: " + t);
            Params.RemoveAll(p => p.GetType().Equals(t));
            EncoderArgumentBuilder();
        }

        #endregion

        private string cmdLine = string.Empty;

        public string CmdLine
        {
            get { return cmdLine; }
            set { cmdLine = value; NotifyPropertyChanged(); }
        }

        private BindableStringBuilder _processText = new BindableStringBuilder();

        public BindableStringBuilder ProcessText
        {
            get { return _processText; }
            set { _processText = (value); NotifyPropertyChanged(); }
        }

        public string ProcessResultText
        {
            get { return ProcessText.Text + Environment.NewLine + Decoder.ProcessText.Text; }
        }

        private BindableStringBuilder _messages = new BindableStringBuilder();
        public BindableStringBuilder Messages
        {
            get { return _messages; }
            set { _messages = (value); NotifyPropertyChanged(); }
        }

        public bool EncoderExecutableExists
        {
            get
            {
                return File.Exists(Options.EncoderPath);
            }
        }


        public string EncoderArgumentBuilder()
        {
            StringBuilder args = new StringBuilder();

            if (InFile != null)
            {
                //Input
                args.Append("\"" + InFilePath + "\"");
            }

            if (OutFile != null)
            {
                //SPACER
                args.Append(" ");
                //Output
                args.Append("\"" + OutFilePath + "\"");
            }

            if(Params != null && Params.Count > 0)
            {
                foreach (var param in Params)
                {
                    //SPACER
                    args.Append(" ");
                    //Param
                    //args.Append(param.Param);
                    args.Append(param.ToString());

                    try
                    {
                        if (param.GetType() == typeof(Quality) && ((Quality)param).Value < 100) args.Append(" --lossless_jpeg=0");
                    }
                    catch(Exception ex)
                    { }

                    try
                    {
                        if (param.GetType() == typeof(Distance) && ((Distance)param).Value > 0) args.Append(" --lossless_jpeg=0");
                    }
                    catch (Exception ex)
                    { }

                }
            }

            CmdLine = args.ToString();

            return args.ToString();
        }


        public string Encode()
        {
            return encode();
        }

        public void Encode(FileInfo InputFile)
        {
            this.InFile = InputFile;
            encode();
        }

        public void Encode(FileInfo InputFile, FileInfo OutputFile)
        {
            this.InFile = InputFile;
            this.OutFile = OutputFile;
            encode();
        }

        public async Task<string> EncodeAsync()
        {
            return await Task.Run(() => encode());
        }

        public async Task<string> EncodeAsync(FileInfo InputFile, FileInfo OutputFile)
        {
            this.InFile = InputFile;
            this.OutFile = OutputFile;
            return await Task.Run(() => encode());
        }


        private string encode()
        {
            //Checks
            if (!File.Exists(InFile.FullName))
            {
                string m = "InputFile not found: " + InFile.FullName;
                Messages.AppendLine(m);
                return "error:  " + m;
            }
            if (!File.Exists(Options.EncoderPath))
            {
                string m = "Encoder not found: " + Options.EncoderPath;
                Messages.AppendLine(m);
                return "error:  " + m;
            }
            if (!Directory.Exists(Options.OutDir))
            {
                Messages.AppendLine("OutDirectory not found, try to create: " + Options.OutDir);

                try
                {
                    Directory.CreateDirectory(Options.OutDir);
                }
                catch (Exception ex)
                {
                    string m = "OutDirectory could not be created: " + Options.OutDir;
                    Messages.AppendLine(m);
                    return "error: " + m;
                }
            }


            var watch = System.Diagnostics.Stopwatch.StartNew();

            if (File.Exists(InFile.FullName) && AllowedExtensions.Contains(InFile.Extension.ToLower()))
            {
                Messages.AppendLine("OutFilePath: " + OutFilePath);

                //Check
                if (!Directory.Exists(OutFile.Directory.FullName))
                {
                    Messages.AppendLine("OutDirectory not found, try to create: " + OutFile.Directory.FullName);

                    try
                    {
                        Directory.CreateDirectory(OutFile.Directory.FullName);
                    }
                    catch (Exception ex)
                    {
                        string m = "OutDirectory could not be created: " + OutFile.Directory.FullName;
                        Messages.AppendLine(m);
                        return "error: " + m;
                    }
                }




                Messages.AppendLine("Encoder Path: " + Options.EncoderPath);

                string args = CmdLine;
                Messages.AppendLine("Encoder Parameter: " + args);

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = args,
                    FileName = Options.EncoderPath,
                    WorkingDirectory = Options.WorkingDirectory,

                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };




                //SAVE New FILE
                Messages.AppendLine("Saving File: " + OutFilePath);

                Process proc = new Process();
                proc.StartInfo = startInfo;
                proc.Start();

                string processStandardOutput = proc.StandardOutput.ReadToEnd();
                string processStandardError = proc.StandardError.ReadToEnd();
                
                ProcessText.Append(Environment.NewLine + processStandardOutput + Environment.NewLine + processStandardError);

                //Console.WriteLine("processStandardOutput: " + processStandardOutput);
                //Console.WriteLine("processStandardError: " + Environment.NewLine + processStandardError);

                Messages.AppendLine("DONE: " + OutFilePath);


                //
                // Nice to have ...
                //

                //Decode and check if checksum is the same as orig file in lossless, Option = Validate

                if (Options.Validate)
                {
                    // Checksum InFile
                    string checksumOrig = Checksum.SHA256CheckSum(InFile.FullName);
                    Messages.AppendLine("checksumOrig: " + checksumOrig);

                    string DecodedFilePath = Path.Combine(OutFile.Directory.FullName, "verify_" + InFile.Name);
                    FileInfo DecodedFileInfo = new FileInfo(DecodedFilePath);

                    //DECODE
                    Decoder.Decode(OutFile, DecodedFileInfo);

                    //CHECKSUM
                    string checksumDecoded = "failed";
                    if (File.Exists(DecodedFileInfo.FullName))
                    {
                        checksumDecoded = Checksum.SHA256CheckSum(DecodedFileInfo.FullName);
                    }

                    Messages.AppendLine("checksumDecoded: " + checksumDecoded);
                    Messages.AppendLine("checksumOrig: " + checksumOrig);
                }

            }
            else
            {
                Messages.AppendLine("ERROR: file dos not exist or extension not correct: " + InFile.FullName);
            }


            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            var elapsedMinutes = TimeSpan.FromMilliseconds(elapsedMs).TotalMinutes;

            Messages.AppendLine("done. (" + elapsedMinutes + " Minutes)");
            return ProcessText.Text + Environment.NewLine + Messages.Text;
        }

    }
}
