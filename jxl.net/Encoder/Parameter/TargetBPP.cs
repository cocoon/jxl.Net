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
    /// "target_bpp", "BPP", ("Aim at file size that has N bits per pixel.\n"   "    Compresses to 1 % of the target BPP in ideal conditions."
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class TargetBPP : jxlNET.Parameter
    {
        public override string Description => "target_bpp, BPP, (Aim at file size that has N bits per pixel.\nCompresses to 1 % of the target BPP in ideal conditions.";
        public override string Name => "TargetBPP";
        public override string Param => ParamLong;
        public override string ParamLong => "--target_bpp";
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
        public TargetBPP() { }
        public TargetBPP(int Vaule) { this.Value = Value; }

        [XmlIgnoreAttribute]
        public int MinValue = 1;
        [XmlIgnoreAttribute]
        public int MaxValue = int.MaxValue;

        private int _value = 16;

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
