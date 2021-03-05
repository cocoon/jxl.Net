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
    /// "resampling", "1|2|4|8", "Subsample all color channels by this factor"
    /// </summary>
    public class Resampling : jxlNET.Parameter
    {
        public override string Description => "resampling, 1|2|4|8, Subsample all color channels by this factor";
        public override string Name => "Resampling";
        public override string Param => ParamLong;
        public override string ParamLong => "--resampling";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public Resampling() { }
        public Resampling(jxlNET.ResamplingBase Vaule) { this.Value = Value; }

        [XmlIgnoreAttribute]
        public jxlNET.ResamplingBase MinValue = jxlNET.ResamplingBase.Sampling_1;
        [XmlIgnoreAttribute]
        public jxlNET.ResamplingBase MaxValue = jxlNET.ResamplingBase.Sampling_8;

        private jxlNET.ResamplingBase _value = jxlNET.ResamplingBase.Sampling_1;

        /// <summary>
        /// Valid values are: [1|2|4|8]
        /// </summary>
        public jxlNET.ResamplingBase Value
        {
            get
            {
                //Contract.Requires(_value == jxlNET.Resampling.Sampling_1 || _value == jxlNET.Resampling.Sampling_2 || _value == jxlNET.Resampling.Sampling_4 || _value == jxlNET.Resampling.Sampling_8);
                return _value;
            }
            set
            {
                //Contract.Requires(value == jxlNET.Resampling.Sampling_1 || value == jxlNET.Resampling.Sampling_2 || value == jxlNET.Resampling.Sampling_4 || value == jxlNET.Resampling.Sampling_8);
                _value = value;
            }

        }

        public override string ToString()
        {
            return Param + " " + Value.Value.ToString();
        }


    }
}
