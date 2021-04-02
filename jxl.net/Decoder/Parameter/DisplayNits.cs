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

namespace jxlNET.Decoder.Parameters
{

    /// <summary>
    /// '\0', "display_nits", 0.3-250, luminance range of the display to which to tone-map; the lower bound can be omitted, &amp;display_nits, &amp;ParseLuminanceRange);
    /// </summary>

    [XmlRoot(Namespace = "jxlNET.Decoder.Parameters")]
    public class DisplayNits : jxlNET.Parameter
    {
        public override string Description => "\0, display_nits, 0.3-250, luminance range of the display to which to tone-map; the lower bound can be omitted, &display_nits, &ParseLuminanceRange);";
        public override string Name => "DisplayNits";
        public override string Param => ParamLong;
        public override string ParamLong => "--display_nits";
        public override OptionType OptionType => OptionType.Value;

        public string MinVersion = "0.3.3"; // added here: https://gitlab.com/wg1/jpeg-xl/-/commit/b1c6fdcd3f6f2720daf3eb953761f1087206f981?page=2

        //Constructor
        public DisplayNits() { }
        public DisplayNits(float Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public float MinValue = 0.3f;
        [XmlIgnoreAttribute]
        public float MaxValue = 250;

        private float _value = 0.3f;

        /// <summary>
        /// Valid values are: [ 0.3:250]
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
            return Param + " " + Value.ToString("N2", cultureInfo);
        }
    }
}
