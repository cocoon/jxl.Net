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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Xml.Serialization;

namespace jxlNET.Deccoder.Parameters
{
    /// <summary>
    /// '\0', "bits_per_sample", "N", defaults to original (input) bit depth
    /// </summary>
    public class BitsPerSample : jxlNET.Parameter
    {
        public override string Description => "\0, bits_per_sample, N, defaults to original (input) bit depth";
        public override string Name => "BitsPerSample";
        public override string Param => ParamLong;
        public override string ParamLong => "--bits_per_sample";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public BitsPerSample() { }
        public BitsPerSample(int Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public int MinValue = 0;
        [XmlIgnoreAttribute]
        public int MaxValue = int.MaxValue;

        private int _value = 0;

        /// <summary>
        /// Valid values are: [0:int.MaxValue]
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
