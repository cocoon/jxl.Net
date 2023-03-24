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
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace jxlNET
{
    public enum OptionType
    {
        Flag,
        Value
    }

    // ENCODER
    [XmlInclude(typeof(jxlNET.Encoder.Parameters.ColorSpace))]
    [XmlInclude(typeof(jxlNET.ColorSpaceBase))]
    [XmlInclude(typeof(jxlNET.ColorSpaceName))]
    [XmlInclude(typeof(jxlNET.Encoder.Parameters.ColorTransform))]
    [XmlInclude(typeof(jxlNET.ColorTransformBase))]
    [XmlInclude(typeof(Container))]

    [XmlInclude(typeof(Distance))]
    [XmlInclude(typeof(Dots))]

    [XmlInclude(typeof(jxlNET.Encoder.Parameters.EdgePreservingFilterLevel))]
    [XmlInclude(typeof(jxlNET.EdgePreservingFilterLevelBase))]
    
    [XmlInclude(typeof(ExtraProperties))]

    [XmlInclude(typeof(Gaborish))]
    [XmlInclude(typeof(GroupSize))]

    [XmlInclude(typeof(IntensityTarget))]
    [XmlInclude(typeof(Iterations))]

    [XmlInclude(typeof(JpegTranscode))]

    [XmlInclude(typeof(LossyPalette))]
    
    [XmlInclude(typeof(MiddleOut))]
    [XmlInclude(typeof(Modular))]
    [XmlInclude(typeof(MQuality))]

    [XmlInclude(typeof(NearLossless))]
    [XmlInclude(typeof(Noise))]
    [XmlInclude(typeof(NumReps))]
    [XmlInclude(typeof(NumThreads))]
    
    [XmlInclude(typeof(OverrideBitdepth))]
    
    [XmlInclude(typeof(Palette))]
    [XmlInclude(typeof(PostCompact))]
    [XmlInclude(typeof(PreCompact))]
    [XmlInclude(typeof(Predictor))]
    [XmlInclude(typeof(PrintProfile))]
    [XmlInclude(typeof(Progressive))]
    [XmlInclude(typeof(ProgressiveAC))]
    [XmlInclude(typeof(ProgressiveDC))]

    [XmlInclude(typeof(QProgressiveAC))]
    [XmlInclude(typeof(Quality))]

    [XmlInclude(typeof(jxlNET.Encoder.Parameters.Resampling))]
    [XmlInclude(typeof(jxlNET.ResamplingBase))]
    [XmlInclude(typeof(Responsive))]

    [XmlInclude(typeof(SaliencyMapFilename))]
    [XmlInclude(typeof(SaliencyNumProgressiveSteps))]
    [XmlInclude(typeof(SaliencyThreshold))]
    [XmlInclude(typeof(Speed))]
    [XmlInclude(typeof(Effort))]
    [XmlInclude(typeof(Strip))]

    [XmlInclude(typeof(TargetBPP))]
    [XmlInclude(typeof(TargetSize))]

    [XmlInclude(typeof(UseNewHeuristics))]

    [XmlInclude(typeof(Verbose))]


    // DECODER
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.AllowMoreProgressiveSteps))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.AllowPartialFiles))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.BitsPerSample))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.ColorSpace))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.DisplayNits))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.DownSampling))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.JPEG))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.JpegQuality))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.JPEG_up_to_0_3_2))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.NumReps))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.NumThreads))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.PrintInfo))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.PrintProfile))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.PrintReadBytes))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.Quiet))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.ToneMap))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.UseSJpeg))]
    [XmlInclude(typeof(jxlNET.Decoder.Parameters.Version))]

    [XmlRoot(Namespace = "jxlNET")]
    public abstract class Parameter
    {
        public abstract string Description { get; }
        public abstract string Name { get; }
        public abstract string Param { get; }
        public abstract string ParamLong { get; }

        public abstract OptionType OptionType { get; }

        //encoder expects dot as separator
        public static NumberStyles nStyle = NumberStyles.Number;
        public static CultureInfo cultureInfo = new CultureInfo("en-US");
        public static NumberFormatInfo n = new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." };



        //Constructor
        public Parameter() { }

        public override string ToString()
        {
            return Param;
        }

        internal static int CheckArgumentRange(string paramName, int value, int minInclusive, int maxInclusive)
        {
            if (value < minInclusive || value > maxInclusive)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"Value should be in range [{minInclusive}-{maxInclusive}]");
            }
            return value;
        }


        /*
          // Add an option with a value of type T. The option can be passed as
          // '-s <value>' or '--long value' or '--long=value'. The CommandLineParser
          // parser will call the function parser with the string pointing to '<value>'
          // in either case. Returns the id of the added option or kOptionError on
          // error.
          template <typename T>
          OptionId AddOptionValue(char short_name, const char* long_name,
                                  const char* metavar, const char* help_text,
                                  T* storage, bool(parser)(const char*, T*),
                                  int verbosity_level = 0) {
            options_.emplace_back(new CmdOptionFlag<T>(short_name, long_name, metavar,
                                                       help_text, storage, parser,
                                                       verbosity_level));
            return options_.size() - 1;
          }

          // Add a flag without a value. Returns the id of the added option or
          // kOptionError on error.
          template <typename T>
          OptionId AddOptionFlag(char short_name, const char* long_name,
                                 const char* help_text, T* storage, bool(parser)(T*),
                                 int verbosity_level = 0) {
            options_.emplace_back(new CmdOptionFlag<T>(
                short_name, long_name, help_text, storage, parser, verbosity_level));
            return options_.size() - 1;
          }
        */


