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
    /// "Order in which groups are stored in the codestream for progressive rendering, default = 0.0 = scanline order. 1 = center-first order."
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class GroupOrder : jxlNET.Parameter
    {
        public override string Description => "Order in which groups are stored in the codestream for progressive rendering, default = 0.0 = scanline order. 1 = center-first order.";
        public override string Name => "GroupOrder";
        public override string Param => ParamLong;
        public override string ParamLong => "--group_order";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public GroupOrder() { }
        public GroupOrder(int Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public int MinValue = 0;
        [XmlIgnoreAttribute]
        public int MaxValue = 3;

        private int _value = 0;

        /// <summary>
        /// Valid values are: [0:3]
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
