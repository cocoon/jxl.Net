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
    /// 'C, modular_colorspace, -1..41,\r\n                            Color transform, default = -1. -1 = try several \r\n                            per group, depending on effort. 0 = RGB (none).\r\n                                1 .. 41 = fixed RCT. 6 = YCoCg."
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Encoder.Parameters")]
    public class ColorTransform : jxlNET.Parameter
    {
        public override bool? Available => true;
        public override string Description => "C, modular_colorspace, -1..41,\r\n                            Color transform, default = -1. -1 = try several \r\n                            per group, depending on effort. 0 = RGB (none).\r\n                                1 .. 41 = fixed RCT. 6 = YCoCg.";
        public override string Name => "ColorTransform";
        public override string Param => "-C";
        public override string ParamLong => "--modular_colorspace";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public ColorTransform() { }
        public ColorTransform(jxlNET.ColorTransformBase Vaule) { this.Value = Value; }

        [XmlIgnoreAttribute]
        public jxlNET.ColorTransformBase MinValue = jxlNET.ColorTransformBase.XYB;
        [XmlIgnoreAttribute]
        public jxlNET.ColorTransformBase MaxValue = jxlNET.ColorTransformBase.YCbCr;

        private jxlNET.ColorTransformBase _value = jxlNET.ColorTransformBase.None;

        /// <summary>
        /// Valid values are: [0:2]
        /// </summary>
        public jxlNET.ColorTransformBase Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }

        }

        public override string ToString()
        {
            return Param + " " + Value.Value.ToString();
        }


    }
}
