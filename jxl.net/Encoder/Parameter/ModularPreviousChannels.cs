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
    /// 'E, modular_nb_prev_channels, -1..11,\r\n        Maximum number of previous-channel MA tree properties to use, default \r\n        -1. -1 = encoder chooses.",
    /// [modular encoding] number of extra MA tree properties to use
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class ModularPreviousChannels : jxlNET.Parameter
    {
        public override bool? Available => true; // -E is now modular_nb_prev_channels in v0.11.2 and since?
        public override string Description => "E, modular_nb_prev_channels, -1..11,\r\n        Maximum number of previous-channel MA tree properties to use, default \r\n        -1. -1 = encoder chooses.";
        public override string Name => "ModularPreviousChannels";
        public override string Param => "-E";
        public override string ParamLong => "--extra-properties";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public ModularPreviousChannels() { }
        public ModularPreviousChannels(int Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public int MinValue = -1;
        [XmlIgnoreAttribute]
        public int MaxValue = 11;

        private int _value = -1;

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
