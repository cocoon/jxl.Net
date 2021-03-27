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
    /// 's', "downsampling", "1,2,4,8,16", maximum permissible downsampling factor (values greater than 16 will return the LQIP if available)
    /// </summary>

    [XmlRoot(Namespace = "jxlNET.Decoder.Parameters")]
    public class DownSampling : jxlNET.Parameter
    {
        public override string Description => "s, downsampling, 1,2,4,8,16, maximum permissible downsampling factor (values greater than 16 will return the LQIP if available)";
        public override string Name => "DownSampling";
        public override string Param => "-s";
        public override string ParamLong => "--downsampling";
        public override OptionType OptionType => OptionType.Value;


        //Constructor
        public DownSampling() { }
        public DownSampling(int Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public int MinValue = 1;
        [XmlIgnoreAttribute]
        public int MaxValue = int.MaxValue;

        private int _value = 85;

        /// <summary>
        /// Valid values are: [ 1,2,4,8,16:int.MaxValue]
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
