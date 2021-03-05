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
    /// 'C', "colorspace", "K", "[modular encoding] color transform: 0=RGB, 1=YCoCg, "2-37=RCT (default: try several, depending on speed)"
    /// </summary>
    public class ColorSpace : jxlNET.Parameter
    {
        public override string Description => "C, colorspace, K, [modular encoding] color transform: 0=RGB, 1=YCoCg, 2-37=RCT (default: try several, depending on speed)";
        public override string Name => "ColorSpace";
        public override string Param => "-C";
        public override string ParamLong => "--colorspace";
        public override OptionType OptionType => OptionType.Value;

        public ColorSpace() { }
        public ColorSpace(jxlNET.ColorSpaceBase Vaule) { this.Value = Value; }


        [XmlIgnoreAttribute]
        public jxlNET.ColorSpaceBase MinValue = jxlNET.ColorSpaceBase.RGB;
        [XmlIgnoreAttribute]
        public jxlNET.ColorSpaceBase MaxValue = jxlNET.ColorSpaceBase.YCoCg;

        private jxlNET.ColorSpaceBase _value = jxlNET.ColorSpaceBase.RGB;

        /// <summary>
        /// Valid values are: [0:37]
        /// </summary>
        public jxlNET.ColorSpaceBase Value
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
