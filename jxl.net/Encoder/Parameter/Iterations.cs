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
using System.Globalization;
using System.Xml.Serialization;

namespace jxlNET.Encoder.Parameters
{

    /// <summary>
    ///'I', "iterations", "F",
    /// PERCENT,\r\n        Percentage of pixels used to learn MA trees, default = -1.\r\n            -1 = encoder chooses. 0 = don't use MA trees.\r\n            Higher values use more memory and may result in better \r\n        compression.
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class Iterations : jxlNET.Parameter
    {
        public override bool? Available => true;
        public override string Description => "I, iterations, PERCENT,\r\n        Percentage of pixels used to learn MA trees, default = -1.\r\n            -1 = encoder chooses. 0 = don't use MA trees.\r\n            Higher values use more memory and may result in better \r\n        compression.";
        public override string Name => "Iterations";
        public override string Param => "-I";
        public override string ParamLong => "--iterations";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public Iterations() { }
        public Iterations(float Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public float MinValue = 0;
        [XmlIgnoreAttribute]
        public float MaxValue = float.MaxValue;

        private float _value = 0.5f;

        /// <summary>
        /// Valid values are: [0:float.MaxValue]
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
