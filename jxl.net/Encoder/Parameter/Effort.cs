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
    /// 'e', "effort", "EFFORT","Encoder effort setting. Range: 1 .. 9. 
    /// Default: 7. 
    /// Higher number is more effort (slower)."
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class Effort : jxlNET.Parameter
    {
        public override string Description => "e, effort, EFFORT, Encoder effort setting. Range: 1 .. 9. (available since v0.5.0)\n Valid values are:\n 1|lightning| 2|thunder| 3|falcon| 4|cheetah| 5|hare| 6|wombat| 7|squirrel| 8|kitten| 9|tortoise\n Default: squirrel (7).\n Default: 7. Higher number is more effort (slower).";
        public override string Name => "Effort";
        public override string Param => "-e";
        public override string ParamLong => "--effort";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public Effort() { }
        public Effort(int Value) { this.Value = Value; }

        [XmlIgnoreAttribute]
        public int MinValue = 1;
        [XmlIgnoreAttribute]
        public int MaxValue = 9;

        private int _value = 9;

        /// <summary>
        /// Valid values are: [1:9]
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
