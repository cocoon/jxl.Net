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
using System.Xml.Serialization;

namespace jxlNET.Decoder.Parameters
{
    /// <summary>
    /// '\0', "color_space", "RGB_D65_SRG_Rel_Lin", defaults to original (input) color space
    /// </summary>
    [XmlRoot(Namespace = "jxlNET.Decoder.Parameters")]
    public class ColorSpace : jxlNET.Parameter
    {
        public override string Description => "\0, color_space, RGB_D65_SRG_Rel_Lin, defaults to original (input) color space";
        public override string Name => "ColorSpace";
        public override string Param => ParamLong;
        public override string ParamLong => "--color_space";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public ColorSpace() { }
        public ColorSpace(string Vaule) { this.Value = Value; }

        private string _value = String.Empty;

        /// <summary>
        /// Valid values are: [string]
        /// </summary>
        public string Value
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
            return Param + " " + Value;
        }


    }
}
