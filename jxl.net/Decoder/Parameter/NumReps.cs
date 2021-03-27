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
    /// "num_reps", "N", "how many times to compress."
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Decoder.Parameters")]
    public class NumReps : jxlNET.Parameter
    {
        public override string Description => "num_reps, N, how many times to compress.";
        public override string Name => "NumReps";
        public override string Param => ParamLong;
        public override string ParamLong => "--num_reps";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public NumReps() { }
        public NumReps(int Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public int MinValue = 0;
        [XmlIgnoreAttribute]
        public int MaxValue = int.MaxValue;

        private int _value = 12;

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
