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

using System.Xml.Serialization;

namespace jxlNET.Encoder.Parameters
{
    /// <summary>
    /// "ec_resampling, -1|1|2|4|8,\r\n                            Resampling for extra channels. Same as \r\n                            --resampling but for extra channels like alpha."
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class ECResampling : jxlNET.Parameter
    {
        public override bool? Available => true;
        public override string Description => "ec_resampling, -1|1|2|4|8,\r\n                            Resampling for extra channels. Same as \r\n                            --resampling but for extra channels like alpha.";
        public override string Name => "ECResampling";
        public override string Param => ParamLong;
        public override string ParamLong => "--ec_resampling";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public ECResampling() { }
        public ECResampling(jxlNET.ResamplingBase Vaule) { this.Value = Value; }

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
