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
    /// 'c', "colortransform", "0..2", "0=XYB, 1=None, 2=YCbCr"
    /// </summary>
    public class ColorTransform : jxlNET.Parameter
    {
        public override string Description => "c, colortransform, 0..2, 0=XYB, 1=None, 2=YCbCr";
        public override string Name => "ColorTransform";
        public override string Param => "-c";
        public override string ParamLong => "--colortransform";
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
