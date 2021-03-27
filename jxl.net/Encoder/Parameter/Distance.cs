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
    ///'d', "distance", "maxError",
    /// Max. butteraugli distance, lower = higher quality. Range: 0 .. 15.
    /// 0.0 = mathematically lossless. Default for already-lossy input (JPEG/GIF).
    /// 1.0 = visually lossless. Default for other input.
    /// Recommended range: 0.5 .. 3.0.
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class Distance : jxlNET.Parameter
    {
        public override string Description => "d, distance, maxError, Max. butteraugli distance, lower = higher quality. Range: 0 .. 15. 0.0 = mathematically lossless. Default for already-lossy input (JPEG/GIF). 1.0 = visually lossless. Default for other input. Recommended range: 0.5 .. 3.0.";
        public override string Name => "Distance";
        public override string Param => "-d";
        public override string ParamLong => "--distance";
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
        public Distance() { }
        public Distance(float Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public float MinValue = 0;
        [XmlIgnoreAttribute]
        public float MaxValue = 15;

        private float _value = 0.0f;

        /// <summary>
        /// Valid values are: [0:15]
        /// </summary>
        public float Value
        {
            get
            {
                Contract.Requires(_value >= MinValue);
                Contract.Requires(_value <= MaxValue);
                return _value;
            }

            //set => _value = CheckArgumentRange(nameof(value), value, MinValue, MaxValue);

            
            set
            {
                Contract.Requires(value >= MinValue);
                Contract.Requires(value <= MaxValue);

                if (value < MinValue || value > MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value,"Valid values are: [" + MinValue + ":" + MaxValue + "]");
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
