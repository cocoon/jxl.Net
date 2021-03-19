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

namespace jxlNET.Encoder.Parameters
{

    /// <summary>
    /// 's', "speed", "EFFORT","Encoder effort/speed setting. 
    /// Valid values are: 
    /// 3|falcon| 4|cheetah| 5|hare| 6|wombat| 7|squirrel| 8|kitten| 9|tortoise
    /// Default: squirrel (7).
    /// Values are in order from faster to slower."
    /// </summary>

    public class Speed : jxlNET.Parameter
    {
        public override string Description => "s, speed, EFFORT, Encoder effort/speed setting.\n Valid values are:\n 3|falcon| 4|cheetah| 5|hare| 6|wombat| 7|squirrel| 8|kitten| 9|tortoise\n Default: squirrel (7).\n Values are in order from faster to slower.";
        public override string Name => "Speed";
        public override string Param => "-s";
        public override string ParamLong => "--speed";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public Speed() { }
        public Speed(int Value) { this.Value = Value; }

        [XmlIgnoreAttribute]
        public int MinValue = 3;
        [XmlIgnoreAttribute]
        public int MaxValue = 9;

        private int _value = 9;

        /// <summary>
        /// Valid values are: [3:9]
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
