// Copyright (c) 2026 github.com/cocoon
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
    /// "P, modular_predictor, 0..15,\r\n        Predictor(s) to use. 0 = zero. 1 = left. 2 = top. 3 = avg0. 4 = \r\n        select. 5 = gradient.\r\n            6 = weighted. 7 = topright. 8 = topleft. 9 = leftleft. \r\n        10 = avg1. 11 = avg2. 12 = avg3.\r\n            13 = toptop predictive average. 14 = mix 5 and 6. 15 = mix \r\n        everything.\r\n            Default = 14 at effort < 10 and 15 at effort 10."
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class ModularPredictor : jxlNET.Parameter
    {
        public override bool? Available => true;
        public override string Description => "P, modular_predictor, 0..15,\r\n        Predictor(s) to use. 0 = zero. 1 = left. 2 = top. 3 = avg0. 4 = \r\n        select. 5 = gradient.\r\n            6 = weighted. 7 = topright. 8 = topleft. 9 = leftleft. \r\n        10 = avg1. 11 = avg2. 12 = avg3.\r\n            13 = toptop predictive average. 14 = mix 5 and 6. 15 = mix \r\n        everything.\r\n            Default = 14 at effort < 10 and 15 at effort 10.";
        public override string Name => "ModularPredictor";
        public override string Param => "-P";
        public override string ParamLong => "--modular_predictor";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public ModularPredictor() { }
        public ModularPredictor(int Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public int MinValue = 0;
        [XmlIgnoreAttribute]
        public int MaxValue = 15;

        private int _value = 1;

        /// <summary>
        /// Valid values are: [0:15]
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
                    throw new ArgumentOutOfRangeException("Valid values are: [" + MinValue + ":" + MaxValue + "]");
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
