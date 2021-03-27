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
using System.Diagnostics.Contracts;
using System.Xml.Serialization;

namespace jxlNET.Encoder.Parameters
{
    /// <summary>
    /// "target_size", "N", ("Aim at file size of N bytes.\n" "    Compresses to 1 % of the target size in ideal conditions.\n" "    Runs the same algorithm as --target_bpp"
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class TargetSize : jxlNET.Parameter
    {
        public override string Description => "target_size, N, (Aim at file size of N bytes.\nCompresses to 1 % of the target size in ideal conditions.\nRuns the same algorithm as --target_bpp";
        public override string Name => "TargetSize";
        public override string Param => ParamLong;
        public override string ParamLong => "--target_size";
        public override OptionType OptionType => OptionType.Value;


        /*
        if (got_target_bpp + got_target_size + got_distance + got_quality > 1) {
        fprintf(stderr,
                "You can specify only one of '--distance', '-q', "
            "'--target_bpp' and '--target_size'. They are all different ways"
            " to specify the image quality. When in doubt, use --distance."
            " It gives the most visually consistent results.\n");
        */

        //Constructor
        public TargetSize() { }
        public TargetSize(int Vaule) { this.Value = Value; }

        [XmlIgnoreAttribute]
        public int MinValue = 1;
        [XmlIgnoreAttribute]
        public int MaxValue = int.MaxValue;

        private int _value = 256;

        /// <summary>
        /// Valid values are: [1:int.MaxValue]
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
