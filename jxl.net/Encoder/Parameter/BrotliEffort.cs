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
    /// "Brotli effort, range: 0 .. 11, default = 9. Higher values allow more computation targeting higher density."
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class BrotliEffort : jxlNET.Parameter
    {
        public override string Description => "Brotli effort, range: 0 .. 11, default = 9. Higher values allow more computation targeting higher density.";
        public override string Name => "BrotliEffort";
        public override string Param => ParamLong;
        public override string ParamLong => "--brotli_effort";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public BrotliEffort() { }
        public BrotliEffort(int Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public int MinValue = 0;
        [XmlIgnoreAttribute]
        public int MaxValue = 11;

        private int _value = 0;

        /// <summary>
        /// Valid values are: [0:11]
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
