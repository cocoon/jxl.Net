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
    /// "epf", "-1..3", "Edge preserving filter level (-1 = choose based on quality, default)"
    /// </summary>
    public class EdgePreservingFilterLevel : jxlNET.Parameter
    {
        public override string Description => "epf, -1..3, Edge preserving filter level (-1 = choose based on quality, default)";
        public override string Name => "EdgePreservingFilterLevel";
        public override string Param => ParamLong;
        public override string ParamLong => "--epf";
        public override OptionType OptionType => OptionType.Value;

        //Constructor
        public EdgePreservingFilterLevel() { }
        public EdgePreservingFilterLevel(jxlNET.EdgePreservingFilterLevelBase Value)
        {
            this.Value = Value;
        }

        [XmlIgnoreAttribute]
        public jxlNET.EdgePreservingFilterLevelBase MinValue = jxlNET.EdgePreservingFilterLevelBase.Level_minus_1;
        [XmlIgnoreAttribute]
        public jxlNET.EdgePreservingFilterLevelBase MaxValue = jxlNET.EdgePreservingFilterLevelBase.Level_3;

        private jxlNET.EdgePreservingFilterLevelBase _value = jxlNET.EdgePreservingFilterLevelBase.Level_minus_1;

        /// <summary>
        /// Valid values are: [-1:3]
        /// </summary>
        public jxlNET.EdgePreservingFilterLevelBase Value
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
