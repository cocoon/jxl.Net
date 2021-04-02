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
    /// 'X', "pre-compact", "PERCENT",
    /// [modular encoding] compact channels (globally) if ratio used/range is below this (default: 80%)
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class PreCompact : jxlNET.Parameter
    {
        public override string Description => "X, pre-compact, PERCENT, [modular encoding] compact channels (globally) if ratio used/range is below this (default: 80%)";
        public override string Name => "PreCompact";
        public override string Param => "-X";
        public override string ParamLong => "--pre-compact";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public PreCompact() { }
        public PreCompact(float Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public float MinValue = 1;
        [XmlIgnoreAttribute]
        public float MaxValue = 100;

        private float _value = 80f;

        /// <summary>
        /// Valid values are: [1:100]
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
