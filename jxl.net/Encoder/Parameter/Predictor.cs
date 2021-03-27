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
    /// 'P', "predictor", "K",
    /// [modular encoding] predictor(s) to use: 0=zero, 
    /// 1=left, 2=top, 3=avg0, 4=select, 5=gradient, 6=weighted, 
    /// 7=topright, 8=topleft, 9=leftleft, 10=avg1, 11=avg2, 12=avg3, 
    /// 13=toptop predictive average 
    /// 14=mix 5 and 6, 15=mix everything. Default 14, at slowest speed 
    /// default 15
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class Predictor : jxlNET.Parameter
    {
        public override string Description => "P, predictor, K, [modular encoding]\npredictor(s) to use:\n0=zero, 1=left, 2=top, 3=avg0, 4=select, 5=gradient, 6=weighted, 7=topright, 8=topleft, 9=leftleft, 10=avg1, 11=avg2, 12=avg3, \n13=toptop predictive average 14=mix 5 and 6, 15=mix everything.\nDefault 14, at slowest speed default 15";
        public override string Name => "Predictor";
        public override string Param => "-P";
        public override string ParamLong => "--predictor";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public Predictor() { }
        public Predictor(int Vaule) { this.Value = Value; }

        [XmlIgnoreAttribute]
        public int MinValue = 1;
        [XmlIgnoreAttribute]
        public int MaxValue = 15;

        private int _value = 15;

        /// <summary>
        /// Valid values are: [1:15]
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