/*
cjxl.exe -h -v
  J P E G   \/ |
            /\ |_   e n c o d e r    [v0.3.2 | SIMD supported: SSE4,Scalar]

Usage: cjxl.exe INPUT OUTPUT [OPTIONS...]
 INPUT
    the input can be PNG, APNG, GIF, JPEG, PPM, PFM, or PGX
 OUTPUT
    the compressed output file (optional)
 -V, --version
    print version number and exit
 --quiet
    be more silent
 --container
    encode using container format
 --print_profile=0|1
    print timing information before exiting
 -d maxError, --distance=maxError
    Max. butteraugli distance, lower = higher quality. Range: 0 .. 15.
    0.0 = mathematically lossless. Default for already-lossy input (JPEG/GIF).
    1.0 = visually lossless. Default for other input.
    Recommended range: 0.5 .. 3.0.
 --target_size=N
    Aim at file size of N bytes.
    Compresses to 1 % of the target size in ideal conditions.
    Runs the same algorithm as --target_bpp
 --target_bpp=BPP
    Aim at file size that has N bits per pixel.
    Compresses to 1 % of the target BPP in ideal conditions.
 -q QUALITY, --quality=QUALITY
    Quality setting. Range: -inf .. 100.
    100 = mathematically lossless. Default for already-lossy input (JPEG/GIF). Positive quality values roughly match libjpeg quality.

 -s SPEED, --speed=SPEED
    Encoder effort/speed setting. Valid values are:
    3|falcon| 4|cheetah| 5|hare| 6|wombat| 7|squirrel| 8|kitten| 9|tortoise
    Default: squirrel (7). Values are in order from faster to slower.
 -p, --progressive
    Enable progressive/responsive decoding.
 --middleout
    Put center groups first in the compressed file.
 --progressive_ac
    Use the progressive mode for AC.
 --qprogressive_ac
    Use the progressive mode for AC.
 --progressive_dc=num_dc_frames
    Use progressive mode for DC.
 -m, --modular
    Use the modular mode (lossy / lossless).
 --use_new_heuristics
    use new and not yet ready encoder heuristics
 -j, --jpeg_transcode
    Do lossy transcode of input JPEG file (decode to pixels instead of doing lossless transcode).
 --num_threads=N
    number of worker threads (zero = none).
 --num_reps=N
    how many times to compress.
 --noise=0|1
    force enable/disable noise generation.
 --dots=0|1
    force enable/disable dots generation.
 --patches=0|1
    force enable/disable patches generation.
 --resampling=1|2|4|8
    Subsample all color channels by this factor
 --epf=-1..3
    Edge preserving filter level (-1 = choose based on quality, default)
 --gaborish=0|1
    force disable gaborish.
 --intensity_target=N
    Intensity target of monitor in nits, higher
   results in higher quality image. Must be strictly positive.
   Default is 255 for standard images, 4000 for input images known to
   to have PQ or HLG transfer function.
 -x key=value, --dec-hints=key=value
    color_space indicates the ColorEncoding, see Description();
icc_pathname refers to a binary file containing an ICC profile.
 -Q luma_q[,chroma_q], --mquality=luma_q[,chroma_q]
    [modular encoding] lossy 'quality' (100=lossless, lower is more lossy)
 -C K, --colorspace=K
    [modular encoding] color transform: 0=RGB, 1=YCoCg, 2-37=RCT (default: try several, depending on speed)
 -g K, --group-size=K
    [modular encoding] set group size to 128 << K (default: 1 or 2)
 -P K, --predictor=K
    [modular encoding] predictor(s) to use: 0=zero, 1=left, 2=top, 3=avg0, 4=select, 5=gradient, 6=weighted, 7=topright, 8=topleft, 9=leftleft, 10=avg1, 11=avg2, 12=avg3, 13=toptop predictive average 14=mix 5 and 6, 15=mix everything. Default 14, at slowest speed default 15
 -N max_d, --near-lossless=max_d
    [modular encoding] apply near-lossless preprocessing with maximum delta = max_d
 --palette=K
    [modular encoding] use a palette if image has at most K colors (default: 1024)
 --lossy-palette
    [modular encoding] quantize to a palette that has fewer entries than would be necessary for perfect preservation; for the time being, it is recommended to set --palette=0 with this option to use the default palette only
 -R K, --responsive=K
    [modular encoding] do Squeeze transform, 0=false, 1=true (default: true if lossy, false if lossless)
 -v, --verbose
    Verbose output (also applies to help).
 -h, --help
    Prints this help message (use -v to see more options).
*/

    }
}
