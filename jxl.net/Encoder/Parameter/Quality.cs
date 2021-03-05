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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Xml.Serialization;

namespace jxlNET.Encoder.Parameters
{
    /// <summary>
    /// 'q', "quality", "QUALITY", "Quality setting.
    /// Range: -inf .. 100. 100 = mathematically lossless. Default for already-lossy input (JPEG/GIF). Positive quality values roughly match libjpeg quality.
    /// </summary>
    public class Quality : jxlNET.Parameter
    {
        public override string Description => "q, quality, QUALITY, Quality setting.\nRange: -inf.. 100.\n 100 = mathematically lossless. Default for already-lossy input (JPEG/GIF).\n Positive quality values roughly match libjpeg quality.";
        public override string Name => "Quality";
        public override string Param => "-q";
        public override string ParamLong => "--quality";
        public override OptionType OptionType => OptionType.Value;

        /*
        if (got_target_bpp + got_target_size + got_distance + got_quality > 1) {
        fprintf(stderr,
                "You can specify only one of '--distance', '-q', "
            "'--target_bpp' and '--target_size'. They are all different ways"
            " to specify the image quality. When in doubt, use --distance."
            " It gives the most visually consistent results.\n");
        */

        public Quality() { }
        public Quality(int Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public int MinValue = int.MinValue;
        [XmlIgnoreAttribute]
        public int MaxValue = 100;

        private int _value = 100;


        /// <summary>
        /// Valid values are: [int.MinValue:100]
        /// </summary>
        public int Value
        {
            get
            {
                Contract.Requires(_value >= MinValue);
                Contract.Requires(_value <= MaxValue);
                return _value;
            }
            set
            {
                Contract.Requires(value >= MinValue);
                Contract.Requires(value <= MaxValue);

                if (value < MinValue || value > MaxValue)
                {
                    throw new ArgumentOutOfRangeException("Valid values are: [" + MinValue + ":"+ MaxValue + "]");
                }

                _value = value;
            }

        }

        public override string ToString()
        {
            return Param + " " + Value.ToString();
        }


    }
}
